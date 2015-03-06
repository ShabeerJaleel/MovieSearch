using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using BrightIdeasSoftware;
using System.Drawing;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using Newtonsoft.Json;

namespace MovieFinder.Client
{
    public class Movie 
    {
        public static string ImgDirectory = "Images";
        private static Dictionary<string, string> langauges = new Dictionary<string, string>();
        

        static Movie()
        {
            langauges.Add("ml", "Malayalam");
            langauges.Add("en", "English");
            langauges.Add("hi", "Hindi");
            langauges.Add("te", "Telugu");
            langauges.Add("ta", "Tamil");
        }

        public Movie()
        {
        }

        [JsonIgnore]
        public string ImageID { 
            get {
                if (String.IsNullOrEmpty(ImageUrl))
                    return String.Empty;
            return GenerateShortID(ImageUrl);
        } }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string Year
        {
            get
            {
                if (ReleasedDate != null)
                    return ReleasedDate.Value.Year.ToString();
                return String.Empty;
            }
        }
        [JsonIgnore]
        public string Language {
            get
            {
               
                 if (langauges.ContainsKey(LCode))
                       return langauges[LCode];
                    return LCode.ToString();
               
            }
        
        
         }
        public string Description { get; set; }
        public List<MovieLink> Links { get; set; }
        [JsonIgnore]
        public MovieLink Link1 { 
            get 
                {
                    return GetLink(1);
                   
                } 
        }
        [JsonIgnore]
        public MovieLink Link2
        {
            get
            {
                return GetLink(2);
            }
        }
        [JsonIgnore]
        public MovieLink Link3
        {
            get
            {
                return GetLink(3);
            }
        }
        [JsonIgnore]
        public MovieLink Link4
        {
            get
            {
                return GetLink(4);
            }
        }
        [JsonIgnore]
        public MovieLink Link5
        {
            get
            {
                return GetLink(5);
            }
        }
        public string LCode { get; set; }
        [JsonIgnore]
        public string ImagePath
        {
            get
            {
                if (String.IsNullOrEmpty(ImageUrl))
                    return Path.Combine(Application.StartupPath, "no_image.gif");
                return Path.Combine(ImgDirectory, ImageID);
            }
        }
        public DateTime CreatedDate { get; set; }
        public EntryChange EntryStatus { get; set; }
        public int Version { get; set; }
        
        private static string GenerateShortID(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private MovieLink GetLink(int pos)
        {
            if (Links.Count < pos)
            {
               
            for(int i = Links.Count; i < pos;i++)
                Links.Add(new MovieLink());
            }

            return Links[pos - 1];

        }
    }

    public class MovieLink
    {
        //public MovieLink (string url, string title)
        //{
        //    Url = url;
        //    Title = title;

        //}
        public MovieLink()
        {
            Url = String.Empty;
            Title = String.Empty;
        }
        public string Url { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }

    public enum EntryChange
    {
        EntryAdded = 0,
        EntryUpdated = 1
    }


}
