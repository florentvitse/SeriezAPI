using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortableClassLibrarySerie;
using System.Collections.Generic;

namespace TestAPI
{
    [TestClass]
    public class SeriezAPITest
    {
        [TestMethod]
        public void TestSearch()
        {
            Connector connect = new Connector("8AAF5699353D0F73", "fr");
            connect.GetMirrors();
            List<SearchSerie> a = new List<SearchSerie>();
            a.AddRange(connect.Search("Person of Interest"));            
        }
    }
}
