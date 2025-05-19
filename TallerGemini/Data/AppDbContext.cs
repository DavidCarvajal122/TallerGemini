using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TallerGemini.Data;
using TallerGemini.Models;

namespace TallerGemini.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RespuestaChat> Respuestas { get; set; }
    }
}
