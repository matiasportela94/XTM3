using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace XTMCore
{
    public class Avion : IEnumerable
    {
        public int PlaneID { get; set; }
        public string PlaneName { get; set; }
        public string PlaneType { get; set; }
        public int FuelCapacity { get; set; }
        public int KmCost { get; set; }
        public int PassengerCapacity { get; set; }
        public int MaxVelocity { get; set; }
        public bool Available { get; set; }
        public Propulsion PropulsionType { get; set; }
        public bool Catering { get; set; }
        public bool Wifi { get; set; }

        public Avion() { }

        public Avion(string planeType, int planeID, string planeName, int fuelCapacity, int kmCost, int passengerCapacity, int maxVelocity, bool available, Propulsion propulsionType, bool catering, bool wifi)
        {
            this.PlaneType = planeType;
            this.PlaneName = planeName;
            this.PlaneID = planeID;
            this.FuelCapacity = fuelCapacity;
            this.KmCost = kmCost;
            this.PassengerCapacity = passengerCapacity;
            this.MaxVelocity = maxVelocity;
            this.Available = available;
            this.PropulsionType = propulsionType;
            this.Catering = catering;
            this.Wifi = wifi;
        }

        public override string ToString()
        {
            return ("\nPlane ID: " + PlaneID +
                    "\nPlane Type: " + PlaneType +
                    "\nFuel Capacity: " + FuelCapacity + " lts" +
                    "\nCost: " + KmCost + ", $/km" +
                    "\nPassengers Capacity: " + PassengerCapacity + " passenger/s" +
                    "\nMaximum Velocity: " + MaxVelocity + " km/h" +
                    "\nCatering: " + Catering +
                    "\nWifi: " + Wifi);
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
