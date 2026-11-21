namespace ABEFWebApp.Models;

public class CategoryPrinciple
{
    public int CategoryId { get; set; }
    public int PrincipleId { get; set; }

    public Category? Category { get; set; }
    public Principle? Principle { get; set; }
}
