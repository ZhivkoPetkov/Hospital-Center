namespace PatientService.API.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
