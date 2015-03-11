using System;
using System.Collections.Generic;

namespace PortableClassLibrarySerie
{
    public class Serie
    {
        int ID { get; set; }
        List<String> Actors { get; set; }
        String Content_Rating { get; set; }
        DateTime First_Aired { get; set; }
        List<String> Genres { get; set; }
        String Network { get; set; }
        String Overview { get; set; }
        double User_Rating { get; set; }
        String Name { get; set; }
        String Status { get; set; }

        public Serie() { }

        public Serie(int prmID, String[] prmAc, String prmCont, DateTime prmDate, String[] prmGen,
                     String prmNet, String prmOverview, double prmRat, String prmName, String prmStat)
        {
            ID = prmID;
            Actors = new List<String>();
            Actors.AddRange(prmAc);
            Content_Rating = prmCont;
            First_Aired = prmDate;
            Genres = new List<String>();
            Genres.AddRange(prmGen);
            Network = prmNet;
            Overview = prmOverview;
            User_Rating = prmRat;
            Name = prmName;
            Status = prmStat;
        }
    }
}
