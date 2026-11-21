namespace ABEFWebApp.Models.ViewModels;

public class AbefWhatViewModel
{
    public IReadOnlyList<Category> Categories { get; init; } = [];
    public IReadOnlyList<Principle> Principles { get; init; } = [];
}
