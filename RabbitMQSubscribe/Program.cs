using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

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

            //channel.QueueDeclare("hello-queue", true, false, false);

            //Random bir şekilde kuyruk oluşturduk. Bu işlemi RabbitMQ Client paketi sayesinde yapıyoruz.
            // 'randomQueueName' adında bir kuyruk adı belirliyoruz.
            var randomQueueName = /*"log-database-save-queue";*/ channel.QueueDeclare().QueueName;


            //channel.QueueDeclare(randomQueueName, true, false, false);
            // Kanal üzerinden bir kuyruk tanımlıyoruz.
            // 1. Parametre: Kuyruğun adı ('randomQueueName' olarak belirlenmiş).
            // 2. Parametre: Kuyruğun kalıcı olup olmadığını belirtir (true: kalıcı, yani sunucu yeniden başlatıldığında bile veriler kaybolmaz).
            // 3. Parametre: Kuyruğun özel olup olmadığını belirtir (false: paylaşımlı, yani diğer bağlantılar tarafından da kullanılabilir).
            // 4. Parametre: Otomatik silinip silinmeyeceğini belirtir (false: otomatik silinmez).


            channel.QueueBind(randomQueueName, "logs-fanout", "", null);

            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);

            //channel.BasicConsume("hello-queue",false,consumer); //Declare işlemleri
            channel.BasicConsume(randomQueueName, false, consumer); //Random işlemleri
            Console.WriteLine("Loglar Dinleniyor...");

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj:" + message);

                channel.BasicAck(e.DeliveryTag, false);
            };
            Console.ReadLine();
        }
    }
}
