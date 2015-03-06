using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTube.Viewer
{
    public class GlobalSettings
    {
        public string DefaultLanguage{get;set;}
        public DateTime DBVersionCheckedTime { get; set; }
        public DateTime AppVersionCheckedTime { get; set; }
        public bool PlayFirstLink { get; set; }
        //public PlayerType Player { get; set; }
    }

    public enum PlayerType
    {
        WMP = 0,
        VLC
    }
}
