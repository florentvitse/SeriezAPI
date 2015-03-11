using System;
using System.Collections.Generic;

namespace PortableClassLibrarySerie
{
    public class Episode
    {
        int ID { get; set; }
        int seasonID { get; set; }
        List<String> GuestStars { get; set; }
        DateTime First_Aired { get; set; }
        String Director { get; set; }
        String Overview { get; set; }
        String language { get; set; }
        int EpisodeNumber { get; set; }
        int SeasonNumber { get; set; }
        // Represents the Xth episode of the serie
        int AbsoluteNumber { get; set; }
        double User_Rating { get; set; }
        String Name { get; set; }

        public Episode() { }

        public Episode(int prmID, int prmSID, String[] prmGS, DateTime prmDate, String prmDirec, String prmOverview,
                     String prmLan, int prmEN, int prmSN, int prmAN, double prmUR, String prmName)
        {
            ID = prmID;
            seasonID = prmSID;
            GuestStars = new List<String>();
            GuestStars.AddRange(prmGS);
            First_Aired = prmDate;
            Director = prmDirec;
            Overview = prmOverview;
            language = prmLan;
            EpisodeNumber = prmEN;
            SeasonNumber = prmSN;
            AbsoluteNumber = prmAN;
            User_Rating = prmUR;
            Name = prmName;
        }  

    }
}
