using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Channels;

namespace RabbitMQPublisher
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

            string message = "Hello RabbitMQ";
            var messageBody = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

            Console.WriteLine("Mesaj Gönderilmiştir");
            Console.ReadLine();
        }
    }
}
