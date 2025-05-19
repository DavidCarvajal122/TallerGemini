using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TallerGemini.Interfaces;
using TallerGemini.Models;

namespace TallerGemini.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatBotServices _chatBotServices;

        public HomeController(ILogger<HomeController> logger, IChatBotServices chatbotServices)
        {
            _logger = logger;
            _chatBotServices = chatbotServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string mensaje, string modelo)
        {
            string respuesta;

            if (modelo == "Gemini")
            {
                respuesta = await _chatBotServices.GetGeminiResponse(mensaje);
            }
            else if (modelo == "Groq")
            {
                respuesta = await _chatBotServices.GetGroqResponse(mensaje);
            }
            else
            {
                respuesta = "Modelo no válido.";
            }

            ViewBag.Respuesta = respuesta;
            return View();
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
}
