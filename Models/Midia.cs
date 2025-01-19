using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MidiotecaWeb.Models
{
    public class Midia
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public DateTime DataCriacao { get; set; }


        public int? PostagemId { get; set; }
        public Postagem Postagem { get; set; }
    }
}
