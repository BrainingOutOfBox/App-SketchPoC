using NUnit.Framework;
using SketchPoC.Dal;

namespace SketchPoC.Tests
{
    [TestFixture]
    public class DownloadImageTest
    {
        [Test]
        [TestCase("5c821e8d8bba563a83029aac")]
        public void TestDownload(string fileId)
        {
            IDalService service = new RestDalService();
            var stream = service.Download(fileId).Result;
            Assert.NotNull(stream);
            Assert.IsTrue(stream.Length > 0);
        }
    }
}
