using Soccer.BLL.DTO;

namespace Soccer.BLL.Interfaces;

public interface IPlayerService
{
    Task CreatePlayer(PlayerDTO playerDto);
    Task UpdatePlayer(PlayerDTO playerDto);
    Task DeletePlayer(int id);
    Task<PlayerDTO> GetPlayer(int id);
    Task<IEnumerable<PlayerDTO>> GetPlayers();
}