
using eCommerce.PublisherSubscriber.Object;

namespace eCommerce.PublisherSubscriber.Contracts
{
    public interface IPublisher<T>
    {
        void PublishMessage(Message<T> message, string queueName);
        void DistributeMessage(Message<T> message, string exchangeName);
    }
}
