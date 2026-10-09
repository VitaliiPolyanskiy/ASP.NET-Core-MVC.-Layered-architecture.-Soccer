namespace Soccer.DAL.Entities;

public class Player
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int BirthYear { get; set; }
    public required string Position { get; set; }
    public int? TeamId { get; set; }
    public Team? Team { get; set; }
}