using MassTransit;
using Shared;

string rabbitMQUri = "amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new Uri($"{rabbitMQUri}/{queueName}"));

Console.Write("Gönderilecek mesaj :");
string message = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new ExampleMessage() { Text = message });

Console.Read(); 