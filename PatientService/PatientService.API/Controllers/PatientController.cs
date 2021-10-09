using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientService.API.HttpProvider;
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
        private readonly IHttpProvider httpProvider;

        public PatientController(IRepository patientRepository, IMapper mapper, IHttpProvider httpProvider)
        {
            this.patientRepository = patientRepository;
            this.mapper = mapper;
            this.httpProvider = httpProvider;
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

            await this.httpProvider.SendPatientData(model);
            return CreatedAtRoute(nameof(GetById), new { id = patient.Id }, patient);
        }

        [HttpGet()]
        public ActionResult GetPatients()
        {
            var patients = this.patientRepository.GetAll();

            return Ok(this.mapper.Map<ICollection<PatientOutputModel>>(patients));
        }

        [HttpPost]
        [Route("vaccine/{patientId}")]
        public ActionResult AddVaccine(int patientId)
        {
            var patient = this.patientRepository.GetByPatientId(patientId);

            if(patient is null)
            {
                return NotFound();
            }
            
            patient.IsVaccinated = true;
            patientRepository.SaveChanges();

            return Ok();
        }
    }
}
