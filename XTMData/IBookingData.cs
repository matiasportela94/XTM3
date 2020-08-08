using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using XTMCore;

namespace XTMData
{
    public interface IBookingData
    {
        IEnumerable<Booking> GetAll();
        Booking Add(Booking newBooking);
        Booking Delete(int bookingID);
        int Commit();
        Booking Update(Booking updatedBooking);
        Booking GetBookingByID(int bookingID);
        IEnumerable<Booking> GetBookingsByDateOrPlaneID(string bookingDateOrID);
        double SetFlightPrice(Booking booking, Avion plane);
        IEnumerable<Booking> GetUserIDBookings(int userID);

    }
}
