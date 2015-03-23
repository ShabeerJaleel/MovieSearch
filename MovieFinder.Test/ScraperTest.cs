using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieFinder.Scraper;
using System.Runtime.InteropServices;
using System.Web;
using Client.Scraper;
using MovieFinder.Data;
using System.Text.RegularExpressions;

namespace MovieFinder.Test
{
    [TestClass]
    public class ScraperTest
    {
        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, StringBuilder lpszPath, CSIDL nFolder, bool fCreate);

        enum CSIDL
        {
            COMMON_STARTMENU = 0x0016,
            COMMON_PROGRAMS = 0x0017
        }
        [TestMethod]
        public void ABCTest()
        {
            var s = new ABCMalayalam();
            var movies = s.ScrapeMovies(new List<string>(), new List<int> { 2014});
            
        }

        [TestMethod]
        public void ApnaViewTest()
        {
            var s = new ApnaView();
            var movies = s.ScrapeMovies(new List<string>(), new List<int> { 2014 });

        }

        [TestMethod]
        public void TVCDTest()
        {
            var s = new ThriruttuVCD();
            s.ScrapeMovies(new List<string>(), new List<int> { 2014 });
            // var movies = s.ScrapeMovies();

        }

        [TestMethod]
        public void HindiMovies4UTest()
        {
            var s = new Hindi4ULink();
            s.ScrapeMovies(new List<string>(), new List<int> { 2014});
            // var movies = s.ScrapeMovies();

        }


        [TestMethod]
        public void TamizhTest()
        {
            var s = new TamizhWS();
            s.ScrapeMovies(new List<string>(), new List<int> { 2014 });
            // var movies = s.ScrapeMovies();

        }


        [TestMethod]
        public void India4MovieTest()
        {
            var s = new India4movie();
            s.ScrapeMovies(new List<string>(), new List<int> { 2014 });
            // var movies = s.ScrapeMovies();

        }

        [TestMethod]
        public void MyTestMethod()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            path = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);

            StringBuilder allUsersStartMenu = new StringBuilder(255);
            if (SHGetSpecialFolderPath(IntPtr.Zero, allUsersStartMenu, CSIDL.COMMON_PROGRAMS, false))
                path = allUsersStartMenu.ToString();
        }

        [TestMethod]
        public void MyTestMethod1()
        {
            var test = HttpUtility.UrlDecode("http%3A%2F%2Fporn.im.97e477de.4657442.x.xvideos.com%2Fvideos%2Fflv%2F9%2F5%2F8%2Fxvideos.com_95891d9928885cde7e2ab458526b9489.flv%3Fe%3D1387433954%26ri%3D1024%26rs%3D85%26h%3Db202ebde14bb68beed9f3ba8313ee52d");
            var a = test;
        }

        [TestMethod]
        public void XScrap()
        {
            var s = new XVideoScraper();
            var videos = s.ScrapeVideos();
            foreach (var vid in videos.Videos)
            {
                s.ScrapeVideoDetails(vid);
            }
        }


        [TestMethod]
        public void DataUpdate()
        {
            using (var db = new MovieFinderEntities())
            {
                var items = db.MovieLinks.Where(x => x.LinkTitle.Contains("part") && x.PartID == null).ToList();

                
                int id = 0;
                foreach (var item in items)
                {
                    if(id == item.MovieID)
                        continue;
                    id = item.MovieID;
                    var currentItems = items.Where(x => x.MovieID == id).ToList();
                    var partId = 0;
                    foreach (var cItem in currentItems)
                    {
                       var index = Convert.ToInt32(Regex.Match(cItem.LinkTitle, @"\d+").Value);
                       if (index == 1)
                           partId = cItem.ID;
                       cItem.PartID = partId;
                       cItem.PartIndex = index;
                       var k =0;
                       while (k++ < 3)
                       {
                           db.SaveChanges();
                           break;
                       }
                    }

                }
            }
        }
    }
}
