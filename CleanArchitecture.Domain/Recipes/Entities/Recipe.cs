namespace CleanArchitecture.Domain.Recipes.Entities;

public record Recipe
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int PrepTime { get; set; }
    public required int CookTime { get; set; }
}