using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AirlineAPI.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendingMessages<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                VirtualHost = "/"
            };

            var conn = factory.CreateConnection();

            using(var channel = conn.CreateModel())
            {
                channel.QueueDeclare("booking", durable: true, exclusive: true);

                var jsonString = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonString);

                channel.BasicPublish("", "booking", body: body);
            }
        }
    }
}
