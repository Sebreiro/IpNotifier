using System;
using System.Threading.Tasks;

namespace IPChangeNotifier.Application.Factory
{
    public interface IMessageJobFactory
    {
        Func<Task<string>> GetJob();
    }
}