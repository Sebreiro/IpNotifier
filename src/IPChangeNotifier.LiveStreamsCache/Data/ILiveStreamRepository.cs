using IPChangeNotifier.LiveStreamsCache.Parameters;

namespace IPChangeNotifier.LiveStreamsCache.Data
{
    public interface ILiveStreamRepository
    {
        void AddOrUpdate(string streamerName, StreamData data);

        void UpdateTime(string streamerName);

        StreamRepositoryData Get(string streamerName);
    }
}
