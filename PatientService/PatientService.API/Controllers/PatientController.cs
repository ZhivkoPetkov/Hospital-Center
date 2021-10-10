using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientService.API.AsyncDataProvider;
using PatientService.Data;
using PatientService.Domains;
using PatientService.Models.Patient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientService.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepository patientRepository;
        private readonly IMapper mapper;
        private readonly IMessageBusClient messageBusClient;

        public PatientController(IRepository patientRepository, IMapper mapper, 
                                IMessageBusClient messageBusClient)
        {
            this.patientRepository = patientRepository;
            this.mapper = mapper;
            this.messageBusClient = messageBusClient;
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<PatientOutputModel> GetById(int id)
        {
            var patient = this.patientRepository.GetByPatientId(id);
            if (patient == null)
            {
                return NotFound();
            }

            return (this.mapper.Map<PatientOutputModel>(patient));
        }

        [HttpPost()]
        public async Task<ActionResult> Create(PatientInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var patient = this.mapper.Map<Patient>(model);

            try
            {
                this.patientRepository.Add(patient);
                this.patientRepository.SaveChanges();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            this.messageBusClient.PublishPatient(this.mapper.Map<PatientPublishModel>(patient));

            return CreatedAtRoute(nameof(GetById), new { id = patient.Id }, patient);
        }

        [HttpGet()]
        public ActionResult GetPatients()
        {
            var patients = this.patientRepository.GetAll();

            return Ok(this.mapper.Map<ICollection<PatientOutputModel>>(patients));
        }
    }
}
