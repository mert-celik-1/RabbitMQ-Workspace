using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;



// Baglantı olusturma
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd");

// Baglantıyı aktiflestirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

// Mesaj okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume("example-queue",autoAck: false,consumer);

consumer.Received += (sender, e) =>
{
    //Kuyruga gelen mesajın işlendiği yerdir
    //e.Body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.ToArray() 
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    channel.BasicAck(e.DeliveryTag, false);

};

Console.Read();