using Microsoft.AspNetCore.Mvc;
using RedisChatTask.Models;
using RedisChatTask.Services.Abstracts;
using System.Diagnostics;

namespace RedisChatTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRedisService _redisService;

        public HomeController(IRedisService redisChannelService)
        {
            _redisService = redisChannelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string selectedChannel = null)
        {
            var viewModel = new ChatViewModel
            {
                Channels = await _redisService.GetAllChannelAsync()
            };

            if (!string.IsNullOrEmpty(selectedChannel))
            {
                viewModel.SelectedChannelName = selectedChannel;
                viewModel.SelectedChannelMessages = await _redisService.GetChannelMessageAsync(selectedChannel);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannel(string channelName)
        {
            if (!string.IsNullOrWhiteSpace(channelName))
            {
                await _redisService.CreateChannelAsync(channelName);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SelectChannel(string channelName)
        {
            if (!string.IsNullOrWhiteSpace(channelName))
            {
                return RedirectToAction("Index", new { selectedChannel = channelName });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string channelName, string message)
        {
            if (!string.IsNullOrWhiteSpace(channelName) && !string.IsNullOrWhiteSpace(message))
            {
                await _redisService.AddMessageToChannelAsync(channelName, message);
            }

            return RedirectToAction("Index", new { selectedChannel = channelName });
        }
    }

}

