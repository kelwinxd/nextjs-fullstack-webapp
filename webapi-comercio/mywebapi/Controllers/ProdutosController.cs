using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mywebapi.Context;
using mywebapi.Models;
using mywebapi.DTO;

namespace mywebapi.Controllers;

[ApiController]
[Route("[controller]")]

public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
    _context = context;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _context.Produtos?.ToList();

        if(produtos is null)
        {
            return NotFound("Produtos não encontrados");
        }
        return produtos;

    }

    [HttpGet("{id:int:min(1)}")]

    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos?.FirstOrDefault(p => p.ProdutoId == id);
        if(produto == null)
        {
            return NotFound("Produto não encontrado!");
        }

        return produto;
    }


    [HttpPost]

    public ActionResult Post(ProdutoPostDTO produtoDTO)
    {
        if(produtoDTO is null)
        {
            return BadRequest();
        }

        var produto = new Produto
    {
        Nome = produtoDTO.Nome,
        Descricao = produtoDTO.Descricao,
        Preco = produtoDTO.Preco,
        ImagemUrl = produtoDTO.ImagemUrl,
        Estoque = produtoDTO.Estoque,
        CategoriaId = produtoDTO.CategoriaId,
        DataCadastro = DateTime.Now
    };
        _context.Produtos?.Add(produto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new {id = produto.ProdutoId}, produto);

    }



   [HttpPut("{id:int:min(1):maxlength(1000)}")]
    public ActionResult Put(int id, ProdutoPostDTO produto)
    {
    var produtoDb = _context.Produtos?.Find(id);

    if(produtoDb is null){
        return NotFound("Produto não encontrado!");
    }

    produtoDb.Nome = produto.Nome;
    produtoDb.Descricao = produto.Descricao;
    produtoDb.Preco = produto.Preco;
    produtoDb.ImagemUrl = produto.ImagemUrl;
    produtoDb.Estoque = produto.Estoque;
    produtoDb.CategoriaId = produto.CategoriaId;

    _context.SaveChanges();

    return Ok(produtoDb);
    }

    [HttpDelete("{id:int:min(1)}")]

    public ActionResult Delete(int id){
        var produtoDb = _context.Produtos?.FirstOrDefault(p => p.ProdutoId == id);

        if(produtoDb is null){
            return NotFound("produto não encontrado!");

        }

        _context.Produtos?.Remove(produtoDb);
        _context.SaveChanges();

        return NoContent();


    }


    /*

    [HttpGet]
    public IActionResult Get()
    {
        var Lista = _context.Produtos.ToList();
        return Ok(Lista);
    }



    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var prod = _context.Produtos.Find(id);

        if (prod == null)
        {
            return NotFound();
        }

        return Ok(prod);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Produto produto) 
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new {id = produto.ProdutoId}, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Produto newProduto)

    {
        
        if(id != newProduto.ProdutoId)
        {
            return BadRequest();
        }

        var produto = await _context.Produtos.FindAsync(id);
        if(produto == null)
        {
            return NotFound();
        }
        _context.Entry(produto).CurrentValues.SetValues(newProduto);

        await _context.SaveChangesAsync();

        return NoContent();
        
        
    }

    */
}
