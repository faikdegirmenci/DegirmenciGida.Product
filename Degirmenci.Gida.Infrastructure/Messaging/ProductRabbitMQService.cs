using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DegirmenciGida.Product.Application.Interfaces;

namespace DegirmenciGida.Product.Infrastructure
{
    public class ProductRabbitMQService : BackgroundService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;

        public ProductRabbitMQService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            // RabbitMQ bağlantısını oluşturuyoruz
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "product_request_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            // Mesaj alındığında tetiklenen event
            consumer.Received += (model, ea) =>
            {
                Guid productId;
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                bool isValidGuid = Guid.TryParse(message, out productId);
                var replyProps = _channel.CreateBasicProperties();
                replyProps.CorrelationId = ea.BasicProperties.CorrelationId;

                if (isValidGuid)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

                        // Product service ile işlemler
                        var product = productService.GetAsync(predicate:p=>p.Id == productId,cancellationToken:stoppingToken);
                        var responseMessage = JsonSerializer.Serialize(product.Result);

                        var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
                        _channel.BasicPublish(exchange: "", routingKey: ea.BasicProperties.ReplyTo, basicProperties: replyProps, body: responseBytes);
                    }
                }

            };

            _channel.BasicConsume(queue: "product_request_queue", autoAck: true, consumer: consumer);

            stoppingToken.Register(() =>
            {
                Dispose();
            });


            return Task.CompletedTask; // Bu task sonsuza kadar çalışacak.
        }



        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
