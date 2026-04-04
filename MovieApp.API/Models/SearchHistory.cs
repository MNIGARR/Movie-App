namespace MovieApp.API.Models;

public class SearchHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Query { get; set; } = string.Empty;
    public string Type { get; set; } = "movie";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}
