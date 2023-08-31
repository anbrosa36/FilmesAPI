using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Filme
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage ="O Titulo do Filme é Obrigatório")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O Titulo do Genero é Obrigatório")]
    [MaxLength(50,ErrorMessage = "O tamanho do genero não pode exceder a 50 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "O campo duração é obrigatório")]
    [Range(70,600,ErrorMessage ="A duração dever ter entre 70 e 600 minutos")]
    public int Duracao { get; set; }

    [Required(ErrorMessage = "O Campo ano é obrigatório")]
    public int Ano { get; set; }

    public virtual ICollection<Sessao> Sessoes { get; set; }

}
