using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using XTMCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace XTMData
{
    public class SqlPlaneData : IAvionData
    {
        private readonly XTMDbContext db;

        public SqlPlaneData(XTMDbContext db)
        {
            this.db = db;
        }


        /**
        * Summary:
        *      El metodo recibe un objeto de tipo Avion (una nuevo avion) y lo agrega a la base de datos.
        *
        * **/

        public Avion Add(Avion newPlane)
        {
            db.Add(newPlane);
            return newPlane;
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
       *      El metodo recibe el ID de un avion(int), este parametro es utilizado para buscar el Avion en la base de datos.
       *      Si es encontrad, es eliminado de la base de datos.
       *
       * **/

        public Avion Delete(int planeID)
        {
            var plane = GetPlanesByID(planeID);
            if (plane != null)
            {
                db.Planes.Remove(plane);
            }
            return plane;
        }


        /**
        * Summary:
        *      El metodo busca y retorna un avion por id. Si no lo encuentra devuelve null.
        *      
        * **/

        public Avion GetPlanesByID(int planeID)
        {
            return db.Planes.Find(planeID);
        }


        /**
        * Summary:
        *      El metodo retorna todos los Aviones guardados en la base de datos. Si no hay elementos, devuelve null.
        *
        * **/

        public IEnumerable<Avion> GetAll()
        {
            var query = from p in db.Planes
                        orderby p.PlaneID
                        select p;
            return query;
        }

        /**
        * Summary:
        *      El metodo recibe un string, el cual puede ser el nombre o el ID de un Avion. Este es comparado con el ID y los nombres de los Aviones
        *      que estan guardados en la base de datos.
        *      Retorna todos los aviones cuyos IDs o Nombres contengan los caracteres del string.
        *      
        *      Por ejempo: 
        *      string planeIDOrName = "15"
        *
        *   Retornara todos los Aviones cuyo ID contengan 15 (ID=15;ID=115;ID=151;ID=152;ID=1500;...)
        *   
        *       string planeIDOrName = "Avion"
        *       
        *   y/o todos los aviones cuyo nombre contenga "avion" (Nombre="Avioncito";Nombre="Avionazo";Nombre="Avion";...)
        *   
        *   
        **/

        public IEnumerable<Avion> GetPlanesByNameOrID(string planeIDOrName)
        {
            var query = from p in db.Planes
                        where (p.PlaneID.ToString().Contains(planeIDOrName) || p.PlaneName.Contains(planeIDOrName))
                        orderby p.PlaneID
                        select p;
            return query;
        }


        /**
        * Summary:
        *      El metodo retorna una Lista con todas las propulsiones del Enum Propulsion
        *   
        *   
        **/

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

        /**
      * Summary:
      *      El metodo recibe un objeto de tipo Avion, el cual es enviado al metodo Attach(Avion avion), el cual actualiza los datos de un Avion que ya esta en la base de datos.
      * **/


        public Avion Update(Avion updatedPlane)
        {
            var entity = db.Planes.Attach(updatedPlane);
            entity.State = EntityState.Modified;
            return updatedPlane;
        }


        /**
      * Summary:
      *      El metodo retorna todos los aviones de una categoria especifica (Gold/Silver/Bronze).
      * **/


        public IEnumerable<Avion> GetPlanesByType(string planeType)
        {
            var query = from p in db.Planes
                        where (p.PlaneType.Equals(planeType))
                        orderby p.PlaneID
                        select p;
            return query;
        }

        /**
      * Summary:
      *      El metodo invoca a la funcion SetAllHabilitados(), y retorna todos los aviones que esten habilitados para volar en una fecha especifica y cuya capacidad sea mayor que la solicitada.
      * **/



        public IEnumerable<Avion> GetAllHabilitados(string Date, int Passengers, IEnumerable<Avion> Planes, IEnumerable<Booking> Bookings)
        {
            SetAllHabilitados(Date, Planes, Bookings);

            var query = from p in Planes
                        where (p.Available == true && Passengers <= p.PassengerCapacity)
                        orderby p.PlaneID
                        select p;
            return query;


        }


        /**
      * Summary:
      *      El metodo busca los aviones reservados con los IDs de los Aviones reservados en la Bookings confirmadas.
      *      Si el avion esta reservado la fecha solicitada, se cambia su estado de Available a false.
      *      
      *      
      *      
      *      
      *      **/

        public void SetAllHabilitados(string Date, IEnumerable<Avion> Planes, IEnumerable<Booking> Bookings)
        {
          

            if (Planes != null && Bookings != null)
            {

                foreach (var plane in Planes)
                {
                    foreach (var booking in Bookings)
                    {
                        if (Date.Equals(booking.Date) && booking.PlaneID == plane.PlaneID)
                        {
                            plane.Available = false;
                            break;
                        }
                        else
                        {
                            plane.Available = true;
                            continue;
                        }
                    }
                }

            }

        }

       
    }
}
