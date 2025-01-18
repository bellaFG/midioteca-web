namespace MidiotecaWeb.Models
{

    public class Comentario
    {
        public int Id { get; set; }  // Chave primária
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }

        // Relacionamento com a postagem
        public int PostagemId { get; set; }
        public Postagem Postagem { get; set; }

        // Relacionamento com o usuário (comentário feito por um usuário)
        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
