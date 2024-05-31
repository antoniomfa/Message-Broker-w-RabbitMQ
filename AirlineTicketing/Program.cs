using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "password",
    VirtualHost = "/"
};

var conn = factory.CreateConnection();

using (var channel = conn.CreateModel())
{
    channel.QueueDeclare("booking", durable: true, exclusive: true);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, eventArgs) =>
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine($"Message received: {message}");
    };

    channel.BasicConsume("booking", true, consumer);

    Console.ReadKey();
}
