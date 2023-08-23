using System;
using System.Collections.Generic;
using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class DbDataOperation
    {
        private PhotoStudioModel db;

        public DbDataOperation()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PhotoStudioModel>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PhotoStudioModel>());
            db = new PhotoStudioModel();
        }

        public List<Room> GetRooms(int minPrice, int maxPrice, int numberOfPeople)
        {
            return db.Rooms
                .Where(r => r.Cost >= minPrice && r.Cost <= maxPrice && r.Status == "Available" && r.Capacity >= numberOfPeople)
                .OrderBy(r => r.Cost)
                .ToList();
        }

        public List<Room> GetRooms(int minPrice, int maxPrice, int numberOfPeople, string category)
        {
            return db.Rooms
                .Where(r => r.Cost >= minPrice && r.Cost <= maxPrice && r.Capacity >= numberOfPeople && r.Category == category && r.Status == "Available")
                .OrderBy(r => r.Cost)
                .ToList();
        }

        public List<Room> GetRooms(int minPrice, int maxPrice, string status, int numberOfPeople)
        {
            return db.Rooms
                .Where(r => r.Status == status)
                .OrderBy(r => r.Cost)
                .ToList();
        }

        public List<Room> GetRooms()
        {
            return db.Rooms.ToList();
        }

        public void UpdateReservation(Reservation reservation)
        {
            db.Reservations.FirstOrDefault(r => r.Id == reservation.Id).ServicesNumber = reservation.ServicesNumber;
            db.Reservations.FirstOrDefault(r => r.Id == reservation.Id).TotalPrice = reservation.TotalPrice;
        }

        public List<ServiceMembership> GetMemberships(int reservationId, int guestId)
        {
            return db.ServiceMemberships.Where(m => m.ReservationId == reservationId && m.GuestId == guestId).ToList();
        }

        public void DeleteMemberships(int reservationId, int guestId)
        {
            var Memberships = db.ServiceMemberships.Where(m => m.ReservationId == reservationId && m.GuestId == guestId).ToList();
            foreach (ServiceMembership membership in Memberships)
                db.ServiceMemberships.Remove(membership);

            db.SaveChanges();
        }

        public Guest FindGuest(Guest guest)
        {
            if (db.Guests != null)
            {
                var foundGuest = db.Guests.FirstOrDefault(g => g.Name == guest.Name && g.Surname == guest.Surname &&
                g.Passport == guest.Passport && g.BirthDate == guest.BirthDate);

                return foundGuest != null ? foundGuest : null;
            }

            return null;
        }

        public void AddMembership(ServiceMembership membership)
        {
            db.ServiceMemberships.Add(membership);
            db.SaveChanges();
        }

        public List<Service> GetServices()
        {
            return db.Services
                .OrderBy(s => s.Cost)
                .ToList();
        }

        public List<Guest> GetGuests()
        {
            return db.Guests.ToList();
        }

        public void RoomControl()
        {
            foreach (Reservation reservation in db.Reservations.Where(r => r.DepartureDate < DateTime.Now && r.isActive == true).ToList())
                foreach (Room room in db.Rooms.Where(r => r.Status == "Occupied" && r.Id == reservation.RoomId).ToList())
                {
                    room.Status = "Cleaning";
                    reservation.isActive = false;
                    db.SaveChanges();
                }
        }

        public void AddGuest(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
        }

        public void CreateReservation(Reservation reservation)
        {
            db.Reservations.Add(reservation);
            db.SaveChanges();
        }

        public void ReservateRoom(int id)
        {
            db.Rooms.Find(id).Status = "Occupied";
            db.SaveChanges();
        }

        public void RoomAvailable(int Id)
        {
            db.Rooms.Find(Id).Status = "Available";
            db.SaveChanges();
        }

        public List<Report> GetReservations(DateTime? startDate, DateTime? endDate)
        {
            return db.Reservations
                .Join(db.Rooms,
                reservarion => reservarion.RoomId,
                room => room.Id,
                (reservarion, room) => new
                {
                    Room = room.Number,
                    ArrivalDate = reservarion.ArrivalDate,
                    DepartureDate = reservarion.DepartureDate,
                    TotalPrice = reservarion.TotalPrice
                })
                .Where(r => r.ArrivalDate >= startDate && r.ArrivalDate <= endDate)
                .OrderBy(r => r.ArrivalDate)
                .Select(r => new Report
                {
                    Room = r.Room,
                    Arrival = r.ArrivalDate.ToString(),
                    Departure = r.DepartureDate.ToString(),
                    Total = "$" + r.TotalPrice.ToString()
                })
                .ToList();
        }

        public List<Reservation> GetAllReservations()
        {
            return db.Reservations
                .Include("Guests")
                .ToList();
        }

        public UserData GetCurrentUser(string Login, string Password)
        {
            List<UserData> users;

            users = db.Users.ToList().Where(u => u.Login == Login && u.Password == Password).Select(u => new UserData
            {
                Id = u.Id,
                Name = u.Name,
                SecondName = u.SecondName,
                Position = u.Position
            }).ToList();

            return (users.Count > 0) ? users[0] : null;
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public int CurrentReservationsNumber()
        {
            return db.Reservations
                .Where(r => r.ReservationDate.Year == DateTime.Now.Year && r.ReservationDate.Month == DateTime.Now.Month)
                .ToList()
                .Count;
        }

        public int CurrentIncome()
        {
            return db.Reservations
                .Where(r => r.ReservationDate.Year == DateTime.Now.Year && r.ReservationDate.Month == DateTime.Now.Month)
                .Sum(r => r.TotalPrice);
        }
    }
}
