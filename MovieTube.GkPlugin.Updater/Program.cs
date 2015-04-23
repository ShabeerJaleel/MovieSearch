using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using MovieFinder.Data;

namespace MovieTube.GkPlugin.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Update();
        }

        private static void Update()
        {
            var doc = XDocument.Load("http://gkplugins.com/mapfile.xml");
            var files = doc.Element("dir").Elements("file").ToList();
            foreach (var file in files)
            {
                using (var db = new MovieFinderEntities())
                {
                    var gkName = file.Attribute("name").Value;
                    var time = Convert.ToInt32(file.Attribute("time").Value);
                    var rec = db.Plugins.FirstOrDefault(x => x.GKName == gkName && x.SiteID != null && time != x.LastUpdatedTime);
                    if (rec == null)
                        continue;
                    //get zip file
                    //updated plugin directory
                    //update plugin xml file
                    //update db
                    rec.LastUpdatedTime = time;
                    db.SaveChanges();
                }
            }
        }
    }
}
