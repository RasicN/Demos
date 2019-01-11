using System;
using System.Threading.Tasks;
using MassTransit;
using Shared.Messages;
using Shared.Messages.Models;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            // establish connection
            Console.WriteLine("Starting up...");
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://rabbitdomain"), h =>
                {
                    h.Username("username");
                    h.Password("password");
                });

                cfg.ReceiveEndpoint(host, "demopublisher", ep =>
                {
                    ep.Durable = true;
                    ep.Consumer<OrderProcessedConsumer>();
                });
            });
            busControl.Start();
            Console.WriteLine("Connected and waiting for messages...");
            Console.WriteLine("Press key 1 to send a message, 2 to publish a message.");

            var input = Console.ReadLine();
            while (input == "1" || input == "2")
            {

                switch (input)
                {
                    case "1":
                        {
                            var sendEndpoint = busControl.GetSendEndpoint(new Uri("rabbitmq://rabbitmq.d.vu.local/demosubscriber1")).GetAwaiter().GetResult();
                            sendEndpoint.Send<OrderSubmitted>(new
                            {
                                Order = new Order { Amount = 10.47m, Name = "Pizza" }
                            });
                            break;
                        }
                    case "2":
                        {
                            busControl.Publish<OrderSubmitted>(new
                            {
                                Order = new Order { Amount = 10.47m, Name = "Pizza" }
                            });
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                input = Console.ReadLine();
            }
            
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }

    internal class OrderProcessedConsumer : IConsumer<OrderProcessed>
    {
        public async Task Consume(ConsumeContext<OrderProcessed> context)
        {
            await Console.Out.WriteLineAsync($"Order {context.Message.OrderId} was processed!");
        }
    }
}
