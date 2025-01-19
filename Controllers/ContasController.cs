using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MidiotecaWeb.Models;
using MidiotecaWeb.ViewModels;
using System.Threading.Tasks;

namespace MidiotecaWeb.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; // Renomeando para _userManager
        private readonly SignInManager<ApplicationUser> _signInManager; // Renomeando para _signInManager

        // Construtor com injeção de dependência
        public ContaController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager; // Usando o nome mais claro para o gerenciador de usuários
            _signInManager = signInManager; // Usando o nome mais claro para o gerenciador de login
        }

        // Exibe a página de registro (GET)
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        // Processa o registro de um novo usuário (POST)
        [HttpPost]
        public async Task<IActionResult> Registrar(CadastroViewModel model) // Alterando 'modelo' para 'model'
        {
            if (ModelState.IsValid)
            {
                // Criando o usuário com o nome completo incluído
                var user = new ApplicationUser
                {
                    UserName = model.Email, // Usando 'model' para manter consistência
                    Email = model.Email,
                    NomeCompleto = model.NomeCompleto // Incluindo o nome completo
                };

                var result = await _userManager.CreateAsync(user, model.Senha); // Usando 'result' em vez de 'resultado'

                if (result.Succeeded)
                {
                    // Faz o login do usuário após o registro
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); // Redireciona para a página inicial após o cadastro
                }

                // Se o cadastro falhar, exibe as mensagens de erro
                foreach (var error in result.Errors) // Usando 'error' para seguir a convenção
                {
                    ModelState.AddModelError(string.Empty, error.Description); // Adicionando as mensagens de erro ao ModelState
                }
            }
            return View(model); // Retorna a view com os dados do model (caso haja erro)
        }

        // Exibe a página de login (GET)
        [HttpGet]
        public IActionResult Entrar()
        {
            return View();
        }

        // Processa o login de um usuário existente (POST)
        [HttpPost]
        public async Task<IActionResult> Entrar(EntrarViewModel model) // Alterando 'modelo' para 'model'
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email); // Usando 'user' para consistência
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Senha, model.LembrarMe, false); // Usando 'result' aqui também

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home"); // Redireciona para a página inicial após o login
                    }
                    ModelState.AddModelError(string.Empty, "Falha ao fazer login. Usuário ou senha incorretos."); // Mensagem de erro caso falhe
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário não encontrado.");
                }
            }
            return View(model); // Retorna a view com os dados do model (caso haja erro)
        }

        // Processa o logout do usuário
        [HttpPost]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync(); // Chama o método de logout
            return RedirectToAction("Index", "Home"); // Redireciona para a página inicial após o logout
        }
    }
}
