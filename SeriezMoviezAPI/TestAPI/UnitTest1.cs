using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortableClassLibrarySerie;
using System.Collections.Generic;

namespace TestAPI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Connector connect = new Connector("8AAF5699353D0F73", "fr");
            connect.GetMirrors();
            List<SearchSerie> a = new List<SearchSerie>();
            Serie serie = new Serie();
            Episode epi = new Episode();
            List<Actor> listA = new List<Actor>();
            a.AddRange(connect.Search("Game"));            
            //serie = connect.GetSeriebyID(a[0].HiddenID);
            //epi = connect.GetEpisodebySeasonAndNumber(a[0].HiddenID, 3, 1);
            listA.AddRange(connect.GetActorsFromSerie(a[2].HiddenID));
        }
    }
}
