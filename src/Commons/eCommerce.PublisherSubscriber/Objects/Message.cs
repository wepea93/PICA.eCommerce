
namespace eCommerce.PublisherSubscriber.Object
{
    public class Message<T>
    {
        public DateTime SendDate { get; set; }
        public T BusinessObject { get; set; }

        public Message(DateTime sendDate, T businessObject) 
        {
            SendDate = sendDate;
            BusinessObject = businessObject;
        }
    }
}
