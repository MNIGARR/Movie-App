using System.ComponentModel.DataAnnotations;

namespace MovieApp.API.DTOs;

public class CreateMovieDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string Overview { get; set; } = string.Empty;

    public string PosterPath { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public double VoteAverage { get; set; }

    public double Popularity { get; set; }
}
