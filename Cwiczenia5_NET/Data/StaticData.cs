using Cwiczenia5_NET.Models;

namespace Cwiczenia5_NET.Data;

public class StaticData
{
    public static List<Room> Rooms = new List<Room>()
    {
        new Room
        {
            Id = 1, Name = "Room 1", BuildingCode = "C", Floor = 3, Capacity = 60, HasProjector = true, IsActive = true
        },
        new Room
        {
            Id = 2, Name = "Room 2", BuildingCode = "A", Floor = 2, Capacity = 40, HasProjector = false, IsActive = true
        },
        new Room
        {
            Id = 3, Name = "Room 3", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = false, IsActive = false
        },
        new Room
        {
            Id = 4, Name = "Room 4", BuildingCode = "B", Floor = 1, Capacity = 10, HasProjector = true, IsActive = false
        }
        ,
        new Room
        {
            Id = 5, Name = "Room 5", BuildingCode = "F", Floor = 4, Capacity = 70, HasProjector = false, IsActive = true
        }
        ,
        new Room
        {
            Id = 6, Name = "Room 6", BuildingCode = "G", Floor = 2, Capacity = 20, HasProjector = false, IsActive = false
        }
        ,
        new Room
        {
            Id = 7, Name = "Room 7", BuildingCode = "H", Floor = 0, Capacity = 5, HasProjector = true, IsActive = true
        }
        

    };

    public static List<Reservation> Reservations = new List<Reservation>()
    {
        new Reservation
        {
            Id = 1, RoomId = 1, OrganizerName = "Name", Topic = "Topic1", Date = new DateTime(2020,10,2),
            StartTime =  new TimeOnly(9,10,5),
            EndTime =  new TimeOnly(10,30,1),
            Status = "Active",
        },
        new Reservation
        {
        Id = 2, RoomId = 2, OrganizerName = "Name2", Topic = "Topic2", Date = new DateTime(2023,5,23),
        StartTime =  new TimeOnly(8,30,5),
        EndTime =  new TimeOnly(9,40,24),
        Status = "Unactive",
        },
        new Reservation
        {
            Id = 3, RoomId = 3, OrganizerName = "Name3", Topic = "Topic3", Date = new DateTime(2008,2,13),
            StartTime =  new TimeOnly(11,30,8),
            EndTime =  new TimeOnly(12,30,23),
            Status = "Planned",
        }
        ,
        new Reservation
        {
            Id = 4, RoomId = 4, OrganizerName = "Name4", Topic = "Topic4", Date = new DateTime(2025,9,14),
            StartTime =  new TimeOnly(9,20,24),
            EndTime =  new TimeOnly(11,20,24),
            Status = "Cancelled",
        }
        ,
        new Reservation
        {
            Id = 5, RoomId = 5, OrganizerName = "Name5", Topic = "Topic5", Date = new DateTime(2026,1,2),
            StartTime =  new TimeOnly(10,0,5),
            EndTime =  new TimeOnly(11,0,24),
            Status = "Planned",
        }
    };


}
