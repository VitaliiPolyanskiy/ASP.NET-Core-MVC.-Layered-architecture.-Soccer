using AutoMapper;
using Soccer.BLL.DTO;
using Soccer.DAL.Entities;

namespace Soccer.BLL.Profiles;

public class SoccerProfile : Profile
{
    public SoccerProfile()
    {
        CreateMap<Team, TeamDTO>();

        CreateMap<Player, PlayerDTO>()
            .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team != null ? src.Team.Name : null));
    }
}