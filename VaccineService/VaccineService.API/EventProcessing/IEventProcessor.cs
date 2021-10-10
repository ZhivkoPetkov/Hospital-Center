namespace VaccineService.API.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
