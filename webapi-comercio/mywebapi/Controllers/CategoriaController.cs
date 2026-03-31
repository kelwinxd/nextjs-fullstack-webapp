using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mywebapi.Context;
using mywebapi.Models;
using mywebapi.DTO;

namespace mywebapi.Controllers;

[ApiController]
[Route("[controller]")]

public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get() {

        try
        {
           var categorias = _context.Categorias?.AsNoTracking().ToList();

        if(categorias == null | !categorias.Any())
        {
            return NotFound("Categorias não encontradas");
        }

        throw new DataMisalignedException();
        return categorias; 
        }
        catch (System.Exception)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
        
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return _context.Categorias.Include((p) => p.Produtos).Where(c => c.CategoriaId <= 3).ToList();

    }

    [HttpGet("{id}")]

    public ActionResult<Categoria> Get(int id)
    {   
        var categoria = _context.Categorias?.FirstOrDefault(c => c.CategoriaId == id);

        if(categoria == null)
        {
            return NotFound("Produto Não Encontrado");
        }
        
        return Ok(categoria);
    }

    [HttpPost]

    public ActionResult Post(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO == null)
        {
            return BadRequest();
        }

        var categoria = new Categoria
        {
            Name = categoriaDTO.Name,
            ImgUrl = categoriaDTO.ImgUrl

        };

        _context.Categorias?.Add(categoria);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new {id = categoria.CategoriaId}, categoria);
    }

    [HttpPut("{id:int:min(1)}")]
     public ActionResult Put(int id, CategoriaDTO categoria)
    {
        var categoriaDb = _context.Categorias?.Find(id);

        if(categoriaDb is null)
        {
            return NotFound("Não encontrado!");
        }

        categoriaDb.Name = categoria.Name;
        categoriaDb.ImgUrl = categoria.ImgUrl;

        _context.SaveChanges();

        return Ok(categoriaDb);


    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var categoria = _context.Categorias?.Find(id);

        
        if(categoria is null){
            return NotFound("produto não encontrado!");

        }
        _context.Categorias?.Remove(categoria);
        _context.SaveChanges();

        return NoContent();

    }


}