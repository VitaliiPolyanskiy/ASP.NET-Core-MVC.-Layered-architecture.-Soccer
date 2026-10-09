namespace Soccer.Models;

public class SortViewModel
{
    public SortState NameSort { get; }     // Значення для сортування за ім'ям
    public SortState AgeSort { get; }      // Значення для сортування за віком
    public SortState PositionSort { get; } // Значення для сортування за позицією
    public SortState TeamSort { get; }     // Значення для сортування за командою
    public SortState Current { get; }      // Поточне вибране значення для сортування
    public bool Up { get; }                // Сортування за зростанням чи спаданням

    public SortViewModel(SortState sortOrder)
    {
        NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
        AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
        PositionSort = sortOrder == SortState.PositionAsc ? SortState.PositionDesc : SortState.PositionAsc;
        TeamSort = sortOrder == SortState.TeamAsc ? SortState.TeamDesc : SortState.TeamAsc;
        Current = sortOrder;
    }
}