using System.ComponentModel.DataAnnotations;

namespace MidiotecaWeb.ViewModels
{
    public class CadastroViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }

        [Required]
        [Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string NomeCompleto { get; set; }
    }
}
