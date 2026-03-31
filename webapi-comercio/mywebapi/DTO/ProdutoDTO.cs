namespace mywebapi.DTO;

public class ProdutoPostDTO
{
    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }

    public int CategoriaId { get; set; }
}