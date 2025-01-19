using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MidiotecaWeb.Models
{
    public class Postagem
    {
        public int Id { get; set; }


        [StringLength(255)]
        public string Titulo { get; set; }

        [StringLength(4000)]
        public string Conteudo { get; set; }

        [StringLength(255)]
        public string ImagemUrl { get; set; }

        public DateTime DataCriacao { get; set; }


        [StringLength(50)]
        public string Tipo { get; set; }

        public string AutorId { get; set; }
        public ApplicationUser Autor { get; set; }

        public ICollection<Comentario> Comentarios { get; set; }
    }
}
