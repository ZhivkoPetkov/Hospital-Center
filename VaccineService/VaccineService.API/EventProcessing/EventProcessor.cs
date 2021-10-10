using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using VaccineService.Domains;
using VaccineService.Models.Patient;
using VaccineService.Services.Contracts;

namespace VaccineService.API.EventProcessing
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
                case EventType.PatientAdded:
                    AddPatient(message);
                    break;
                case EventType.Unknown:
                    break;
                default:
                    break;
            }
        }

        private void AddPatient(string message)
        {
            using (var scope = this.scopeFactory.CreateScope())
            {
                var patientService = scope.ServiceProvider.GetRequiredService<IPatientService>();
                var model = JsonSerializer.Deserialize<PatientInputModel>(message);

                try
                {
                    patientService.AddPatient(this.mapper.Map<Patient>(model));
                    Console.WriteLine("New patient was added to the vaccination center!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occured while adding new patient to the vaccination center! Error: {ex.Message}");
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            var evenType = JsonSerializer.Deserialize<GenericEvent>(notificationMessage);
            switch (evenType.Event)
            {
                case "Patient Added":
                    return EventType.PatientAdded;
                default:
                    return EventType.Unknown;
            }
        }
    }

    enum EventType
    {
        PatientAdded,
        Unknown
    }
}
