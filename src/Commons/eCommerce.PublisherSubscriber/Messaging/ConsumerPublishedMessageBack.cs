using eCommerce.PublisherSubscriber.Contracts;
using eCommerce.PublisherSubscriber.Object;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace eCommerce.PublisherSubscriber.Messaging
{
    public class ConsumerPublishedMessageBack : BackgroundService, IConsumer
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _queueName;
        private IModel _channel;
        private IConnection _connection;

        public ConsumerPublishedMessageBack(string queueName)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "MqServer"
            };
            _queueName = queueName;
            Init();
        }

        private void Init() 
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                        queue: _queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
            _channel.BasicQos(prefetchSize:0, prefetchCount: 1, global: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, eventArgs) =>
            {
                var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Message message = JsonConvert.DeserializeObject<Message>(content);

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

        public Task ProcessMessage(Message messsage)
        {
            return Task.CompletedTask;
        }
    }
}
