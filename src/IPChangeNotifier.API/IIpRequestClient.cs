using System.Threading.Tasks;

namespace IPChangeNotifier.Clients
{
    public interface IIpRequestClient
    {
        Task<string> GetIpAddress();
    }
}