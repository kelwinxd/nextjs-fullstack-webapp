using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mywebapi.Models;

[Table("Categorias")]
public class Categoria
{

    [Key]
    public int CategoriaId {get;set;}

    [Required]
    [StringLength(80)]
    public string? Name {get;set;}

    [Required]
    [StringLength(300)]
    public string? ImgUrl {get;set;}
    public ICollection<Produto>? Produtos {get;set;}
    

}