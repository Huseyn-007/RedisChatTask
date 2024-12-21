using Microsoft.AspNetCore.Mvc;
using RedisChatTask.Models;
using RedisChatTask.Services.Abstracts;
using System.Diagnostics;

namespace RedisChatTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRedisService _redisService;

        public HomeController(IRedisService redisService)
        {
            _redisService = redisService;
        }



        [HttpGet]
        public async Task<IActionResult> Index(string selectedChannel = " ")
        {
            var channels = _redisService.GetAllChannelAsync();
            if (!string.IsNullOrWhiteSpace(selectedChannel))
            {
                var messages = await _redisService.GetChannelMessageAsync(selectedChannel);
                var vm = new ChatViewModel
                {
                    SelectedChannelMessages = messages,
                    SelectedChannelName = selectedChannel,
                };
                return View(vm);
            }
            return View();
        }
    }
}
