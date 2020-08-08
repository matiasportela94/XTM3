using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using XTMCore;
using Microsoft.EntityFrameworkCore;

namespace XTMData
{
    public class SqlClientData : IClientData
    {

        

            private readonly XTMDbContext db;

            public SqlClientData(XTMDbContext db)
            {
                this.db = db;
            }

        /**
         * Summary:
         *      El metodo recibe un objeto de tipo Booking (una nueva reserva) y lo agrega a la base de datos.
         *
         * **/

        public Client Add(Client newClient)
            {
                db.Add(newClient);
                return newClient;
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
     *      El metodo recibe el ID de un Client(int), este parametro es utilizado para buscar el cliente en la base de datos.
     *      Si es encontrado, es eliminado de la base de datos.
     *
     * **/

        public Client Delete(int clientID)
            {
                var client = GetClientsByID(clientID);
                if (client != null)
                {
                    db.Clients.Remove(client);
                }
                return client;
            }

        /**
        * Summary:
        *      El metodo retorna todos los Clientes guardados en la base de datos. Si no hay elementos, devuelve null.
        *
        * **/

        public IEnumerable<Client> GetAll()
            {
                var query = from c in db.Clients
                            orderby c.UserID
                            select c;
                return query;
            }

        /**
        * Summary:
        *      El metodo recibe un string y retorna todos los clientescuyo ID contengan el string recibido
        *      
        *      Por ejempo: 
        *      string bookingDateOrID = "15"
        *      
        *      Retornara todos las reservas cuyo ID contengan 15 (ID=15;ID=115;ID=151;ID=152;ID=1500;...)
        * **/

        public IEnumerable<Client> GetClients(string clientID)
            {
                var query = from c in db.Clients
                            where (c.UserID.ToString().Contains(clientID))
                            orderby c.UserID
                            select c;
                return query;
            }

            /**
          * Summary:
          *      El metodo recibe un ID de un Cliente, lo busca y lo retorna, si no lo encuentro devuelve null.
          *
          * **/


        public Client GetClientsByID(int clientID)
            {
                return db.Clients.Find(clientID);
            }

        /**
       * Summary:
       *      El metodo recibe un objeto de tipo Client, el cual es enviado al metodo Attach(Client client), el cual actualiza los datos de un Cliente que ya esta en la base de datos.
       * **/

        public Client Update(Client updatedClient)
            {
                var entity = db.Clients.Attach(updatedClient);
                entity.State = EntityState.Modified;
                return updatedClient;
            }


    }
}
