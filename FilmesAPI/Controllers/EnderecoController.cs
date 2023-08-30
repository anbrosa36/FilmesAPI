using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("endereco/")]
    public class EnderecoController : Controller
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona Endereco ao Banco de dados SQLServer
        /// </summary>
        /// <param name="enderecoDTO"> Objeto com os campos necessário para a criação de um Endereco</param>
        /// <returns>IActionReulte</returns>
        /// <response code="201">Caso a inserção seja realizada com sucesso</response>
        [HttpPost]
        public IActionResult AdiconarEndereco([FromBody] CreateEnderecoDTO enderecoDTO)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDTO);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarEnderecoPorId), new { Id = endereco.Id }, enderecoDTO);
        }

        /// <summary>
        /// Retorna uma quantidade especifica de Endereços conforme o take
        /// </summary>
        /// <param name="skip"> Parametro para estipular a partir de qual registro retornar os endereçoc</param>
        /// <param name="take">Parametro que determina a quantidade de registros a serem retornados</param>
        /// <returns>IEnumerable de ReadEnderecoDTO</returns>
        [HttpGet]
        public IEnumerable<ReadEnderecoDTO> RetornaEnderecos()
        {
            return _mapper.Map<List<ReadEnderecoDTO>>(_context.Enderecos.ToList());
        }

        /// <summary>
        /// Retorna um endereco especifico com base no id informado
        /// </summary>
        /// <param name="id">Id do registro do endereco a ser retornado</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public IActionResult RecuperarEnderecoPorId(Guid id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id.Equals(id));

            if (endereco == null) return NotFound();
            ReadEnderecoDTO enderecoDto = _mapper.Map<ReadEnderecoDTO>(endereco);
            return Ok(enderecoDto);
        }

        /// <summary>
        /// Atualiza todos os campos do registro informado mesmo que não seja especificado
        /// </summary>
        /// <param name="id">Id do registro a ser atualizado na base</param>
        /// <param name="enderecoDto">Contem todos os campos a serem atualizados</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult AtualizarEndereco(Guid id, [FromBody] UpDateEnderecoDTO enderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(end => end.Id.Equals(id));
            if (endereco == null) return NotFound();
            _mapper.Map(enderecoDto, endereco);                
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Exclui um registro conforme o Id informado
        /// </summary>
        /// <param name="id">Identificação do registro a serm excluido</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletarEndereco(Guid id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(end => end.Id.Equals(id));
            if (endereco == null) return NotFound();
            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
