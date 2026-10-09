using Microsoft.EntityFrameworkCore;
using Soccer.DAL.Entities;
using Soccer.DAL.Interfaces;
using Soccer.DAL.EF;

namespace Soccer.DAL.Repositories;

public class PlayerRepository(SoccerContext db) : IRepository<Player>
{
    public async Task<IEnumerable<Player>> GetAll()
    {
        return await db.Players.Include(o => o.Team).ToListAsync();
    }

    public async Task<Player?> Get(int id)
    {
        return await db.Players
            .Include(o => o.Team)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Player?> Get(string name)
    {
        return await db.Players
            .FirstOrDefaultAsync(a => a.Name == name);
    }

    public async Task Create(Player player) => await db.Players.AddAsync(player);

    public void Update(Player player) => db.Entry(player).State = EntityState.Modified;

    public async Task Delete(int id)
    {
        Player? player = await db.Players.FindAsync(id);
        if (player != null)
        {
            db.Players.Remove(player);
        }
    }
}