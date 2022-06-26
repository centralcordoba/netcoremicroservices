using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body1 = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body1);
                    Console.WriteLine("Received message {0}...", message);

                };

                channel.BasicConsume("BasicTest", true, consumer);
                Console.WriteLine("Pres [enter] to exit the Consumer...");
                Console.ReadLine();
            }
            
        }
    }
}
