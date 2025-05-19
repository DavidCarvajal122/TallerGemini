using System;

namespace TallerGemini.Models
{
    public class RespuestaChat
    {
        public int Id { get; set; }
        public string Respuesta { get; set; }
        public DateTime Fecha { get; set; }
        public string Proveedor { get; set; }
        public string GuardadoPor { get; set; }
    }
}
