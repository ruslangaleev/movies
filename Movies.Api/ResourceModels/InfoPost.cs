using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.ResourceModels
{
  public class InfoPost
  {
    public Response response { get; set; }
  }

  public class Response
  {
    public Item[] items { get; set; }
  }

  public class Item
  {
    public int id { get; set; }

    public string text { get; set; }

    public Attachments[] attachments { get; set; }
  }

    public class Attachments
    {
        public string type { get; set; }

        public Video video { get; set; }

        public Photo photo { get; set; }
    }

    public class Photo
    {
        public int id { get; set; }

        public int owner_id { get; set; }

        [Obsolete]
        public Size[] sizes { get; set; }

        public string access_key { get; set; }
    }

    public class Size
    {
        public string url { get; set; }
    }

    public class Video
    {
        public int id { get; set; }

        public int owner_id { get; set; }

        public string title { get; set; }

        public string access_key { get; set; }
    }
}