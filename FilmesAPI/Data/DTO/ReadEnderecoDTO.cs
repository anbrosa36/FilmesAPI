using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public class ReadEnderecoDTO
{
    public string Logradouro { get; set; }

    public int Numero { get; set; }
}
