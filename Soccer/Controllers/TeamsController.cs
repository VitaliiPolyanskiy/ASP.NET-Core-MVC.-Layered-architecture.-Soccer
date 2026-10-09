using Microsoft.AspNetCore.Mvc;
using Soccer.BLL.DTO;
using Soccer.BLL.Interfaces;
using Soccer.BLL.Infrastructure;

namespace Soccer.Controllers;

public class TeamsController(ITeamService teamService) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await teamService.GetTeams());
    }

    public async Task<IActionResult> Details(int? id)
    {
        try
        {
            if (id is null) return NotFound();

            TeamDTO team = await teamService.GetTeam(id.Value);
            return View(team);
        }
        catch (ValidationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TeamDTO team)
    {
        if (ModelState.IsValid)
        {
            await teamService.CreateTeam(team);
            // Використання патерну PRG (Post/Redirect/Get)[cite: 24]
            return RedirectToAction(nameof(Index));
        }
        return View(team);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        try
        {
            if (id is null) return NotFound();

            TeamDTO team = await teamService.GetTeam(id.Value);
            return View(team);
        }
        catch (ValidationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TeamDTO team)
    {
        if (ModelState.IsValid)
        {
            await teamService.UpdateTeam(team);
            return RedirectToAction(nameof(Index));
        }
        return View(team);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        try
        {
            if (id is null) return NotFound();

            TeamDTO team = await teamService.GetTeam(id.Value);
            return View(team);
        }
        catch (ValidationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await teamService.DeleteTeam(id);
        return RedirectToAction(nameof(Index));
    }
}