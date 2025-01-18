using System; // Para DateTime e tipos básicos
using System.Collections.Generic; // Para ICollection<T>
using Microsoft.AspNetCore.Identity; // Para ApplicationUser (se estiver usando Identity)
using System.ComponentModel.DataAnnotations; // Para [Key] e [Required]

namespace MidiotecaWeb.Models
{

    public class Postagem
    {
        public int Id { get; set; }  // Chave primária
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string ImagemUrl { get; set; }  // Caso tenha upload de imagem
        public DateTime DataCriacao { get; set; }

        // Tipo de conteúdo, podendo ser 'Filme' ou 'Livro'
        public string Tipo { get; set; }

        // Relacionamento com o usuário (autor da postagem)
        public string AutorId { get; set; }
        public ApplicationUser Autor { get; set; }

        // Relacionamento com os comentários
        public ICollection<Comentario> Comentarios { get; set; }
    }
}
