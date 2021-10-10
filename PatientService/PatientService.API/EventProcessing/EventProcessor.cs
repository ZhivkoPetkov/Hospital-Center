using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PatientService.Data;
using PatientService.Models.Patient;
using System;
using System.Text.Json;

namespace PatientService.API.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IMapper mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.PatientVaccinated:
                    AddVaccineStatusForPatient(message);
                    break;
                case EventType.Unknown:
                    break;
                default:
                    break;
            }
        }

        private void AddVaccineStatusForPatient(string message)
        {
            using (var scope = this.scopeFactory.CreateScope())
            {
                var patientRepo = scope.ServiceProvider.GetRequiredService<IRepository>();
                var model = JsonSerializer.Deserialize<PatientVaccinePublishedModel>(message);

                try
                {
                    var patient = patientRepo.GetByPatientId(model.PatientId);
                    patient.IsVaccinated = true;
                    patientRepo.SaveChanges();

                    Console.WriteLine($"Patient with Id: {model.PatientId} was vaccinated!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Patient with Id: {model.PatientId} was not synched with the db! Error: {ex.Message}");
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            var evenType = JsonSerializer.Deserialize<GenericEvent>(notificationMessage);
            switch (evenType.Event)
            {
                case "Patient vaccinated":
                    return EventType.PatientVaccinated;
                default:
                    return EventType.Unknown;
            }
        }
    }

    enum EventType
    {
        PatientVaccinated,
        Unknown
    }
}
