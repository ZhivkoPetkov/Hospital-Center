using VaccineService.Models.Vaccine;

namespace VaccineService.API.AsyncDataProvider
{
    public interface IMessageBusClient
    {
        void PublishVaccine(VaccinePublishModel vaccine);
    }
}
