using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Web.Hosting;

namespace MovieFinder.Web
{
    public class ScheduleService
    {
        private static string url = "http://www.davidlloyd.co.uk/services/webdata.svc/timetables/search?clubno={0}&startdate={1}&enddate={2}&time=All";
        private static List<string> activities = new List<string>();
        public static Dictionary<int, string> clubs = new Dictionary<int, string>();

        static ScheduleService()
        {
            Init();
        }

        public static List<Activity> GetActivities(DateTime start, DateTime end)
        {
            var data = new List<Activity>();
            foreach (var dic in clubs)
            {
                data.AddRange(GetActivities(dic.Key, start, end));
            }

            data = data.Where(x => (activities.Count == 0 || activities.Contains(x.ActivityTitle.ToLower()))).ToList();
            return data.Where(x =>
                ((IsWeekend(x.Date) &&
                (x.StartTime.Hour >= 6 && x.StartTime.Minute >= 0) &&
                (x.StartTime.Hour <= 22 && x.StartTime.Minute >= 0)) ||
                (!IsWeekend(x.Date) &&
                (x.StartTime.Hour <= 8 && x.StartTime.Minute <= 0) ||
                (x.StartTime.Hour >= 18 && x.StartTime.Minute >= 0))))
                .OrderBy(x => x.StartTime).ToList();
        }
        private static  List<Activity> GetActivities(int clubNo, DateTime start, DateTime end)
        {
            using (var client = new WebClient())
            {
                var text = client.DownloadString(String.Format(url, clubNo,
                    start.ToString("dd/MM/yyyy").Replace("/", "%2F"),
                    end.ToString("dd/MM/yyyy").Replace("/", "%2F")));
                var data = new JavaScriptSerializer().Deserialize<List<Activity>>(text);
                foreach (var d in data)
                {
                    d.StartTime = new DateTime(d.Date.Year, d.Date.Month, d.Date.Day, d.StartTime.Hour, d.StartTime.Minute, 0);
                    d.EndTime = new DateTime(d.Date.Year, d.Date.Month, d.Date.Day, d.EndTime.Hour, d.EndTime.Minute, 0);
                }
                return data;
            }
        }

        private static bool IsWeekend(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday) ||
                   (date.DayOfWeek == DayOfWeek.Sunday);
        }

        private static void Init()
        {
            var f = HostingEnvironment.MapPath("~/activities.txt");
            activities = File.ReadAllLines(f).Select(x => x.ToLower()).ToList();

            f = HostingEnvironment.MapPath("~/clubs.txt");
            var cl = File.ReadAllLines(f);
            foreach (var c in cl)
                clubs.Add(Convert.ToInt32(c.Split(',')[0]), c.Split(',')[1]);
        }


    }
}