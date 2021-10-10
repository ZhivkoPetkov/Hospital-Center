using PatientService.Models.Patient;

namespace PatientService.API.AsyncDataProvider
{
    public interface IMessageBusClient
    {
        void PublishPatient(PatientPublishModel patient);
    }
}
