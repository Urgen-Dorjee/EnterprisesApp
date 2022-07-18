namespace ClientWebApp.Shared.Models;
//ReSharper disable all
public class EmployeeTerritory
{
    public int EmployeeId { get; set; }
    public string? TerritoryId { get; set; }
    public Employee? Employee { get; set; } = new();
    public Territory? Territory { get; set; } 
}
