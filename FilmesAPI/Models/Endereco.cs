using System.ComponentModel.DataAnnotations;


namespace FilmesAPI.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O Logradouro é Obrigatório")]
    public string Logradouro { get; set; }
    
    public int Numero { get; set; }

    //relacionamento com Cinema
    public virtual Cinema Cinema { get; set; }
}
