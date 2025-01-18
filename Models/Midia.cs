namespace MidiotecaWeb.Models
{
    public class Midia
    {
        public int Id { get; set; }
        public string Tipo { get; set; }  // Tipo da mídia (imagem, vídeo, áudio)
        public string Url { get; set; }  // Caminho/URL para o arquivo de mídia
        public DateTime DataCriacao { get; set; }

        // Relacionamento com a postagem (se a mídia for relacionada a uma postagem específica)
        public int? PostagemId { get; set; }
        public Postagem Postagem { get; set; }
    }
}
