using System.Collections.Generic;
using XTMCore;

namespace XTMData
{
    public interface IClientData
    {
        IEnumerable<Client> GetAll();
        Client Add(Client newClient);
        Client Delete(int clientID);
        int Commit();
        Client Update(Client updatedClient);
        Client GetClientsByID(int clientID);
        IEnumerable<Client> GetClients(string clientID);

    }
}
