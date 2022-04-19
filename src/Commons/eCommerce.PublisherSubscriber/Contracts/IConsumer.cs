
using eCommerce.PublisherSubscriber.Object;

namespace eCommerce.PublisherSubscriber.Contracts
{
    public interface IConsumer<T>
    {
        void InitAsPublishedMessage(string queueName);
        void InitAsDistributedMessage(string exchangeName);
        Task ProcessMessage(Message<T> messsage);
    }
}
