using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using XTMCore;
using Microsoft.EntityFrameworkCore;

namespace XTMData
{
    public class SqlBookingData : IBookingData
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
                        where (b.Date.Contains(bookingDateOrID) || b.BookingID.ToString().Contains(bookingDateOrID))
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

        public double SetFlightPrice(Booking booking, Avion plane)
        {
            const int BSAS_CORDOBA = 696;
            const int BSAS_MONTEVIDEO = 593;
            const int BSAS_SANTIAGO = 1411;
            const int CORDOBA_MONTEVIDEO = 1002;
            const int CORDOBA_SANTIAGO = 1000;
            const int MONTEVIDEO_SANTIAGO = 1864;

            double flightPrice = 0.00;

            if (booking.OriginCity.Equals("BUENOS_AIRES") && booking.DestinyCity.Equals("CORDOBA") || booking.OriginCity.Equals("CORDOBA") && booking.DestinyCity.Equals("BUENOS_AIRES"))
            {
                flightPrice = ((BSAS_CORDOBA * plane.KmCost) / 10);
            }
            else if (booking.OriginCity.Equals("BUENOS_AIRES") && booking.DestinyCity.Equals("MONTEVIDEO") || booking.OriginCity.Equals("MONTEVIDEO") && booking.DestinyCity.Equals("BUENOS_AIRES"))
            {
                flightPrice = ((BSAS_MONTEVIDEO * plane.KmCost) / 10);
            }
            else if (booking.OriginCity.Equals("BUENOS_AIRES") && booking.DestinyCity.Equals("SANTIAGO_DE_CHILE") || booking.OriginCity.Equals("SANTIAGO_DE_CHILE") && booking.DestinyCity.Equals("BUENOS_AIRES"))
            {
                flightPrice = ((BSAS_SANTIAGO * plane.KmCost) / 10);
            }
            else if (booking.OriginCity.Equals("CORDOBA") && booking.DestinyCity.Equals("MONTEVIDEO") || booking.OriginCity.Equals("MONTEVIDEO") && booking.DestinyCity.Equals("CORDOBA"))
            {
                flightPrice = ((CORDOBA_MONTEVIDEO * plane.KmCost) / 10);
            }
            else if (booking.OriginCity.Equals("CORDOBA") && booking.DestinyCity.Equals("SANTIAGO_DE_CHILE") || booking.OriginCity.Equals("SANTIAGO_DE_CHILE") && booking.DestinyCity.Equals("CORDOBA"))
            {
                flightPrice = ((CORDOBA_SANTIAGO * plane.KmCost) / 10);
            }
            else if (booking.OriginCity.Equals("MONTEVIDEO") && booking.DestinyCity.Equals("SANTIAGO_DE_CHILE") || booking.OriginCity.Equals("SANTIAGO_DE_CHILE") && booking.DestinyCity.Equals("MONTEVIDEO"))
            {
                flightPrice = ((MONTEVIDEO_SANTIAGO * plane.KmCost) / 10);
            }

            return flightPrice;
        }

     
    }
}
