using RabbitMQ.Client;
using System.Text;


// Baglantı olusturma
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd");

// Baglantıyı aktiflestirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue oluşturma
channel.QueueDeclare(queue: "example-queue",exclusive:false);

// Queue mesaj gönderme

//RabbitMQ kuyruğa atacağı mesajları byte türünden kabul etmektedir.

//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange:"",routingKey: "example-queue", body: message);

for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Merhaba"+i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}

Console.Read();