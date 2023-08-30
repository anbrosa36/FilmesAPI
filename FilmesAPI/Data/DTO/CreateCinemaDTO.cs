using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO
{
    public class CreateCinemaDTO
    {
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        public string Nome { get; set; }

        public int EnderecoId { get; set; }
    }
}
