using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TallerGemini.Models;
using TallerGemini.Repositories;

namespace TallerGemini.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task <IActionResult> Index()
    {
        GeminiRepository repo = new GeminiRepository();
        string answer = await repo.GetChatResponse("Dame un resumen"); 

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
