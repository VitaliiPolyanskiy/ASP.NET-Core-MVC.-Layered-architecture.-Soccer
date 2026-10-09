using Soccer.BLL.DTO;
using Soccer.DAL.Entities;
using Soccer.DAL.Interfaces;
using Soccer.BLL.Infrastructure;
using Soccer.BLL.Interfaces;
using AutoMapper;

namespace Soccer.BLL.Services;

public class PlayerService(IUnitOfWork database, IMapper mapper) : IPlayerService
{
    public async Task CreatePlayer(PlayerDTO playerDto)
    {
        var player = new Player
        {
            Id = playerDto.Id,
            Name = playerDto.Name,
            BirthYear = playerDto.BirthYear,
            Position = playerDto.Position,
            TeamId = playerDto.TeamId
        };
        await database.Players.Create(player);
        await database.Save();
    }

    public async Task UpdatePlayer(PlayerDTO playerDto)
    {
        var player = new Player
        {
            Id = playerDto.Id,
            Name = playerDto.Name,
            BirthYear = playerDto.BirthYear,
            Position = playerDto.Position,
            TeamId = playerDto.TeamId
        };
        database.Players.Update(player);
        await database.Save();
    }

    public async Task DeletePlayer(int id)
    {
        await database.Players.Delete(id);
        await database.Save();
    }

    public async Task<PlayerDTO> GetPlayer(int id)
    {
        var player = await database.Players.Get(id);
        if (player == null)
            throw new ValidationException("Дані відсутні!", nameof(id));

        return mapper.Map<PlayerDTO>(player);
    }

    public async Task<IEnumerable<PlayerDTO>> GetPlayers()
    {
        var players = await database.Players.GetAll();
        return mapper.Map<IEnumerable<PlayerDTO>>(players);
    }
}