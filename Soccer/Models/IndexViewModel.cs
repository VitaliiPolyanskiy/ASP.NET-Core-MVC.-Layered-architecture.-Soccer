using Soccer.BLL.DTO;

namespace Soccer.Models;

// Використання первинного конструктора (C# 12) для лаконічної ініціалізації
public class IndexViewModel(
    IEnumerable<PlayerDTO> players,
    PageViewModel pageViewModel,
    FilterViewModel filterViewModel,
    SortViewModel sortViewModel)
{
    public IEnumerable<PlayerDTO> Players { get; } = players;
    public PageViewModel PageViewModel { get; } = pageViewModel;
    public FilterViewModel FilterViewModel { get; } = filterViewModel;
    public SortViewModel SortViewModel { get; } = sortViewModel;
}