using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MidiotecaWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(255)]
        public string NomeCompleto { get; set; }

        [StringLength(255)]
        public string FotoPerfil { get; set; }
    }
}
