using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VaccineService.Domains;
using VaccineService.Models.Patient;
using VaccineService.Services.Contracts;

namespace VaccineService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IMapper mapper;

        public PatientController(IPatientService patientService, IMapper mapper)
        {
            this.patientService = patientService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<PatientOutputModel> GetById(int id)
        {
            var patient = this.patientService.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }

            return (this.mapper.Map<PatientOutputModel>(patient));
        }

        [HttpGet()]
        public ActionResult GetPatients()
        {
            var patients = this.patientService.All();

            return Ok(this.mapper.Map<ICollection<PatientOutputModel>>(patients));
        }

        [HttpPost()]
        public ActionResult AddPatient(PatientInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var patients = this.patientService.AddPatient(this.mapper.Map<Patient>(model));

            return Ok();
        }
    }
}
