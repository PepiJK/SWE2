using System;
using System.Collections.Generic;
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

        [Test]
        public void ShouldSavePicturesFromDir()
        {
            var dirs = new List<string>
            {
                "C:\\Users\\josef\\Pictures\\Pallas Athene.png",
                "C:\\Users\\josef\\Pictures\\Bildnis der Sonja Knips.png",
                "C:\\Users\\josef\\Pictures\\Nuda Veritas.jpg"
            };

            var pictures = _bl.SavePicturesFromDir(dirs);
            var allPictures = _bl.GetPictures().ToList();
            
            Assert.That(allPictures.Count, Is.EqualTo(6));
        }

        [Test]
        public void ShouldNotAddAnAlreadyExistingPicture()
        {
            var dirs = new List<string>
            {
                "C:\\Users\\wally\\Pictures\\Bauerngarten mit Sonnenblumen.png"
            };
            
            var pictures = _bl.SavePicturesFromDir(dirs);
            var allPictures = _bl.GetPictures().ToList();
            
            Assert.That(allPictures.Count, Is.EqualTo(3));
        }
        
        [Test]
        public void ShouldNotAddAPictureWithWrongType()
        {
            var dirs = new List<string>
            {
                "C:\\Users\\josef\\Pictures\\Pallas Athene.txt",
                "C:\\Users\\josef\\Pictures\\Bildnis der Sonja Knips.pdf",
                "C:\\Users\\josef\\Pictures\\Nuda Veritas.ogg"
            };
            
            var pictures = _bl.SavePicturesFromDir(dirs);
            var allPictures = _bl.GetPictures().ToList();
            
            Assert.That(allPictures.Count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldDeletePhotographer()
        {
            var photographer = _bl.GetPhotographers().First();
            _bl.Delete(photographer);
            var photographers = _bl.GetPhotographers().ToList();
            
            Assert.That(photographers.Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldDeletePicture()
        {
            var picture = _bl.GetPictures().First();
            _bl.Delete(picture);
            var pictures = _bl.GetPictures().ToList();
            
            Assert.That(pictures.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetPhotographers()
        {
            var searchString = "wal";
            var photographer = _bl.GetPhotographers().First();
            var photographers = _bl.GetPhotographers(searchString).ToList();
            
            Assert.That(photographers.Count, Is.EqualTo(1));
            Assert.That(photographers.First(), Is.EqualTo(photographer));
        }
    }
}