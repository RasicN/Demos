using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Newtonsoft.Json;
using Shared.Messages;

namespace Subscriber2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting up Subscriber2...");
            var busControl = CreateUsingAzure();
            busControl.Start();
            Console.WriteLine("Connected and waiting for messages...");


            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static IBusControl CreateUsingRabbitMq()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://rabbitdomain"), h =>
                {
                    h.Username("username");
                    h.Password("password");
                });

                cfg.ReceiveEndpoint(host, "demosubscriber2", ep =>
                {
                    ep.Durable = true;
                    ep.Consumer<OrderSubmittedConsumer>();
                });

            });
        }

        private static IBusControl CreateUsingAzure()
        {
            var connectionString = "{ConnectionString}"; // Must have manage permissions
            return Bus.Factory.CreateUsingAzureServiceBus(cfg =>
            {
                var host = cfg.Host(connectionString, x => { });

                cfg.ReceiveEndpoint(host, "demosubscriber2", ep =>
                {
                    ep.EnablePartitioning = false;
                    ep.Consumer<OrderSubmittedConsumer>();
                });
            });
        }
    }
    internal class OrderSubmittedConsumer : IConsumer<OrderSubmitted>
    {
        public async Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            await Console.Out.WriteLineAsync($"Received: {JsonConvert.SerializeObject(context.Message)}");
        }
    }
}
