using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPChangeNotifier.Clients
{
    public interface IIpRequestClient
    {
        Task<string> GetIpAddress();
    }
}
