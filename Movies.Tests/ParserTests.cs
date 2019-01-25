using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Movies.Api.Models;
using Movies.Api.Services.Interfaces;
using Movies.Api.Services.Logic;
using NUnit.Framework;

namespace Movies.Tests
{
  [TestFixture]
  public class ParserTests
  {
    [Test]
    public async Task Test()
    {
      var vkontakteClient = new VkontakteClient();
      var accountManager = new Mock<IAccountManager>();
      accountManager.Setup(t => t.Get(It.IsAny<Role>())).ReturnsAsync(new List<Account>
      {
        new Account
        {
          AccountId = 14624192,
          Role = Role.Admin
        }
      });
      var parser = new Parser(vkontakteClient, accountManager.Object);
      await parser.Start();
    }
  }
}
