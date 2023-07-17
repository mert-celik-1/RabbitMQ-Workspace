using MassTransit;
using Worker_Publisher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, _config) =>
            {
                _config.Host("amqps://iqkofwwd:dikwbq0ziS05tUH6lk4etutORFgaZFEI@cow.rmq2.cloudamqp.com/iqkofwwd");
            });
        });

        services.AddHostedService<PublishMessageService>(provider =>
        {
            using IServiceScope scope = provider.CreateScope();
            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
            return new(publishEndpoint);
        });
    })
    .Build();

host.Run();
