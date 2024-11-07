using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQSubscribe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://vijfebwc:tcGO4QbSfEK3cfGkql7fJzHT8yb92zF8@fish.rmq.cloudamqp.com/vijfebwc");

            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("hello-queue", true, false, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("hello-queue",true,consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Gelen Mesaj:" + message);
            };
            Console.ReadLine();
        }
    }
}
