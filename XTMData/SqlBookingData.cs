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


        /**
         * Summary:
         *      El metodo recibe un objeto de tipo Booking (una nueva reserva) y lo agrega a la base de datos.
         *
         * **/

        public Booking Add(Booking newBooking)
        {
            db.Add(newBooking);
            return newBooking;
        }

        /**
        * Summary:
        *      El metodo guarda los cambios realizados en la base de datos.
        *
        * **/

        public int Commit()
        {
            return db.SaveChanges();
        }

        /**
        * Summary:
        *      El metodo recibe el ID de una reserva (int), este parametro es utilizado para buscar la Reserva en la base de datos.
        *      Si la reserva es encontrada, es eliminada de la base de datos.
        *
        * **/

        public Booking Delete(int bookingID)
        {
            var booking = GetBookingByID(bookingID);
            if (booking != null)
            {
                db.Bookings.Remove(booking);
            }
            return booking;
        }

        /**
        * Summary:
        *      El metodo retorna todas las Reservas guardadas en la base de datos. Si no hay elementos, devuelve null.
        *
        * **/

        public IEnumerable<Booking> GetAll()
        {
            var query = from b in db.Bookings
                        orderby b.BookingID
                        select b;
            return query;
        }

        /**
        * Summary:
        *      El metodo recibe un ID de Reserva, la busca y la retorna, si no la encuentra devuelve null.
        *
        * **/

        public Booking GetBookingByID(int bookingID)
        {
            return db.Bookings.Find(bookingID);
        }


        /**
        * Summary:
        *      El metodo recibe un string, el cual puede ser la Fecha o el ID de una Reserva. Este es comparado con el ID y la Fecha de las Reservas 
        *      que estan guardadas en la base de datos.
        *      Retorna todas las Reservas cuyos IDs o Fechas contengan los caracteres del string.
        *      
        *      Por ejempo: 
        *      string bookingDateOrID = "15"
        *      
        *      Retornara todos las reservas cuyo ID contengan 15 (ID=15;ID=115;ID=151;ID=152;ID=1500;...) y/o todas las reservas que tengan "15" (Date="15/12/2020"; Date="01/01/2015";Date="15/03/2015";...)
        *
        * **/
        public IEnumerable<Booking> GetBookingsByDateOrPlaneID(string bookingDateOrID)
        {
            var query = from b in db.Bookings
                        where (b.Date.Contains(bookingDateOrID) || b.BookingID.ToString().Contains(bookingDateOrID))
                        orderby b.BookingID
                        select b;
            return query;
        }

        /**
       * Summary:
       *      El metodo recibe un objeto de tipo booking, el cual es enviado al metodo Attach(Booking booking), el cual actualiza los datos de una Reserva que ya esta en la base de datos.
       * **/

        public Booking Update(Booking updatedBooking)
        {
            var entity = db.Bookings.Attach(updatedBooking);
            entity.State = EntityState.Modified;
            return updatedBooking;
        }

        /**
        * Summary:
        *      El metodo recibe un objeto de tipo booking y otro objeto de tipo Avion que representa al Avion que sera utilizado el dia de la reserva.
        *      La funcion utiliza el Origenes, Destino, y la cantidad de pasajeros de la reserva, del objeto de tipo Booking, y el precio por km recorrido del objeto de tipo avion. Con una 
        *      formula simple se establece el precio del pasaje.

        *      
        *      * **/

        public double SetFlightPrice(Booking booking, Avion plane)
        {
            const int BSAS_CORDOBA = 696;
            const int BSAS_MONTEVIDEO = 593;
            const int BSAS_SANTIAGO = 1411;
            const int CORDOBA_MONTEVIDEO = 1002;
            const int CORDOBA_SANTIAGO = 1000;
            const int MONTEVIDEO_SANTIAGO = 1864;

            double flightPrice = 0.00;

            if (booking.OriginCity.Equals(Ciudad.BUENOS_AIRES) && booking.DestinyCity.Equals(Ciudad.CORDOBA) || booking.OriginCity.Equals(Ciudad.CORDOBA) && booking.DestinyCity.Equals(Ciudad.BUENOS_AIRES))
            {
                flightPrice = ((BSAS_CORDOBA * plane.KmCost) / 10) * booking.Passengers;
            }
            else if (booking.OriginCity.Equals(Ciudad.BUENOS_AIRES) && booking.DestinyCity.Equals(Ciudad.MONTEVIDEO) || booking.OriginCity.Equals(Ciudad.MONTEVIDEO) && booking.DestinyCity.Equals(Ciudad.BUENOS_AIRES))
            {
                flightPrice = ((BSAS_MONTEVIDEO * plane.KmCost) / 10) * booking.Passengers;
            }
            else if (booking.OriginCity.Equals(Ciudad.BUENOS_AIRES) && booking.DestinyCity.Equals(Ciudad.SANTIAGO_DE_CHILE) || booking.OriginCity.Equals(Ciudad.SANTIAGO_DE_CHILE) && booking.DestinyCity.Equals(Ciudad.BUENOS_AIRES))
            {
                flightPrice = ((BSAS_SANTIAGO * plane.KmCost) / 10) * booking.Passengers;
            }
            else if (booking.OriginCity.Equals(Ciudad.CORDOBA) && booking.DestinyCity.Equals(Ciudad.MONTEVIDEO) || booking.OriginCity.Equals(Ciudad.MONTEVIDEO) && booking.DestinyCity.Equals(Ciudad.CORDOBA))
            {
                flightPrice = ((CORDOBA_MONTEVIDEO * plane.KmCost) / 10) * booking.Passengers;
            }
            else if (booking.OriginCity.Equals(Ciudad.CORDOBA) && booking.DestinyCity.Equals(Ciudad.SANTIAGO_DE_CHILE) || booking.OriginCity.Equals(Ciudad.SANTIAGO_DE_CHILE) && booking.DestinyCity.Equals(Ciudad.CORDOBA))
            {
                flightPrice = ((CORDOBA_SANTIAGO * plane.KmCost) / 10) * booking.Passengers;
            }
            else if (booking.OriginCity.Equals(Ciudad.MONTEVIDEO) && booking.DestinyCity.Equals(Ciudad.SANTIAGO_DE_CHILE) || booking.OriginCity.Equals(Ciudad.SANTIAGO_DE_CHILE) && booking.DestinyCity.Equals(Ciudad.MONTEVIDEO))
            {
                flightPrice = ((MONTEVIDEO_SANTIAGO * plane.KmCost) / 10) * booking.Passengers;
            }

            return flightPrice;
        }


        /**
       * Summary:
       *      El metodo recibe el ID del usuario y devuelve todas sus reservas.
       *      
       *      
        **/

        public IEnumerable<Booking> GetUserIDBookings(int userID)
        {
            var query = from b in db.Bookings
                        where (b.UserID == userID)
                        orderby b.BookingID
                        select b;
            return query;
        }

        

    }
}
