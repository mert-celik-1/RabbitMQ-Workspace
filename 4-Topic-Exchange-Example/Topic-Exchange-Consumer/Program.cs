using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

Console.Write("Topic : ");
string topic = Console.ReadLine();
string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queueName, exchange: "topic-exchange-example", topic);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: queueName, true, consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();