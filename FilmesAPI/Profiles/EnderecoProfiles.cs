using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class EnderecoProfiles : Profile
    {
        public EnderecoProfiles()
        {
            CreateMap<CreateEnderecoDTO, Endereco>();
            CreateMap<UpDateEnderecoDTO, Endereco>();
            CreateMap<Endereco,ReadEnderecoDTO > ();
        }
            
    }
}
