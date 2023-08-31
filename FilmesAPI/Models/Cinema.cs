using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Cinema
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage ="Campo nome é obrigatório")]
    public string Nome { get; set; }

    //Relacionamento entre cinema e endereço
    public int EnderecoId{ get; set; }
    public virtual Endereco Endereco { get; set; }

    public virtual ICollection<Sessao> Sessoes { get; set; }
}
