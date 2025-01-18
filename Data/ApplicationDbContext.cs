using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  // Para IdentityDbContext
using Microsoft.EntityFrameworkCore;
using MidiotecaWeb.Models;

namespace MidiotecaWeb.Data
{
    // Alterando para herdar de IdentityDbContext<ApplicationUser> para integração com Identity
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets para as entidades do banco de dados
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Midia> Midias { get; set; } // Renomeado para evitar acentos
    }
}
