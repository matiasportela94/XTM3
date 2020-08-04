using System;
using System.Collections.Generic;
using System.Text;

namespace XTMCore
{
    public class Client
    {
        public int UserID { get; set; } // DNI
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }

        public Client() { }

        public Client(int UserID, string FirstName, string LastName, int Age, string Location)
        {
            this.UserID = UserID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Age = Age;
            this.Location = Location;
        }


        public override string ToString()
        {
            return ("\nUser ID: " + UserID +
                    "\nName: " + FirstName + " " + LastName +
                    "\nAge: " + Age +
                    "\nLocation: " + Location);
        }

    }
}
