using AutoMapper;
using PatientService.Domains;
using PatientService.Models.Patient;

namespace PatientService.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PatientInputModel, Patient>();
            CreateMap<Patient, PatientOutputModel>();
        }
    }
}
