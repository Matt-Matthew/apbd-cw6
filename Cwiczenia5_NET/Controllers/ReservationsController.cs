using Cwiczenia5_NET.Data;
using Cwiczenia5_NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia5_NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController: ControllerBase
{
    [HttpGet]
    public ActionResult<List<Reservation>> GetAllReservations([FromQuery] DateTime? date, 
        [FromQuery] string? status, [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> query = StaticData.Reservations;
        if (date.HasValue)
        {
            query = query.Where(r=> r.Date.Date == date.Value.Date);
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(r=> r.Status.Equals(status) );
        }

        if (roomId.HasValue)
        {
            query = query.Where(r=> r.RoomId == roomId.Value);
        }
        return Ok(query.ToList());
    }
   

    [HttpGet("{id}")]
    public ActionResult<Reservation> GetReservationById(int id)
    {
        var reservation = StaticData.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteReservation(int id)
    {
        var ifExistsRes = StaticData.Reservations.FirstOrDefault(r=> r.Id == id);
        if (ifExistsRes == null)
        {
            return NotFound();
        }
        StaticData.Reservations.Remove(ifExistsRes);
        return NoContent();
    }

    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation reservation)
    {
        int newId = StaticData.Reservations.Any() ? StaticData.Reservations.Max(r => r.Id) + 1 : 1;
        reservation.Id = newId;

        var room = StaticData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room == null)
        {
            return BadRequest("Sala o podanym Id nie istnieje");
        }

        if (!room.IsActive)
        {
            return BadRequest("Nie mozna zarezerwowac nieaktywnej sali");
        }
        bool isConflict = StaticData.Reservations.Any(e => HasTimeConflict(e, reservation.RoomId, reservation.Date, reservation.StartTime, reservation.EndTime));
        if (isConflict)
        {
            return Conflict("Sala jest juz zajeta");
        }
        StaticData.Reservations.Add(reservation);
        return CreatedAtAction("GetReservationById", new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservation(int id, [FromBody] Reservation updateReservation)
    {
        if (id != updateReservation.Id)
        {
            return BadRequest("ID w adresie URL nie zgadza sie z ID w ciele zadania");
        }
        
        
        var room = StaticData.Rooms.FirstOrDefault(r => r.Id == updateReservation.RoomId);
        if (room == null)
        {
            return BadRequest("Sala o podanym Id nie istnieje");
        }

        if (!room.IsActive)
        {
            return BadRequest("Nie mozna przydzielic rezerwacji do nieaktywnej sali");
        }
        
        bool isConflict = StaticData.Reservations.Any(e => e.Id != id &&
            HasTimeConflict(e, updateReservation.RoomId, updateReservation.Date, updateReservation.StartTime, updateReservation.EndTime));
        if (isConflict)
        {
            return Conflict("Nie mozna updateowac rezerwacji");
        }
     
            
        var existing = StaticData.Reservations.FirstOrDefault(r => r.Id == id);
        if (existing == null) return NotFound();
        
        existing.OrganizerName = updateReservation.OrganizerName;
        existing.Topic = updateReservation.Topic;
        existing.Date = updateReservation.Date;
        existing.StartTime = updateReservation.StartTime;
        existing.EndTime = updateReservation.EndTime;
        existing.Status = updateReservation.Status;
        existing.RoomId = updateReservation.RoomId;
        return Ok(existing);
    }

    private bool HasTimeConflict(Reservation existingRes, int roomId, DateTime date, TimeOnly startTime, TimeOnly endTime)
    {
        return existingRes.RoomId == roomId &&
               existingRes.Date.Date == date.Date &&
               startTime < existingRes.EndTime &&
               endTime > existingRes.StartTime;
    }


}