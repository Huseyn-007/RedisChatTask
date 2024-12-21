namespace RedisChatTask.Services.Abstracts;

public interface IRedisService
{
    Task CreateChannelAsync(string channelName);
    Task<List<string>> GetAllChannelAsync();
    Task AddMessageToChannelAsync(string channelName, string message);
    Task<List<string>> GetChannelMessageAsync(string channelName);
}
