using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PortableClassLibrarySerie
{
    public class Connector
    {
        private String apikey { get; set; }
        public String language { get; set; }
        private List<String> mirrorsPath { get; set; }

        private bool connected = false;

        public Connector(String prmAPI = null, String prmLan = "en")
        {
            apikey = prmAPI;
            language = prmLan;
            GetMirrors();
        }

        public void GetMirrors()
        {
            mirrorsPath = new List<String>();
            if (!String.IsNullOrEmpty(apikey))
            {
                IEnumerable<XElement> mirrors = XElement.Load("http://thetvdb.com/api/" + apikey + "/mirrors.xml").Elements("Mirror");

                foreach (XElement mirror in mirrors)
                {
                    mirrorsPath.Add(mirror.Element("mirrorpath").Value);
                }
                connected = true;
            }
        }

        public SearchSerie[] Search(String prmS)
        {
            List<SearchSerie> valR = new List<SearchSerie>();
            String name = null;

            IEnumerable<XElement> series = XElement.Load(mirrorsPath[0] + "/api/GetSeries.php?seriesname=" + prmS + "&language=" + language).Elements("Series");

            if(series.Any())
            {
                foreach (XElement el in series)
                {
                    try
                    {
                        name = el.Element("SeriesName").Value + " (" + el.Element("AliasNames").Value + ") - " + el.Element("language").Value;
                    }
                    catch (NullReferenceException)
                    {
                        name = el.Element("SeriesName").Value + " - " + el.Element("language").Value;
                    }

                    valR.Add(new SearchSerie(name, Convert.ToInt32(el.Element("seriesid").Value)));
                }
            }
            return valR.ToArray();
        }

        public Serie GetSeriebyID(int prmID)
        {
            Serie valR = null;
            List<String> tempAct = null, tempGenre = null;
            if (!String.IsNullOrEmpty(apikey))
            {
                try
                {
                    XElement doc = XElement.Load(mirrorsPath[0] + "/api/" + apikey + "/series/" + prmID + "/" + language + ".xml");
                    if (doc != null)
                    {
                        XElement serie = doc.Element("Series");

                        tempAct = new List<String>();
                        tempGenre = new List<String>();
                        tempAct.AddRange(serie.Element("Actors").Value.Substring(1, serie.Element("Actors").Value.Length - 2).Split('|'));
                        tempGenre.AddRange(serie.Element("Genre").Value.Substring(1, serie.Element("Genre").Value.Length - 2).Split('|'));

                        valR = new Serie(Convert.ToInt32(serie.Element("id").Value),
                                        tempAct.ToArray(),
                                        serie.Element("ContentRating").Value,
                                        Convert.ToDateTime(serie.Element("FirstAired").Value),
                                        tempGenre.ToArray(),
                                        serie.Element("Network").Value,
                                        serie.Element("Overview").Value,
                                        Convert.ToDouble(serie.Element("Rating").Value.Replace('.', ',')),
                                        serie.Element("SeriesName").Value,
                                        serie.Element("Status").Value);
                    }
                }
                catch (Exception) { }
            }
            return valR;
        }

        private Serie GetSeriebyID(XElement s)
        {
            Serie valR = null;
            List<String> tempAct = null, tempGenre = null;

            try
            {
                tempAct = new List<String>();
                tempGenre = new List<String>();
                tempAct.AddRange(s.Element("Actors").Value.Substring(1, s.Element("Actors").Value.Length - 2).Split('|'));
                tempGenre.AddRange(s.Element("Genre").Value.Substring(1, s.Element("Genre").Value.Length - 2).Split('|'));

                valR = new Serie(Convert.ToInt32(s.Element("id").Value),
                                tempAct.ToArray(),
                                s.Element("ContentRating").Value,
                                Convert.ToDateTime(s.Element("FirstAired").Value),
                                tempGenre.ToArray(),
                                s.Element("Network").Value,
                                s.Element("Overview").Value,
                                Convert.ToDouble(s.Element("Rating").Value.Replace('.', ',')),
                                s.Element("SeriesName").Value,
                                s.Element("Status").Value);
            }
            catch (Exception) {}
            return valR;
        }

        public Episode GetEpisodebyID(int prmID)
        {
            Episode valR = null;
            List<String> tempGStars = null;

            if (!String.IsNullOrEmpty(apikey))
            {
                try
                {
                    XElement doc = XElement.Load(mirrorsPath[0] + "/api/" + apikey + "/series/" + prmID + "/" + language + ".xml");

                    if (doc != null)
                    {
                        XElement serie = doc.Element("Episode");

                        tempGStars = new List<String>();
                        tempGStars.AddRange(serie.Element("GuestStars").Value.Split('|'));

                        valR = new Episode(prmID,
                                            Convert.ToInt32(serie.Element("seasonid").Value),
                                            tempGStars.ToArray(),
                                            Convert.ToDateTime(serie.Element("FirstAired").Value),
                                            serie.Element("Director").Value,
                                            serie.Element("Overview").Value,
                                            serie.Element("Language").Value,
                                            Convert.ToInt32(serie.Element("EpisodeNumber").Value),
                                            Convert.ToInt32(serie.Element("SeasonNumber").Value),
                                            Convert.ToInt32(serie.Element("absolute_number").Value),
                                            Convert.ToDouble(serie.Element("Rating").Value.Replace('.', ',')),
                                            serie.Element("EpisodeName").Value);
                    }
                }
                catch (Exception) {}
            }
            return valR;
        }

        public Episode GetEpisodebySeasonAndNumber(int IDserie, int prmS, int prmN)
        {
            Episode valR = null;
            List<String> tempGStars = null;

            if (!String.IsNullOrEmpty(apikey))
            {
                try
                {
                    XElement doc = XElement.Load(mirrorsPath[0] + "/api/" + apikey + "/series/" + IDserie + "/default/" + prmS + "/" + prmN + "/" + language + ".xml");

                    if (doc != null)
                    {
                        XElement serie = doc.Element("Episode");

                        tempGStars = new List<string>();
                        tempGStars.AddRange(serie.Element("GuestStars").Value.Split('|'));

                        valR = new Episode(IDserie,
                                            Convert.ToInt32(serie.Element("seasonid").Value),
                                            tempGStars.ToArray(),
                                            Convert.ToDateTime(serie.Element("FirstAired").Value),
                                            serie.Element("Director").Value,
                                            serie.Element("Overview").Value,
                                            serie.Element("Language").Value,
                                            Convert.ToInt32(serie.Element("EpisodeNumber").Value),
                                            Convert.ToInt32(serie.Element("SeasonNumber").Value),
                                            Convert.ToInt32(serie.Element("absolute_number").Value),
                                            Convert.ToDouble(serie.Element("Rating").Value.Replace('.', ',')),
                                            serie.Element("EpisodeName").Value);
                    }
                }
                catch (Exception) {}
            }
            return valR;
        }

        public Actor[] GetActorsFromSerie(int IDserie)
        {
            List<Actor> valR = null;
            if (!String.IsNullOrEmpty(apikey))
            {
                try
                {
                    XElement doc = XElement.Load(mirrorsPath[0] + "/api/" + apikey + "/series/" + IDserie + "/actors.xml");

                    if (doc != null)
                    {
                        valR = new List<Actor>();
                        List<XElement> actors = new List<XElement>();
                        actors.AddRange(doc.Elements("Actor"));

                        foreach (XElement el in actors)
                        {
                            valR.Add(new Actor(Convert.ToInt32(el.Element("id").Value),
                                            mirrorsPath[0] + "/banners/" + el.Element("Image").Value,
                                            el.Element("Name").Value,
                                            el.Element("Role").Value));
                        }
                    }
                }
                catch (Exception) {}
            }
            if (valR.Count != 0)
            {
                return valR.ToArray();
            }
            else
            {
                return null;
            }
        }

        private Episode[] GetAllEpisodesFromSerie(XElement[] s)
        {
            List<Episode> valR = null;
            List<String> temp = null;
            int seasonNumber;

            if (!String.IsNullOrEmpty(apikey))
            {
                try
                {
                    temp = new List<String>();

                    foreach (XElement el in s)
                    {
                        temp.AddRange(el.Element("GuestStars").Value.Split('|'));
                        seasonNumber = Convert.ToInt32(el.Element("SeasonNumber").Value);

                        if (seasonNumber > 0)
                        {
                            valR.Add(new Episode(Convert.ToInt32(el.Element("id").Value),
                                                Convert.ToInt32(el.Element("seasonid").Value),
                                                temp.ToArray(),
                                                Convert.ToDateTime(el.Element("FirstAired").Value),
                                                el.Element("Director").Value,
                                                el.Element("Overview").Value,
                                                el.Element("Language").Value,
                                                Convert.ToInt32(el.Element("EpisodeNumber").Value),
                                                seasonNumber,
                                                Convert.ToInt32(el.Element("absolute_number").Value),
                                                Convert.ToDouble(el.Element("Rating").Value.Replace('.', ',')),
                                                el.Element("EpisodeName").Value));
                        }

                        temp = null;
                    }
                }
                catch (Exception) { /* NO INTERNET CONNECTION */ }
            }
            return valR.ToArray();
        }

        public All GetAllInfosSerie(int IDserie)
        {
            All valR = null;
            Serie s = new Serie();
            List<Episode> e;
            List<Actor> a;
            List<XElement> epis = new List<XElement>();

            if (!String.IsNullOrEmpty(apikey))
            {
                try
                {
                    XElement doc = XElement.Load(mirrorsPath[0] + "/api/" + apikey + "/series/" + IDserie + "/default/" + language + ".xml");

                    if (doc != null)
                    {

                        s = GetSeriebyID(doc.Element("Series"));
                        e = new List<Episode>();
                        epis = new List<XElement>();
                        epis.AddRange(doc.Elements("Episode"));
                        e.AddRange(GetAllEpisodesFromSerie(epis.ToArray()));
                        a = new List<Actor>();
                        a.AddRange(GetActorsFromSerie(IDserie));
                        valR = new All(s, e.ToArray(), a.ToArray());
                    }
                }
                catch (Exception) {}
            }
            return valR;
        }
    }

    public class SearchSerie
    {
        public String NameLanguage;
        public int HiddenID;

        public SearchSerie() { }

        public SearchSerie(String prmN, int prmHid)
        {
            NameLanguage = prmN;
            HiddenID = prmHid;
        }
    }

    public class All
    {
        Serie serieBase { get; set; }
        List<Episode> allEpi { get; set; }
        List<Actor> actors { get; set; }

        public All() { }

        public All(Serie prmS, Episode[] prmEpis, Actor[] prmAc)
        {
            serieBase = prmS;
            allEpi = new List<Episode>();
            allEpi.AddRange(prmEpis);
            actors = new List<Actor>();
            actors.AddRange(prmAc);
        }
    }
}
