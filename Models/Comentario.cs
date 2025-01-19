using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace MidiotecaWeb.Models

{
    public class Comentario
    {
        public int Id { get; set; }

        [StringLength(1000)]
        public string Texto { get; set; }

        public DateTime DataCriacao { get; set; }


        public int PostagemId { get; set; }
        public Postagem Postagem { get; set; }

        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
