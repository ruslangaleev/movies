using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.Models
{
  public class Account
  {
    public Guid Id { get; set; }

    public int AccountId { get; set; }

    public Role Role { get; set; }
  }

  public enum Role
  {
    Admin = 0,
    Moderator = 1
  }
}