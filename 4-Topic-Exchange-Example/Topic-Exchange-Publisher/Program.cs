
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
    Console.Write("Topic belirtiniz :");
    string topic = Console.ReadLine();
    channel.BasicPublish(exchange: "topic-exchange-example", routingKey: topic,body:message);
}

Console.Read();