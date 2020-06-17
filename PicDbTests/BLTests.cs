using System;
using System.Linq;
using System.Security.Principal;
using NUnit.Framework;
using PicDb.Business;
using PicDb.Data;
using PicDb.Models;

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
            Assert.That(photographers.Count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldGetAllPictures()
        {
            var pictures = _bl.GetPictures().ToList();
            Assert.That(pictures, Is.Not.Null);
            Assert.That(pictures.Count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldSavePhotographer()
        {
            var newPhotographer = new Photographer
            {
                Firstname = "Max",
                Lastname = "Mustermann"
            };
            
            _bl.Save(newPhotographer);
            var blPhotographer = _bl.GetPhotographers().Last();
            
            Assert.That(blPhotographer, Is.Not.Null);
            Assert.That(blPhotographer.Id, Is.EqualTo(4));
            Assert.That(blPhotographer.Firstname, Is.EqualTo(newPhotographer.Firstname));
            Assert.That(blPhotographer.Lastname, Is.EqualTo(newPhotographer.Lastname));
            Assert.That(blPhotographer.Birthdate, Is.EqualTo(newPhotographer.Birthdate));
        }

        [Test]
        public void ShouldThrowExceptionOnSaveInvalidPhotographer()
        {
            Photographer newPhotographer = null;
            Assert.Throws<ArgumentNullException>(() => _bl.Save(newPhotographer));
            
            newPhotographer = new Photographer
            {
                Lastname = "Mustermann"
            };
            var ex = Assert.Throws<Exception>(() => _bl.Save(newPhotographer));
            Assert.That(ex.Message, Is.EqualTo("Firstname is empty."));
            
            newPhotographer = new Photographer
            {
                Firstname = "Max",
                Lastname = ""
            };
            ex = Assert.Throws<Exception>(() => _bl.Save(newPhotographer));
            Assert.That(ex.Message, Is.EqualTo("Lastname is empty."));
            
            newPhotographer = new Photographer
            {
                Firstname = "Max",
                Lastname = "Mustermann",
                Birthdate = DateTime.Now.AddDays(1)
            };
            ex = Assert.Throws<Exception>(() => _bl.Save(newPhotographer));
            Assert.That(ex.Message, Is.EqualTo("Birthdate is not in the past."));

            var photographers = _bl.GetPhotographers().ToList();
            Assert.That(photographers, Is.Not.Null);
            Assert.That(photographers.Count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldUpdatePicture()
        {
            var picture = _bl.GetPictures().First();
            Assert.That(picture, Is.Not.Null);

            picture.Filename = "newFile.png";
            picture.Exif.Model = "PowerShot G7 X Mark III";
            
            _bl.Update(picture);
            var updatedPicture = _bl.GetPictures().First();
            
            Assert.That(updatedPicture, Is.Not.Null);
            Assert.That(updatedPicture, Is.EqualTo(picture));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionOnUpdateInvalidPicture()
        {
            Picture nullPicture = null;
            Assert.Throws<ArgumentNullException>(() => _bl.Update(nullPicture));
        }

        [Test]
        public void ShouldMockExifIptcOnUpdatePictureWithNoExifIptc()
        {
            var picture = _bl.GetPictures().Last();
            Assert.That(picture, Is.Not.Null);
            Assert.That(picture.ExifId, Is.Null);
            Assert.That(picture.Exif, Is.Null);
            Assert.That(picture.IptcId, Is.Null);
            Assert.That(picture.Iptc, Is.Null);
            
            _bl.Update(picture);

            var updatedPicture = _bl.GetPictures().Last();
            Assert.That(updatedPicture, Is.Not.Null);
            Assert.That(picture.ExifId, Is.Not.Null);
            Assert.That(picture.Exif, Is.Not.Null);
            Assert.That(picture.IptcId, Is.Not.Null);
            Assert.That(picture.Iptc, Is.Not.Null);
        }
    }
}