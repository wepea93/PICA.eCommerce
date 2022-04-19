
namespace eCommerce.PublisherSubscriber.Utilities
{
    internal static class MessagingUtilities
    {
        public enum MessageTypes
        {
            Published,
            Distributed
        }

        public static string? GetServerName() 
        {
            string FileToRead = Path.Combine(AppContext.BaseDirectory, "MqServer.txt");
            var lines = File.ReadLines(FileToRead);
            var line = lines.FirstOrDefault();
            return line;
        }
    }
}
