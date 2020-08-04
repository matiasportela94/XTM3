using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using XTMCore;
using Microsoft.EntityFrameworkCore;

namespace XTMData
{
    class SqlBookingData : IBookingData
    {

        private readonly XTMDbContext db;

        public SqlBookingData(XTMDbContext db)
        {
            this.db = db;
        }


        public Booking Add(Booking newBooking)
        {
            db.Add(newBooking);
            return newBooking;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Booking Delete(int bookingID)
        {
            var booking = GetBookingByID(bookingID);
            if (booking != null)
            {
                db.Bookings.Remove(booking);
            }
            return booking;
        }

        public IEnumerable<Booking> GetAll()
        {
            var query = from b in db.Bookings
                        orderby b.BookingID
                        select b;
            return query;
        }

        public Booking GetBookingByID(int bookingID)
        {
            return db.Bookings.Find(bookingID);
        }

        public IEnumerable<Booking> GetBookingsByDateOrPlaneID(string bookingDateOrID)
        {
            var query = from b in db.Bookings
                        where (b.Date.Contains(bookingDateOrID) || b.BookingID.ToString().IndexOf(bookingDateOrID))
                        orderby b.BookingID
                        select b;
            return query;
        }

      
        public Booking SetBookingPlaneID(int selectedPlaneID)
        {
            throw new NotImplementedException();
        }

        public Booking Update(Booking updatedBooking)
        {
            var entity = db.Bookings.Attach(updatedBooking);
            entity.State = EntityState.Modified;
            return updatedBooking;
        }
    }
}
