using eCommerce.PublisherSubscriber.Contracts;
using eCommerce.PublisherSubscriber.Object;
using eCommerce.PublisherSubscriber.Utilities;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace eCommerce.PublisherSubscriber.Messaging
{
    public abstract class ConsumerMessage<T> : BackgroundService, IConsumer<T>
    {
        private readonly ConnectionFactory _connectionFactory;
        private string _queueName;
        private readonly string _exchangeName;
        private IModel _channel;
        private IConnection _connection;

        public ConsumerMessage()
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = MessagingUtilities.GetServerName()
            };
        }

        public virtual void InitAsDistributedMessage(string exchangeName) 
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(
                      exchange: exchangeName,
                      type: ExchangeType.Fanout);

            _queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(
                queue: _queueName,
                exchange: _exchangeName,
                routingKey: ""); // llenar en caso de usar un ExchangeType diferente a Fanout (debe ser igual al publisher)
        }

        public virtual void InitAsPublishedMessage(string queueName)
        {
            _queueName = queueName;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                        queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, eventArgs) =>
            {
                var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Message<T> message = JsonConvert.DeserializeObject<Message<T>>(content);

                await ProcessMessage(message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };
            _channel.BasicConsume(_queueName, false, consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }

        public abstract Task ProcessMessage(Message<T> messsage);
    }
}
