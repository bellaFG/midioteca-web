using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MidiotecaWeb.Data;
using MidiotecaWeb.Models;

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

// Configuração do cookie de autenticação
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Configuração dos serviços para a aplicação
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Adicionar suporte ao Identity
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configuração do middleware para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Configuração do pipeline de requisições
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Configuração do middleware de autenticação e autorização
app.UseAuthentication(); // Certifique-se de que este middleware vem antes de UseAuthorization()
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
