using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading.Channels;

namespace RabbitMQPublisher
{
    //Log seviyeleri
    public enum LogNames
    {
        Critical=1,
        Error=2,
        Warning=3,
        Info=4
    }

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

            channel.ExchangeDeclare("logs-direct", durable: true, type: ExchangeType.Direct); //Direct İşlemleri

            Enum.GetNames(typeof(LogNames)).ToList().ForEach(x => //Direct İşlemleri
            {
                var routeKey = $"route-{x}";

                var queueName = $"direct-queue-{x}";
                channel.QueueDeclare(queueName, true, false, false);

                channel.QueueBind(queueName, "logs-direct", routeKey, null);
            });

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                LogNames log = (LogNames) new Random().Next(1, 5); //Direct İşlemleri

                //string message = $"Hello RabbitMQ {x}"; //Declare işlemleri
                //string message = $"log {x}"; //Fanout İşlemleri
                string message = $"log-type: {log}"; //Direct İşlemleri
                var messageBody = Encoding.UTF8.GetBytes(message);

                //channel.BasicPublish("hello-queue",string.Empty, null, messageBody); //Declare işlemleri
                /*channel.BasicPublish("logs-fanout","", null, messageBody);*/ //Fanout İşlemleri

                var routeKey = $"route-{log}"; //Direct İşlemleri
                channel.BasicPublish("logs-direct", routeKey, null, messageBody); //Direct İşlemleri

                Console.WriteLine($"Log Gönderilmiştir: {message}");
            });

            Console.ReadLine();
        }
    }
}
