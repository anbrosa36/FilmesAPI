using System.Reflection.Metadata.Ecma335;

namespace FilmesAPI.Data.DTO
{
    public class CreateSessaoDTO
    {
        public int FilmeId { get; set; }
        public int CinemaId { get; set; }
    }
}
