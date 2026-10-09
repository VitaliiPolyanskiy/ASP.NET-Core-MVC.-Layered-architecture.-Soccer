using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Soccer.BLL.DTO;
using Soccer.BLL.Interfaces;
using Soccer.BLL.Infrastructure;
using Soccer.Models;

namespace Soccer.Controllers;

// Використання первинного конструктора для інжекції сервісів
public class PlayersController(IPlayerService playerService, ITeamService teamService) : Controller
{
    public async Task<IActionResult> Index(string? position, int team = 0, int page = 1,
        SortState sortOrder = SortState.NameAsc)
    {
        var players = await playerService.GetPlayers();
        int pageSize = 5;

        // Фільтрація
        if (team != 0)
        {
            players = players.Where(p => p.TeamId == team);
        }
        if (!string.IsNullOrEmpty(position))
        {
            players = players.Where(p => p.Position == position);
        }

        // Сортування
        players = sortOrder switch
        {
            SortState.NameDesc => players.OrderByDescending(s => s.Name),
            SortState.AgeAsc => players.OrderBy(s => DateTime.Now.Year - s.BirthYear),
            SortState.AgeDesc => players.OrderByDescending(s => DateTime.Now.Year - s.BirthYear),
            SortState.PositionAsc => players.OrderBy(s => s.Position),
            SortState.PositionDesc => players.OrderByDescending(s => s.Position),
            SortState.TeamAsc => players.OrderBy(s => s.Team),
            SortState.TeamDesc => players.OrderByDescending(s => s.Team),
            _ => players.OrderBy(s => s.Name),
        };

        // Пагінація
        var list = players.ToList();
        var count = list.Count;
        var items = list.Skip((page - 1) * pageSize).Take(pageSize);

        // Формуємо модель представлення
        var teamlist = await teamService.GetTeams();
        var viewModel = new IndexViewModel(
            items,
            new PageViewModel(count, page, pageSize),
            new FilterViewModel(teamlist.ToList(), team, position ?? string.Empty),
            new SortViewModel(sortOrder)
        );

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        try
        {
            if (id is null) return NotFound();

            PlayerDTO player = await playerService.GetPlayer(id.Value);
            return View(player);
        }
        catch (ValidationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.ListTeams = new SelectList(await teamService.GetTeams(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PlayerDTO player)
    {
        if (ModelState.IsValid)
        {
            await playerService.CreatePlayer(player);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.ListTeams = new SelectList(await teamService.GetTeams(), "Id", "Name", player.TeamId);
        return View(player);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        try
        {
            if (id is null) return NotFound();

            PlayerDTO player = await playerService.GetPlayer(id.Value);
            ViewBag.ListTeams = new SelectList(await teamService.GetTeams(), "Id", "Name", player.TeamId);
            return View(player);
        }
        catch (ValidationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PlayerDTO player)
    {
        if (ModelState.IsValid)
        {
            await playerService.UpdatePlayer(player);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.ListTeams = new SelectList(await teamService.GetTeams(), "Id", "Name", player.TeamId);
        return View(player);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        try
        {
            if (id is null) return NotFound();

            PlayerDTO player = await playerService.GetPlayer(id.Value);
            return View(player);
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
        await playerService.DeletePlayer(id);
        return RedirectToAction(nameof(Index));
    }
}