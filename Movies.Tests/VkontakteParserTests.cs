using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Api.Services.Logic;
using NUnit.Framework;

namespace Movies.Tests
{
  [TestFixture]
  public class VkontakteParserTests
  {
    [Test]
    public async Task Test()
    {
      var vkontakteParser = new VkontakteParser(58170807);
      var result = await vkontakteParser.GetInfoPost(10);

      Assert.AreEqual(10, result.Count());
    }
  }
}
