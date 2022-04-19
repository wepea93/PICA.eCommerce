using eCommerce.PublisherSubscriber.Contracts;
using eCommerce.PublisherSubscriber.Object;
using eCommerce.PublisherSubscriber.Utilities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace eCommerce.PublisherSubscriber.Messaging
{
    public abstract class PublisherMessage<T> : IPublisher<T>
    {
        private readonly ConnectionFactory _connectionFactory;
        private IModel _channel;
        private readonly IConnection _connection;

        public PublisherMessage() 
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = MessagingUtilities.GetServerName()
            };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public virtual void DistributeMessage(Message<T> message, string exchangeName)
        {
            if(_channel.IsClosed)
            {
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(
                    exchange: exchangeName,
                    type: ExchangeType.Fanout); //topic : guardar mensajes para consumidores fuera de linea
            }

            string content = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(content);

            _channel.BasicPublish(
                exchange: exchangeName,
                routingKey: "", // llenar en caso de usar otro ExchangeType diferente a Fanout
                basicProperties: null,
                body: body);
        }

        public virtual void PublishMessage(Message<T> message, string queueName)
        {

            if (_channel.IsClosed)
            {
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string content = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(content);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: queueName,
                    basicProperties: properties,
                    body: body);
            }
        }
    }
}
