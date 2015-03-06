using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTube.Client.Scraper
{
    public class Language
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static List<Language> Languages = new List<Language>{
            new Language{ Id = "", Name ="(Language)"},
            new Language{Id = "hi", Name="Hindi"},
            new Language{Id = "ml", Name="Malayalam"},
            new Language{Id = "ta", Name = "Tamil"},
            new Language{Id = "te", Name="Telugu"}
        };
    }

   
}
