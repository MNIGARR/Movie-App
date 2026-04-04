namespace MovieApp.API.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string PosterPath { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public double VoteAverage { get; set; }
    public double Popularity { get; set; }

    public ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
