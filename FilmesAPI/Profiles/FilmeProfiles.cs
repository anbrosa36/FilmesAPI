using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class FilmeProfiles : Profile
{
    public FilmeProfiles()
    {
        CreateMap<CreateFilmeDTO, Filme>();
        CreateMap<UpDateFilmeDTO, Filme>();
        CreateMap<Filme, UpDateFilmeDTO>();
        CreateMap<Filme, ReadFilmeDTO>()
            .ForMember(filmeDto => filmeDto.Sessoes,
                opt => opt.MapFrom(filme => filme.Sessoes)); ;

    }

}
