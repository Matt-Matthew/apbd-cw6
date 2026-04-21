using Cwiczenia5_NET.Data;
using Cwiczenia5_NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia5_NET.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{

    [HttpGet("{id}")]
    public ActionResult<Room> GetRoomById(int id)
    {
        var findAny = StaticData.Rooms.FirstOrDefault(r => r.Id == id);
        if (findAny == null)
        {
            return NotFound();
        }
        return Ok(findAny);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<List<Room>> GetBuilding(string buildingCode)
    {
        var findById = StaticData.Rooms.Where(r => r.BuildingCode == buildingCode);
        if  (!findById.Any())
        {
            return NotFound();
        }
        return Ok(findById);
    }

    [HttpGet]
    public ActionResult<List<Room>> GetAllRooms([FromQuery] int? minCapacity, 
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        IEnumerable<Room> query = StaticData.Rooms;
        if (minCapacity.HasValue)
        {
            query = query.Where(r=> r.Capacity >= minCapacity);
        }

        if (hasProjector.HasValue)
        {
            query = query.Where(r=> r.HasProjector == hasProjector.Value);
        }

        if (activeOnly.HasValue)
        {
            query = query.Where(r => r.IsActive == activeOnly.Value);
        }
        return Ok(query.ToList());
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room room)
    {
        int newId = StaticData.Rooms.Any() ? StaticData.Rooms.Max(r => r.Id) + 1 : 1;
        room.Id = newId;
        StaticData.Rooms.Add(room);
        return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, [FromBody] Room updateRoom)
    {
        if (id != updateRoom.Id)
        {
            return BadRequest("ID w adresie URL nie zgadza sie z ID w ciele zadania");
        }
        var existingRoom = StaticData.Rooms.FirstOrDefault(r => r.Id == id);
        if (existingRoom == null)
        {
            return NotFound();
        }

        existingRoom.Name = updateRoom.Name;
        existingRoom.BuildingCode = updateRoom.BuildingCode;
        existingRoom.Floor = updateRoom.Floor;
        existingRoom.Capacity = updateRoom.Capacity;
        existingRoom.HasProjector = updateRoom.HasProjector;
        existingRoom.IsActive = updateRoom.IsActive;
        return Ok(existingRoom);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        var ifExists = StaticData.Rooms.FirstOrDefault(r => r.Id == id);
        if (ifExists == null)
        {
            return NotFound();
        }
        bool hasReservation = StaticData.Reservations.Any(r => r.RoomId == id);
        if (hasReservation)
        {
            return Conflict("Nie mozna usunac sali");
        }
        StaticData.Rooms.Remove(ifExists);
        
        return NoContent();
    }
}