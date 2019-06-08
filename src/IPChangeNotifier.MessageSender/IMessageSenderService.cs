using System.Threading.Tasks;

namespace IPChangeNotifier.MessageSender
{
    public interface IMessageSenderService
    {
        Task Send(string message);
    }
}