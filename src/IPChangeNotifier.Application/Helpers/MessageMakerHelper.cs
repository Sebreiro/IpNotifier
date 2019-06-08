namespace IPChangeNotifier.Application.Helpers
{
    public static class MessageMakerHelper
    {
        public static string CreateMessage(string message)
        {
            return $"IP: {message}";
        }
    }
}