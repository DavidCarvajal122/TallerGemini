using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TallerGemini.Data;
using TallerGemini.Interfaces;
using TallerGemini.Models;

namespace TallerGemini.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatBotServices _chatBotServices;
        private readonly AppDbContext _context; 


        public HomeController(ILogger<HomeController> logger, IChatBotServices chatbotServices, AppDbContext context)
        {
            _logger = logger;
            _chatBotServices = chatbotServices;
            _context = context;
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
            var respuestaChat = new RespuestaChat
            {
                Respuesta = respuesta,
                Fecha = DateTime.Now,
                Proveedor = modelo,
                GuardadoPor = User.Identity?.Name ?? "Anónimo"
            };

            _context.Respuestas.Add(respuestaChat);
            await _context.SaveChangesAsync();


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
