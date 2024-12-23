using System.Security.Cryptography;

namespace RedisChatTask.Models;

public class ChatViewModel
{
    public ICollection<string> Channels { get; set; }

    public string SelectedChannelName { get; set; }
    public List<string> SelectedChannelMessages { get; set; }
}
