using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MovieTube.Web.Models
{
    public class ThumbNailVm
    {
        public ThumbNailVm()
        {
            Thumbnails = new List<MovieThumbnailVm>();
        }
        public List<MovieThumbnailVm> Thumbnails { get; set; }
        public int NextPage { get; set; }
    }

    public class MovieThumbnailVm
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string PostedDate { get; set; }
        public string PostedBy { get; set; }
        public string Language { get; set; }
        public int ReleasedYear { get; set; }
    }

    public class MovieVm : MovieThumbnailVm
    {
        public MovieVm()
        {
            Links = new List<VideoLinkVm>();
        }
        public string Description { get; set; }
        public List<VideoLinkVm> Links { get; set; }
        public VideoLinkVm Active { get { return Links[0]; } }

         public string JsonLink
        {
            get
            {
                //[{"title":"title1","file":"httpLink"},{"title":"title2","file":"httpLink","captions.file":"httpSub.srt"}]
                var links = new List<JsonVm>();
                foreach(var v in Links)
                    links.Add(new JsonVm { file = v.Url, title = v.Title });
                return new JavaScriptSerializer().Serialize(links);
            }
        }

         class JsonVm
         {
             public string title { get; set; }
             public string file { get; set; }
         }
    }

    public class VideoLinkVm : IComparable<VideoLinkVm>
    {
        private string url;
        public int ID { get; set; }
        public string Title { get; set; }
        public string HostSite { get; set; }
        public string Url {
            get
            {
                return this.url;
            }
            set
            {
                if (value != null)
                {
                    this.url = value;// value.Replace("&", "%26");
                }
            }
        }

        public int? PartID { get; set; }
        public int? PartIndex { get; set; }

        public int CompareTo(VideoLinkVm other)
        {
            if(this.PartID == null && other.PartID == null)
                return other.ID.CompareTo(this.ID);
            if (this.PartID == null && other.PartID != null)
                return -1;
            if (this.PartID != null && other.PartID == null)
                return 1;
            if (this.PartID == other.PartID)
                return this.PartIndex.Value.CompareTo(other.PartIndex.Value);

            return other.ID.CompareTo(this.ID);
        }
    }

    public class SearchResult
    {
        public string name { get; set; }
        public string id { get; set; }
    }
}