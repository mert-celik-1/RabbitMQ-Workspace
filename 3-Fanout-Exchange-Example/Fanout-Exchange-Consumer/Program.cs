
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

channel.QueueDeclare(queue: "fanout-queue", exclusive: false);

channel.QueueBind("fanout-queue", "fanout-exchange-example", "");

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("fanout-queue",true,consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();