using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieTube.Client.Scraper;
using System.Text.RegularExpressions;

namespace MovieFinder.Test
{
    [TestClass]
    public class MovieTubeTest
    {

        [TestMethod]
        public void EithusanTest()
        {
            var s = new Einthusan().Scrape("http://www.einthusan.com/movies/watch.php?hindimoviesonline=Goliyon+Ki+Raasleela+Ram+Leela&lang=hindi&id=2277");
        }

        [TestMethod]
        public void PutlockerTest()
        {
            var s = new FireDrive().Scrape("http://www.firedrive.com/file/BA9F7CBB68CE6602");
        }


        [TestMethod]
        public void VidToTest()
        {
            var s = new Vidto().Scrape("http://vidto.me/mgvcmy83vvsx.html");
        }

        [TestMethod]
        public void MuchShareTest()
        {
            var s = new MuchShare().Scrape("http://muchshare.net/k23neiaypm53");
        }

        [TestMethod]
        public void NowVideoTest()
        {
            //var s = new NowVideo().Scrape("http://www.nowvideo.sx/video/5daecd0fa0fa5");
            var s = new NowVideo().Scrape("http://www.nowvideo.ch/video/xg6egl7fjb4zr");

            
        }

        [TestMethod]
        public void DivxStageTest()
        {
            var s = new DivxStage().Scrape("http://www.divxstage.eu/video/b156da1e00fa3");
        }

        [TestMethod]
        public void FlashXTest()
        {
            var s = new FlashX().Scrape("http://flashx.tv/video/YH5U0WGJ6WTHVEO/");
        }

        [TestMethod]
        public void SockShareTest()
        {
            var s = new SockShare().Scrape("http://www.sockshare.com/embed/413BBFA5534651BD");
        }

        [TestMethod]
        public void NovamovTest()
        {
            //var s = new Novamov().Scrape("http://www.novamov.com/video/8dd7bc903b733");
            var s = new Novamov().Scrape("http://embed.novamov.com/embed.php?width=600&height=480&v=b3e32385d7b10&px=1");
            
        }

        [TestMethod]
        public void VideoweedTest()
        {
            var s = new VideoWeed().Scrape("http://www.videoweed.es/file/a40344bcb892d");
        }

        [TestMethod]
        public void DivxStreamTest()
        {
            var s = new DivxStream().Scrape("http://divxstream.net/b1515secdlx3");
        }


        [TestMethod]
        public void NosVideoTest()
        {
            var s = new NosVideo().Scrape("http://nosvideo.com/?v=pioscqeaf38k");
        }


        [TestMethod]
        public void MovReelTest()
        {
            var s = new MovReel().Scrape("http://movreel.com/8jm0u6k9w4oo");
        }


        [TestMethod]
        public void MovShareTest()
        {
            var s = new MovShare().Scrape("http://embed.movshare.net/embed.php?v=519639752ea04");
        }

        [TestMethod]
        public void LoboVideoTest()
        {
            var s = new LoboVideo().Scrape("http://lobovideo.com/8ecagr01a59d");
        }

          [TestMethod]
        public void YoutubeTest()
        {
            var s = new Youtube().Scrape("http://www.youtube.com/watch?v=pJ9_K2yYd9w");
        }

        [TestMethod]
        public void CloudyTest()
        {
            var s = new Cloudy().Scrape("http://www.cloudy.ec/embed.php?id=26332efa74de3");
        }
       

         [TestMethod]
        public void VeohTest()
        {
            var s = new Veoh().Scrape("http://www.veoh.com/watch/v29762839fyctcHHE");
        }


         [TestMethod]
         public void StageVuTest()
         {
             var s = new StageVu().Scrape("http://stagevu.com/video/ubxqpqqqrwml");
         }

         [TestMethod]
         public void PlayedTest()
         {
             var s = new Played().Scrape("http://played.to/embed-vadzbjzqsuw5-640x360.html");
         }

         [TestMethod]
         public void DwnTest()
         {
             var s = new Dwn().Scrape("http://dwn.so/xml/videolink.php?v=DS92AF22AC");
         }

         [TestMethod]
         public void HostingBulkTest()
         {
             var s = new HostingBulk().Scrape("http://hostingbulk.com/embed-9fb0ommwq1ji-640x344.html");
         }


         [TestMethod]
         public void MovZapTest()
         {
             var s = new HostingBulk().Scrape("http://movzap.com/embed/k1r8lzqizhiq");
         }

         [TestMethod]
         public void VkTest()
         {
             var s = new Vk().Scrape("http://vk.com/video_ext.php?oid=258654113&id=168999542&hash=44bb819f0e3eb586&hd=1");
         }

         [TestMethod]
         public void GorillaVidTest()
         {
             var s = new GorillaVid().Scrape("http://gorillavid.in/gybpcv1kkshv");
         }

        
         [TestMethod]
         public void VodLockerTest()
         {
             var s = new VodLocker().Scrape("http://vodlocker.com/1pewd2yx6q4y");
         }


         [TestMethod]
         public void TheVideoTest()
         {
             var s = new TheVideo().Scrape("http://www.thevideo.me/laxehj48qjum");
         }

         [TestMethod]
         public void VideoRajTest()
         {
             var s = new VideoRaj().Scrape("http://www.videoraj.ch/embed.php?id=702a0588590ca");
         }
        
        

       
        
    }
}
