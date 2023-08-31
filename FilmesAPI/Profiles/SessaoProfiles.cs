using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfiles : Profile
    {
        public SessaoProfiles()
        {
            CreateMap<CreateSessaoDTO, Sessao>();
            CreateMap<Sessao, ReadSessaoDTO>();
        }
        
    }
}
