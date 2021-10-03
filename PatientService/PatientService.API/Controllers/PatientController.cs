using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientService.Data;
using PatientService.Domains;
using PatientService.Models.Patient;
using System.Collections.Generic;

namespace PatientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepository patientRepository;
        private readonly IMapper mapper;

        public PatientController(IRepository patientRepository, IMapper mapper)
        {
            this.patientRepository = patientRepository;
            this.mapper = mapper;
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
        public ActionResult Create(PatientInputModel model)
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
