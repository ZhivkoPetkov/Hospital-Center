using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PatientService.API.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatientService.API.AsyncDataProvider
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration configuration;
        private readonly IEventProcessor eventProcessor;
        private IConnection connection;
        private IModel channel;
        private string queueName;

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            this.configuration = configuration;
            this.eventProcessor = eventProcessor;
            this.ConnectToRabbitMq();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("Event received from Vaccination center");

                var messageBody = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(messageBody.ToArray());

                eventProcessor.ProcessEvent(notificationMessage);
            };

            this.channel.BasicConsume(queueName, true, consumer);
            return Task.CompletedTask;
        }

        private void ConnectToRabbitMq()
        {
            var factory = new ConnectionFactory() { HostName = this.configuration["RabbitMQHost"], Port = int.Parse(this.configuration["RabbitMQPort"]) };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            this.channel.ExchangeDeclare("vaccine-trigger", ExchangeType.Fanout);
            this.queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, "vaccine-trigger", "");
        }

    }
}
