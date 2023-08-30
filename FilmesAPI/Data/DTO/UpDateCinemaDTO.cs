using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO
{
    public class UpDateCinemaDTO
    {
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        public string Nome { get; set; }
    }
}
