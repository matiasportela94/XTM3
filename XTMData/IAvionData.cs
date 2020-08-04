using System;
using System.Collections.Generic;
using System.Text;
using XTMCore;

namespace XTMData
{
    public interface IAvionData
    {
        IEnumerable<Avion> GetAll();
        IEnumerable<Avion> GetPlanesByNameOrID(string planeID);
        bool IsPlane(int planeID);
        List<Propulsion> GetPropulsions();
        Avion Add(Avion newPlane);
        Avion Delete(int planeID);
        int Commit();
        Avion Update(Avion newPlaneupdatedPlane);
        Avion GetPlanesByID(int planeID);
        Avion SetPlaneID(Avion avion);
    }
}
