using System;

namespace PortableClassLibrarySerie
{
    public class Actor
    {
        int ID {get; set;} 
        String linkPicture {get; set;}
        String Name {get; set;}
        String Role { get; set; }

        public Actor() { }

        public Actor(int prmID, String prmPic, String prmName, String prmRole)
        {
            ID = prmID;
            linkPicture = prmPic;
            Name = prmName;
            Role = prmRole;
        }
    }
}
