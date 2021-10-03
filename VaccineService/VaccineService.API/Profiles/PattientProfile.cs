using AutoMapper;
using System.Linq;
using VaccineService.Domains;
using VaccineService.Models.Patient;

namespace VaccineService.API.Profiles
{
    public class PattientProfile : Profile
    {
        public PattientProfile()
        {
            CreateMap<PatientInputModel, Patient>();
            CreateMap<Patient, PatientOutputModel>().
                ForMember(dest => dest.Vaccines, src => src.MapFrom(x =>x.Vaccnies.Select(p => p.Vaccine.Name)));
        }
    }
}
