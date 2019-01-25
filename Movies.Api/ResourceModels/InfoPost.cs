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
  }
}