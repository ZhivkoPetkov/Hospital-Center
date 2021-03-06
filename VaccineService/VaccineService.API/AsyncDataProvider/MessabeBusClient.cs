using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using VaccineService.Models.Vaccine;

namespace VaccineService.API.AsyncDataProvider
{
    public class MessabeBusClient : IMessageBusClient
    {
        private readonly IConfiguration configuration;
        private readonly ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private const string PatientTrigger = "vaccine-trigger";
        public MessabeBusClient(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQHost"],
                Port = int.Parse(configuration["RabbitMQPort"])
            };

            try
            {
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(PatientTrigger, ExchangeType.Fanout);

                connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("Connected to RabbitMQ");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Couldn't connecto to Message bus! {ex.Message}");
            }
        }

        public void PublishVaccine(VaccinePublishModel vaccine)
        {
            var message = JsonSerializer.Serialize(vaccine);
            if (connection.IsOpen)
            {
                Console.WriteLine("Connection is open");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("Connection is not open");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(PatientTrigger, "", null, body);
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("RabbitMQ connection was shut down");
        }
    }
}
