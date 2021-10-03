using AutoMapper;
using VaccineService.Domains;
using VaccineService.Models.Vaccine;

namespace VaccineService.API.Profiles
{
    public class VaccineProfile : Profile
    {
        public VaccineProfile()
        {
            CreateMap<VaccineInputModel, Vaccine>();
            CreateMap<Vaccine, VaccineOutputModel>();
        }
    }
}
