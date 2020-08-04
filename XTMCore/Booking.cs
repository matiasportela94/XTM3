using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XTMCore
{
    public class Booking : IEnumerable
    {

        public int BookingID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public Ciudad OriginCity { get; set; }
        [Required]
        public Ciudad DestinyCity { get; set; }
        public int PlaneID { get; set; }
        public double Price { get; set; }
        [Required]
        public int Passengers { get; set; }


        public Booking()
        {
            this.UserID = 0;
            this.Date = "";
            this.OriginCity = Ciudad.NONE;
            this.DestinyCity = Ciudad.NONE;
            this.PlaneID = 0;
            this.Price = 0.00;
            this.Passengers = 0;
        }

        public Booking(int UserID, string Date, Ciudad OriginCity, Ciudad DestinyCity, int PlaneID, int Passengers) : this()
        {
            this.UserID = UserID;
            this.Date = Date;
            this.OriginCity = OriginCity;
            this.DestinyCity = DestinyCity;
            this.PlaneID = PlaneID;
            this.Price = 0.00;
            this.Passengers = Passengers;

        }


        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
