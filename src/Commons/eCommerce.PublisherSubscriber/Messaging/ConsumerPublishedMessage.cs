using eCommerce.PublisherSubscriber.Contracts;
using eCommerce.PublisherSubscriber.Object;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace eCommerce.PublisherSubscriber.Messaging
{
    public abstract class ConsumerPublishedMessage<T> : BackgroundService, IConsumer<T>
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _queueName;
        private IModel _channel;
        private IConnection _connection;

        public ConsumerPublishedMessage(string queueName)
        {
            string FileToRead = Path.Combine(AppContext.BaseDirectory, "MqServer.txt");
            var lines = File.ReadLines(FileToRead);
            var line = lines.FirstOrDefault();

            _connectionFactory = new ConnectionFactory()
            {
                HostName = line
            };
            _queueName = queueName;
            Init();
        }

        protected virtual void Init() 
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
