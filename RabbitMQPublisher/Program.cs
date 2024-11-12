using RabbitMQ.Client;
using System;
using System.Linq;
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

            //channel.QueueDeclare("hello-queue", true, false, false); //Declare işlemleri
            channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout); //Fanout İşlemleri

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                //string message = $"Hello RabbitMQ {x}"; //Declare işlemleri
                string message = $"log {x}"; //Fanout İşlemleri
                var messageBody = Encoding.UTF8.GetBytes(message);

                //channel.BasicPublish("hello-queue",string.Empty, null, messageBody); //Declare işlemleri
                channel.BasicPublish("logs-fanout","", null, messageBody); //Fanout İşlemleri

                Console.WriteLine($"Mesaj Gönderilmiştir: {message}");
            });

            Console.ReadLine();
        }
    }
}
