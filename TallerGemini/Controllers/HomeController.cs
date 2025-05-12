using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TallerGemini.Interfaces;
using TallerGemini.Models;
using TallerGemini.Repositories;

namespace TallerGemini.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IChatBotServices _chatBotServices;

    public HomeController(ILogger<HomeController> logger, IChatBotServices chatbotServices)
    {
        _logger = logger;
        _chatBotServices = chatbotServices;
    }

    public async Task <IActionResult> Index()
    {
        GeminiRepository repo = new GeminiRepository();
        string answer = await _chatBotServices.GetChatResponse("Dame un resumen de la película Shrek"); 
        return View(answer);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
