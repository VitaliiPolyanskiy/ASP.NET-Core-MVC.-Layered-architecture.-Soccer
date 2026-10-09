using Microsoft.EntityFrameworkCore;
using Soccer.DAL.Entities;
using Soccer.DAL.Interfaces;
using Soccer.DAL.EF;

namespace Soccer.DAL.Repositories;

public class TeamRepository(SoccerContext db) : IRepository<Team>
{
    public async Task<IEnumerable<Team>> GetAll()
    {
        return await db.Teams.ToListAsync();
    }

    public async Task<Team?> Get(int id)
    {
        return await db.Teams.FindAsync(id);
    }

    public async Task<Team?> Get(string name)
    {
        return await db.Teams.FirstOrDefaultAsync(a => a.Name == name);
    }

    public async Task Create(Team team) => await db.Teams.AddAsync(team);

    public void Update(Team team) => db.Entry(team).State = EntityState.Modified;

    public async Task Delete(int id)
    {
        Team? team = await db.Teams.FindAsync(id);
        if (team != null)
        {
            db.Teams.Remove(team);
        }
    }
}