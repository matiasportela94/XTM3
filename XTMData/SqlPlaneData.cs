using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using XTMCore;
using Microsoft.EntityFrameworkCore;

namespace XTMData
{
    public class SqlPlaneData : IAvionData
    {
        private readonly XTMDbContext db;

        public SqlPlaneData(XTMDbContext db)
        {
            this.db = db;
        }

        public Avion Add(Avion newPlane)
        {
            db.Add(newPlane);
            return newPlane;
        }

        public int Commit()
        {
            return db.SaveChanges();

        }

        public Avion Delete(int planeID)
        {
            var plane = GetPlanesByID(planeID);
            if (plane != null)
            {
                db.Planes.Remove(plane);
            }
            return plane;
        }

        public Avion GetPlanesByID(int planeID)
        {
            return db.Planes.Find(planeID);
        }

        public IEnumerable<Avion> GetAll()
        {
            var query = from p in db.Planes
                        orderby p.PlaneID
                        select p;
            return query;
        }


        public IEnumerable<Avion> GetPlanesByNameOrID(string planeID)
        {
            var query = from p in db.Planes
                        where (p.PlaneID.ToString().Contains(planeID) || p.PlaneName.Contains(planeID))
                        orderby p.PlaneID
                        select p;
            return query;
        }

        public List<Propulsion> GetPropulsions()
        {
            var propulsions = new List<Propulsion>
            {
                Propulsion.HELICE,
                Propulsion.MOTOR_A_REACCION,
                Propulsion.PISTONES
            };

            return propulsions;
        }

        public bool IsPlane(int planeID)
        {
            throw new NotImplementedException();
        }

        public Avion Update(Avion updatedPlane)
        {
            var entity = db.Planes.Attach(updatedPlane);
            entity.State = EntityState.Modified;
            return updatedPlane;
        }

        public Avion SetPlaneID(Avion avion)
        {
            var updatePlane = avion;

            if (updatePlane.PlaneType.Equals("Gold"))
            {
                var goldPlanes = GetPlanesByType("Gold");
                if (goldPlanes.Count() == 0)
                {
                    updatePlane.PlaneID = 1000;
                }
                else
                {
                    var lastPlane = goldPlanes.Last<Avion>();
                    updatePlane.PlaneID = lastPlane.PlaneID + 1;
                }
            }
            else if (updatePlane.PlaneType.Equals("Silver"))
            {
                var silverPlanes = GetPlanesByType("Silver");
                if (silverPlanes.Count() == 0)
                {
                    updatePlane.PlaneID = 2000;
                }
                else
                {
                    var lastPlane = silverPlanes.Last<Avion>();
                    updatePlane.PlaneID = lastPlane.PlaneID + 1;
                }
            }
            else if (updatePlane.PlaneType.Equals("Bronze"))
            {
                var bronzePlanes = GetPlanesByType("Bronze");
                if (bronzePlanes.Count() == 0)
                {
                    updatePlane.PlaneID = 3000;
                }
                else
                {
                    var lastPlane = bronzePlanes.Last<Avion>();
                    updatePlane.PlaneID = lastPlane.PlaneID + 1;
                }
            }
            return updatePlane;
        }



        public IEnumerable<Avion> GetPlanesByType(string planeType)
        {
            var query = from p in db.Planes
                        where (p.PlaneType.Equals(planeType))
                        orderby p.PlaneID
                        select p;
            return query;
        }

       
        

    }
}
