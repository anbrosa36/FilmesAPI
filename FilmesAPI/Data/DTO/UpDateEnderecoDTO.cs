﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public class UpDateEnderecoDTO
{
    [Required(ErrorMessage = "O Logradouro é Obrigatório")]
    public string Logradouro { get; set; }

    public int Numero { get; set; }
}
