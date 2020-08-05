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


            public Client Add(Client newClient)
            {
                db.Add(newClient);
                return newClient;
            }

            public int Commit()
            {
                return db.SaveChanges();
            }

            public Client Delete(int clientID)
            {
                var client = GetClientsByID(clientID);
                if (client != null)
                {
                    db.Clients.Remove(client);
                }
                return client;
            }

            public IEnumerable<Client> GetAll()
            {
                var query = from c in db.Clients
                            orderby c.UserID
                            select c;
                return query;
            }

            public IEnumerable<Client> GetClients(string clientID)
            {
                var query = from c in db.Clients
                            where (c.UserID.ToString().Contains(clientID))
                            orderby c.UserID
                            select c;
                return query;
            }

            public Client GetClientsByID(int clientID)
            {
                return db.Clients.Find(clientID);
            }

            public Client Update(Client updatedClient)
            {
                var entity = db.Clients.Attach(updatedClient);
                entity.State = EntityState.Modified;
                return updatedClient;
            }


    }
}
