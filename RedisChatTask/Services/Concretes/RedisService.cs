using Pipelines.Sockets.Unofficial;
using RedisChatTask.Services.Abstracts;
using StackExchange.Redis;

namespace RedisChatTask.Services.Concretes;

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task AddMessageToChannelAsync(string channelName, string message)
    {
        if (!string.IsNullOrWhiteSpace(channelName) && !string.IsNullOrWhiteSpace(message))
        {
            var db = _redis.GetDatabase();
            await db.ListRightPushAsync($"channel:{channelName}:messages", message);
        }
    }

    public async Task CreateChannelAsync(string channelName)
    {
        if (!string.IsNullOrWhiteSpace(channelName))
        {
            var db = _redis.GetDatabase();
            await db.SetAddAsync("channels", channelName);
        }
    }

    public async Task<List<string>> GetAllChannelAsync()
    {
        var db = _redis.GetDatabase();
        var channels = await db.SetMembersAsync("channels");
        return  channels.Select(x => x.ToString()).ToList();
        
    }

    public async Task<List<string>> GetChannelMessageAsync(string channelName)
    {
        var db = _redis.GetDatabase();
        var messages = await db.ListRangeAsync($"channel:{channelName}:messages");
        return messages.Select(m => m.ToString()).ToList();
    }
}
