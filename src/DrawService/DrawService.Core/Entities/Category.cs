namespace DrawService.Core.Entities;

public class Category
{
    public string CategoryName { get; set; } = string.Empty;
    public List<int> Numbers { get; set; } = [];
}