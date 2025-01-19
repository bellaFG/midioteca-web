using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MidiotecaWeb.Data;
using MidiotecaWeb.Models;

namespace MidiotecaWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do contexto de dados com MySQL
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39")));

            // Configuração do ASP.NET Core Identity (alteração para IdentityRole caso queira usar roles no futuro)
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configuração dos serviços para a aplicação
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configuração do pipeline de requisições
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Configuração das rotas de controle
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Configuração do middleware de autenticação
            app.UseAuthentication(); // Importante para autenticação de usuários
            app.UseAuthorization(); // Importante para autorização de usuários

            app.Run();
        }
    }
}
