using System;
using System.Collections.Generic;
using System.Text;
using XTMCore;

namespace XTMData
{
    public interface IAvionData
    {
        IEnumerable<Avion> GetAll();
        IEnumerable<Avion> GetAllHabilitados(string Date, int Passengers,IEnumerable<Avion> Planes,  IEnumerable<Booking> Bookings);
        IEnumerable<Avion> GetPlanesByNameOrID(string planeID);
        bool IsPlane(int planeID);
        List<Propulsion> GetPropulsions();
        Avion Add(Avion newPlane);
        Avion Delete(int planeID);
        int Commit();
        Avion Update(Avion newPlaneupdatedPlane);
        Avion GetPlanesByID(int planeID);
        Avion SetPlaneID(Avion avion);
        void SetAllHabilitados(string Date, IEnumerable<Avion> Planes, IEnumerable<Booking> Bookings);

    }
}
