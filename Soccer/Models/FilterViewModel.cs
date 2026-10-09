using Microsoft.AspNetCore.Mvc.Rendering;
using Soccer.BLL.DTO;

namespace Soccer.Models;

public class FilterViewModel
{
    public FilterViewModel(List<TeamDTO> teams, int team, string position)
    {
        // Встановлюємо початковий елемент, який дозволить обрати всіх
        teams.Insert(0, new TeamDTO { Name = "Усі", Id = 0, Coach = string.Empty });
        Teams = new SelectList(teams, "Id", "Name", team);
        SelectedTeam = team;
        SelectedPosition = position;
    }

    public SelectList Teams { get; } // Список клубів
    public int SelectedTeam { get; } // Обраний клуб
    public string SelectedPosition { get; } // Введена позиція
}