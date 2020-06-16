using System.Linq;
using System.Security.Principal;
using NUnit.Framework;
using PicDb.Business;
using PicDb.Data;

namespace PicDbTests
{
    public class BLTests
    {
        private BL _bl;
        
        [SetUp]
        public void Setup()
        {
            _bl = new BL();
        }

        [Test]
        public void ShouldGetAllPhotographers()
        {
            var photographers = _bl.GetPhotographers().ToList();
            Assert.That(photographers, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllPictures()
        {
            var pictures = _bl.GetPictures().ToList();
            Assert.IsNotNull(pictures);
        }
    }
}