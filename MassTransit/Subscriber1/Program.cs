using System;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;
using Shared.Messages;

namespace Subscriber1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting up Subscriber1...");
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://rabbitdomain"), h =>
                {
                    h.Username("username");
                    h.Password("password");
                });

                cfg.ReceiveEndpoint(host, "demosubscriber1", ep =>
                {
                    ep.Durable = true;
                    ep.Consumer<OrderSubmittedConsumer>();
                });

            });
            busControl.Start();
            Console.WriteLine("Connected and waiting for messages...");


            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }

    internal class OrderSubmittedConsumer : IConsumer<OrderSubmitted>
    {
        public async Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            await Console.Out.WriteLineAsync($"Received: {JsonConvert.SerializeObject(context.Message)}");
            //await Console.Out.WriteLineAsync("Sending OrderProcessed message.");

            //await context.Publish<OrderProcessed>(new
            //{
            //    OrderId = "12345"
            //});
        }
    }
}
