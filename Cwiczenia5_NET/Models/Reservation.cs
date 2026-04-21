using System.ComponentModel.DataAnnotations;

namespace Cwiczenia5_NET.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }
    
    public int RoomId { get; set; }
    
    [Required(ErrorMessage = "Organizer name musi byc podane")]
    public string OrganizerName { get; set; }
    
    [Required(ErrorMessage = "Topic musi byc podany")]
    public string Topic {get; set;}
    
    public DateTime Date { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    public string Status { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult("Czas zakonczenie musi byc wiekszy niz ropoczecia", 
                new[] {nameof(EndTime)});
        }
    }
    
}