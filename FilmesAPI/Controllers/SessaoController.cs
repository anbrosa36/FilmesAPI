using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("sessao/")]
    public class SessaoController : Controller
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public SessaoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        /// <summary>
        /// Adiciona uma Sessao ao Banco de dados SQLServer
        /// </summary>
        /// <param name="sessaoDto"> Objeto com os campos necessário para a criação de uma Sessao</param>
        /// <returns>IActionReulte</returns>
        /// <response code="201">Caso a inserção seja realizada com sucesso</response>
        [HttpPost]
        public IActionResult AdiconarSessao([FromBody] CreateSessaoDTO sessaoDTO)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDTO);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarSessaoPorId)
                , new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
        }

        /// <summary>
        /// Retorna uma quantidade especifica de sessoes conforme o take
        /// </summary>
        /// <param name="skip"> Parametro para estipular a partir de qual registro retornar as Sessoes</param>
        /// <param name="take">Parametro que determina a quantidade de registros a serem retornados</param>
        /// <returns>IEnumerable de ReadCinemaDTO</returns>
        [HttpGet]
        public IEnumerable<ReadSessaoDTO> RetornaSessoes()
        {
            return _mapper.Map<List<ReadSessaoDTO>>(_context.Sessoes.ToList());
        }

        /// <summary>
        /// Retorna uma sessao especifica com base no id informado
        /// </summary>
        /// <param name="id">Id do registro da sessao a ser retornado</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{cinemaId}/{filmeId}")]
        public IActionResult RecuperarSessaoPorId(int cinemaId,int filmeId )
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(
                sessao => sessao.CinemaId == cinemaId && sessao.FilmeId == filmeId);

            if (sessao == null) return NotFound();
            ReadSessaoDTO sessaoDto = _mapper.Map<ReadSessaoDTO>(sessao);
            return Ok(sessaoDto);
        }

    }
}
