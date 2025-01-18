using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MidiotecaWeb.Data;  // Seu DbContext
using MidiotecaWeb.Models; // Seu ApplicationUser

namespace MidiotecaWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura o banco de dados usando a string de conexão definida no appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39")));

            // Configura o Identity com ApplicationUser
            builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Configura o pipeline de requisições HTTP
            var app = builder.Build();

            // Força HTTPS
            app.UseHttpsRedirection();
            app.UseStaticFiles();  // Para servir arquivos estáticos como CSS, JS, imagens

            // Configura as rotas
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
