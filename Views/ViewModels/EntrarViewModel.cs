using System.ComponentModel.DataAnnotations;

namespace MidiotecaWeb.ViewModels
{
    public class EntrarViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}
