using Soccer.DAL.EF;
using Soccer.DAL.Interfaces;
using Soccer.DAL.Entities;

namespace Soccer.DAL.Repositories;

/*
 * Патерн Unit of Work дозволяє спростити роботу з різними репозиторіями 
 * та гарантує, що всі вони використовуватимуть спільний контекст даних 
 * для транзакційної цілісності.
 */
public class EFUnitOfWork(SoccerContext db) : IUnitOfWork
{
    private PlayerRepository? _playerRepository;
    private TeamRepository? _teamRepository;

    public IRepository<Team> Teams => _teamRepository ??= new TeamRepository(db);

    public IRepository<Player> Players => _playerRepository ??= new PlayerRepository(db);

    public async Task Save() => await db.SaveChangesAsync();
}