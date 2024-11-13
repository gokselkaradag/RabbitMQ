using RabbitMQ.Client;
using System;
using System.Collections.Generic;
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
            /*channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout);*/ //Fanout İşlemleri

            //channel.ExchangeDeclare("logs-direct", durable: true, type: ExchangeType.Direct); //Direct İşlemleri

            //channel.ExchangeDeclare("logs-topic", durable: true, type: ExchangeType.Topic); //Topic İşlemleri

            channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers); //Headers İşlemleri

            Dictionary<string,object> headers = new Dictionary<string, object>();

            headers.Add("format", "pdf");
            headers.Add("shae", "a4");

            var properties = channel.CreateBasicProperties();
            properties.Headers = headers;

            channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes("Header Mesajım"));
            Console.WriteLine("Mesaj Gönderildi");

            #region LogName
            //Enum.GetNames(typeof(LogNames)).ToList().ForEach(x => //LogName İşlemleri
            //{
            //    /*var routeKey = $"route-{x}";*/ //Direct İşlemleri

            //    //var queueName = $"direct-queue-{x}";
            //    //channel.QueueDeclare(queueName, true, false, false);

            //    //channel.QueueBind(queueName, "logs-direct", routeKey, null);
            //});
            #endregion LogName

            #region Random
            //Random rnd = new Random();
            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    /*LogNames log = (LogNames) new Random().Next(1, 5);*/ //Direct İşlemleri

            //    //string message = $"Hello RabbitMQ {x}"; //Declare işlemleri
            //    //string message = $"log {x}"; //Fanout İşlemleri
            //    /*string message = $"log-type: {log}";*/ //Direct İşlemleri

            //    LogNames logOne = (LogNames)rnd.Next(1, 5);
            //    LogNames logTwo = (LogNames)rnd.Next(1, 5);
            //    LogNames logThree = (LogNames)rnd.Next(1, 5);

            //    var routeKey = $"{logOne}, {logTwo}, {logThree}"; //Topic İşlemleri
            //    string message = $"log-type: {logOne}-{logTwo}-{logThree}"; //Topic İşlemleri
            //    var messageBody = Encoding.UTF8.GetBytes(message);

            //    //channel.BasicPublish("hello-queue",string.Empty, null, messageBody); //Declare işlemleri
            //    /*channel.BasicPublish("logs-fanout","", null, messageBody);*/ //Fanout İşlemleri

            //    /*var routeKey = $"route-{log}";*/ //Direct İşlemleri
            //    /*channel.BasicPublish("logs-direct", routeKey, null, messageBody);*/ //Direct İşlemleri
            //    channel.BasicPublish("logs-topic", routeKey, null, messageBody); //Topic İşlemleri

            //    Console.WriteLine($"Log Gönderilmiştir: {message}");
            //});
            #endregion Random

            Console.ReadLine();
        }
    }
}
