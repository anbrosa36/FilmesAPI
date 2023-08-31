using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class CinemaProfiles : Profile
    {
        public CinemaProfiles()
        {
            CreateMap<CreateCinemaDTO, Cinema>();
            CreateMap<UpDateCinemaDTO, Cinema>();


            CreateMap<Cinema,ReadCinemaDTO >()
                .ForMember(cinemaDto => cinemaDto.Endereco,
                opt=>opt.MapFrom(cinema => cinema.Endereco))
                .ForMember(cinemaDto => cinemaDto.Sessoes,
                opt => opt.MapFrom(cinema => cinema.Sessoes)); 
        }
        
    }
}
