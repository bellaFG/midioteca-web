using System; // Para DateTime e tipos básicos
using System.Collections.Generic; // Para ICollection<T>
using Microsoft.AspNetCore.Identity; // Para ApplicationUser (se estiver usando Identity)
using System.ComponentModel.DataAnnotations; // Para [Key] e [Required]

namespace MidiotecaWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(255)] // Ajusta o comprimento do nome completo
        public string NomeCompleto { get; set; }

        [StringLength(255)] // Ajusta o comprimento do caminho para a foto de perfil
        public string FotoPerfil { get; set; }
    }
}
