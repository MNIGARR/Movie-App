namespace MovieApp.API.DTOs;

public class WatchlistDto
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public string MoviePosterPath { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
