using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MidiotecaWeb.Models;
using MidiotecaWeb.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MidiotecaWeb.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ContaController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NomeCompleto = model.NomeCompleto
                };

                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("NomeCompleto", model.NomeCompleto));
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Entrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(EntrarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Senha, model.LembrarMe, false);

                    if (result.Succeeded)
                    {
                        await AtualizarClaims(user);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Falha ao fazer login. Usuário ou senha incorretos.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário não encontrado.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Perfil()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Entrar", "Conta");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Entrar", "Conta");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPerfil(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Entrar", "Conta");
                }

                user.NomeCompleto = model.NomeCompleto;
                user.Email = model.Email;
                user.FotoPerfil = model.FotoPerfil;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Perfil");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task AtualizarClaims(ApplicationUser user)
        {
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = false
            };

            await HttpContext.SignInAsync(
                IdentityConstants.ApplicationScheme,
                claimsPrincipal,
                authenticationProperties
            );
        }
    }
}
