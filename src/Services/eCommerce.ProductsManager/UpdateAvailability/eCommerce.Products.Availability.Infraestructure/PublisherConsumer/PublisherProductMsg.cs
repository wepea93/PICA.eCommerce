using eCommerce.Commons.Objects.Messaging;
using eCommerce.PublisherSubscriber.Messaging;

namespace eCommerce.Products.Availability.Infraestructure.Publisher
{
    public class PublisherProductMsg : PublisherMessage<IEnumerable<ProductMsg>>
    {
    }
}
