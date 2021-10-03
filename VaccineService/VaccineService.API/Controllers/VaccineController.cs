using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VaccineService.Domains;
using VaccineService.Models.Vaccine;
using VaccineService.Services.Contracts;

namespace VaccineService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService vaccineService;
        private readonly IMapper mapper;

        public VaccineController(IVaccineService vaccineService, IMapper mapper)
        {
            this.vaccineService = vaccineService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
            => Ok(this.mapper.Map<ICollection<VaccineOutputModel>>(this.vaccineService.All()));

        [HttpPost]
        public ActionResult Add(VaccineInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vaccine = this.mapper.Map<Vaccine>(model);

            try
            {
                this.vaccineService.Add(vaccine);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return CreatedAtRoute(nameof(GetById), new { id = vaccine.Id }, vaccine);
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<VaccineOutputModel> GetById(int id)
        {
            var vaccine = this.vaccineService.GetById(id);

            if (vaccine is null)
            {
                return NotFound();
            }

            return this.mapper.Map<VaccineOutputModel>(vaccine);
        }

        [HttpPost("injekt")]
        public ActionResult InjektVaccine(VaccinePatientInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var sucess = this.vaccineService.AddForPattient(model.VaccineId, model.PatientId);

            if (!sucess)
            {
                return BadRequest();
            }

            return Ok("The pattient is succesflly vaccinated");
        }
    }
}
