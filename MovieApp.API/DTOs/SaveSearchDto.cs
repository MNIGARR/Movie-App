namespace MovieApp.API.DTOs;

public class SaveSearchDto
{
    public string Query { get; set; } = string.Empty;
    public string Type { get; set; } = "movie";
}
