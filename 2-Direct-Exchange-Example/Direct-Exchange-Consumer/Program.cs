using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queueName, "direct-exchange-example", "direct-queue-example");

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queueName,true,consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();
