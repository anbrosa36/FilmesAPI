﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public class ReadFilmeDTO
{
    public string Titulo { get; set; }

    public string Genero { get; set; }

    public int Duracao { get; set; }

    public int Ano { get; set; }

    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;

    public ICollection<ReadSessaoDTO> Sessoes { get; set; }
}
