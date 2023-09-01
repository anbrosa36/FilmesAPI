using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("cinema/")]
public class CinemasController : Controller
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemasController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona Cinema ao Banco de dados SQLServer
    /// </summary>
    /// <param name="cinemaDto"> Objeto com os campos necessário para a criação de um Cinema</param>
    /// <returns>IActionReulte</returns>
    /// <response code="201">Caso a inserção seja realizada com sucesso</response>
    [HttpPost]
    public IActionResult AdiconarCinema([FromBody] CreateCinemaDTO cinemaDTO)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarCinemasPorId), new { Id = cinema.Id }, cinemaDTO);
    }

    /// <summary>
    /// Retorna uma quantidade especifica de Cinemas conforme o take
    /// </summary>
    /// <param name="skip"> Parametro para estipular a partir de qual registro retornar os cinemas</param>
    /// <param name="take">Parametro que determina a quantidade de registros a serem retornados</param>
    /// <returns>IEnumerable de ReadCinemaDTO</returns>
    [HttpGet]
    public IEnumerable<ReadCinemaDTO> RetornaCinemas([FromQuery] int? enderecoId = null)
    {
        if (enderecoId == null)
        {
            return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.ToList());
        }
        return _mapper.Map<List<ReadCinemaDTO>>(
            _context.Cinemas.FromSqlRaw($"Select Id, Nome, EnderecoId From Cimenas" +
            $"Where Cimenas.EnderecoId={enderecoId}").ToList());
        
    }

    /// <summary>
    /// Retorna um cinema especifico com base no id informado
    /// </summary>
    /// <param name="id">Id do registro do cinema a ser retornado</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]
    public IActionResult RecuperarCinemasPorId(Guid id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id.Equals(id));

        if (cinema == null) return NotFound();
        ReadCinemaDTO cinemaDto = _mapper.Map<ReadCinemaDTO>(cinema);
        return Ok(cinemaDto);
    }


    /// <summary>
    /// Atualiza todos os campos do registro informado mesmo que não seja especificado
    /// </summary>
    /// <param name="id">Id do registro a ser atualizado na base</param>
    /// <param name="cinemaDto">Contem todos os campos a serem atualizados</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{id}")]
    public IActionResult AtualizarCinema(Guid id, [FromBody] UpDateCinemaDTO cinemaDto)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id.Equals(id));
        if (cinema == null) return NotFound();
        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Exclui um registro conforme o Id informado
    /// </summary>
    /// <param name="id">Identificação do registro a serm excluido</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletarCinema(Guid id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(f => f.Id.Equals(id));
        if (cinema == null) return NotFound();
        _context.Cinemas.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }

}
