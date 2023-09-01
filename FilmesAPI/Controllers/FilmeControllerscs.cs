using AutoMapper;
using Azure;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;


[ApiController]
[Route("filme/")]
public class FilmeControllerscs : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeControllerscs(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona filme ao Banco de dados SQLServer
    /// </summary>
    /// <param name="filmeDto"> Objeto com os campos necessário para a criação de um filme</param>
    /// <returns>IActionReulte</returns>
    /// <response code="201">Caso a inserção seja realizada com sucesso</response>
    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDTO filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RetornaFilmePorId), new { id = filme.Id }, filme);
    }

    /// <summary>
    /// Retorna uma quantidade especifica de filmes conforme o take
    /// </summary>
    /// <param name="skip"> Parametro para estipular a partir de qual registro retornar os filmes</param>
    /// <param name="take">Parametro que determina a quantoidade de registros a serem retornados</param>
    /// <returns>IEnumerable de ReadFilmeDTO</returns>
    [HttpGet]
    public IEnumerable<ReadFilmeDTO> RetornaFilmes(
        [FromQuery] int skip = 0, [FromQuery] int take = 50, [FromQuery] string? nomeCinema = null)
    {

        if (nomeCinema == null)
        {
            return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes.Skip(skip).Take(take).ToList());
        }

        return _mapper.Map<List<ReadFilmeDTO>>(_context.Filmes
            .Skip(skip).Take(take).Where(filme => filme.Sessoes
            .Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());
   


}


    /// <summary>
    /// Retorna um filme especifico com base no id informado
    /// </summary>
    /// <param name="id">Id do registro do filme a ser retornado</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]
    public IActionResult RetornaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id.Equals(id));

        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDTO>(filme);
        return Ok(filmeDto);
    }


    /// <summary>
    /// Atualiza todos os campos do registro informado mesmo que não seja especificado
    /// </summary>
    /// <param name="id">Id do registro a ser atualizado na base</param>
    /// <param name="filmeDTO">Contem todos os campos a serem atualizados</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpDateFilmeDTO filmeDTO)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id.Equals(id));
        if (filme == null) return NotFound();
        _mapper.Map(filmeDTO,filme);
        _context.SaveChanges();
        return NoContent(); 
    }

    /// <summary>
    /// Atualiza os campos especificados com base no id 
    /// </summary>
    /// <param name="id">Identifica o registro a ser atualizado</param>
    /// <param name="patch">Identifica os campos ou campo a ser atualizado</param>
    /// <returns>IActionResult</returns>
    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcial(int id,
        JsonPatchDocument<UpDateFilmeDTO> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id.Equals(id));
        if (filme == null) return NotFound();

        var filmaParaAtualizar = _mapper.Map<UpDateFilmeDTO>(filme);
        patch.ApplyTo(filmaParaAtualizar, ModelState);
        if (!TryValidateModel(filmaParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmaParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Exclui um registro conforme o Id informado
    /// </summary>
    /// <param name="id">Identificação do registro a serm excluido</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id.Equals(id));
        if (filme == null) return NotFound();
        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
