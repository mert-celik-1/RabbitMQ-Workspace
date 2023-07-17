using MassTransit;
using Worker_Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<ExampleMessageConsumer>();

            config.UsingRabbitMq((context, _config) =>
            {
                _config.Host("amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd");

                _config.ReceiveEndpoint("example-message-queue",e=>e.ConfigureConsumer<ExampleMessageConsumer>(context));
            });
        });
    })
    .Build();

await host.RunAsync();
