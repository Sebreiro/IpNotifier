using IPChangeNotifier.LiveStreamsCache.Parameters;

namespace IPChangeNotifier.LiveStreamsCache.Services
{
    public interface ILiveStreamsCacheManager
    {
        bool IsNewStream(StreamData data);
    }
}