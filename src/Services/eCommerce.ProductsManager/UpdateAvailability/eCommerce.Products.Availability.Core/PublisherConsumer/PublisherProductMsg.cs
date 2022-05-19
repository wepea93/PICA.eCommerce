using eCommerce.Commons.Objects.Messaging;
using eCommerce.PublisherSubscriber.Messaging;

namespace eCommerce.Products.Availability.Core.Publisher
{
    public class PublisherProductMsg : PublisherMessage<IEnumerable<ProductMsg>>
    {
    }
}
