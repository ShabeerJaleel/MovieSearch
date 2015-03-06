using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieFinder.Web
{
    public class ScheduleModel
    {
        public ScheduleModel()
        {
            Dates = new List<DateTime>();
            for (var i = 0; i < 30; i++)
                Dates.Add(DateTime.Now.AddDays(i));
            Activities = new List<Activity>();
        }
        public List<DateTime> Dates { get; set; }
        public List<Activity> Activities { get; set; }
    }

    public class Activity
    {
        public string ActivityTitle { get; set; }
        public bool IsAdult { get; set; }
        public bool IsSeniorCitizen { get; set; }
        public string Location { get; set; }
        public string MoreInformation { get; set; }
        public float Price { get; set; }
        public int ClubNo { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }


        //display stuff
        public string DateText
        {
            get
            {
                return Date.ToShortDateString();
            }

        }

        public string StartTimeText
        {
            get
            {
                return StartTime.ToShortTimeString();
            }

        }

        public string EndTimeText
        {
            get
            {
                return EndTime.ToShortTimeString();
            }

        }

        public string Club
        {
            get
            {
                return ScheduleService.clubs[ClubNo];
            }
        }


    }
}