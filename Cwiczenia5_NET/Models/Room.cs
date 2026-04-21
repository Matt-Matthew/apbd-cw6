using System.ComponentModel.DataAnnotations;

namespace Cwiczenia5_NET.Models;

public class Room
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nazwa musi byc podana")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Kod budynku musi byc podany")]
    public string BuildingCode { get; set; }
    
    public int Floor { get; set; }
    [Range(1, int.MaxValue,ErrorMessage = "Pojemnosc musi byc wieksza od 0")]
    public int Capacity { get; set; }
    
    public bool HasProjector { get; set; }
    
    public bool IsActive { get; set; }
    
}