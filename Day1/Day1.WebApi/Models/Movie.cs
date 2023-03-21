using System.ComponentModel.DataAnnotations;

public class Movie
{
    [Required]
    [MinLength(3), MaxLength(3)]
    public string Id { get; set; }
    public string Title { get; set; }
};