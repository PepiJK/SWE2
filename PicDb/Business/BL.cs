using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Automation;
using PicDb.Data;
using PicDb.Models;

namespace PicDb.Business
{
    public class BL
    {
        private readonly IDAL _dal = DALFactory.GetDAL();

        public IEnumerable<Photographer> GetPhotographers()
        {
            return _dal.GetPhotographers();
        }

        public IEnumerable<Picture> GetPictures()
        {
            return _dal.GetPictures();
        }

        public IEnumerable<Picture> SavePicturesFromDir(IEnumerable<string> dirs)
        {
            var pictures = new List<Picture>();
            foreach (var dir in dirs)
            {
                var subStringFile = dir.Split('.');
                var subStringFilename = dir.Split('\\');
                var filename = subStringFilename.Last();
                var type = subStringFile.Last();

                if (type == "png" || type == "gif" || type == "jpg" || type == "jpeg")
                {
                    var picture = new Picture
                    {
                        Filename = filename,
                        Directory = dir.Substring(0, dir.Length - filename.Length),
                        Exif = MockExif(),
                        Iptc = MockIptc()
                    };

                    if (_dal.PictureExists(picture))
                    {
                        picture = _dal.GetPicture(picture.Directory, picture.Filename);
                    }
                    else
                    {
                        _dal.Save(picture);
                    }

                    pictures.Add(picture);
                }
            }

            return pictures;
        }

        public void Save(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Save(photographer);
        }

        public void Update(Picture picture)
        {
            if (picture == null) throw new ArgumentNullException();
            picture.Exif ??= MockExif();
            picture.Iptc ??= MockIptc();
            _dal.Update(picture);
        }

        public void Update(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Update(photographer);
        }

        public void Delete(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Delete(photographer);
        }

        public void Delete(Picture picture)
        {
            if (picture == null) throw new ArgumentNullException();
            _dal.Delete(picture);
        }

        public IEnumerable<Photographer> GetPhotographers(string searchString)
        {
            if (searchString == null) throw new ArgumentNullException();
            return _dal.GetPhotographers(searchString);
        }

        private void CheckPhotographerValidity(Photographer photographer)
        {
            if (photographer == null) throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(photographer.Firstname)) throw new Exception("Firstname is empty.");
            if (string.IsNullOrWhiteSpace(photographer.Lastname)) throw new Exception("Lastname is empty.");
            if (photographer.Birthdate.HasValue && photographer.Birthdate > DateTime.Now)
                throw new Exception("Birthdate is not in the past.");
        }

        private Exif MockExif()
        {
            var random = new Random();
            var mockExif = (random.Next() % 3) switch
            {
                0 => new Exif
                {
                    Manufacturer = "Canon",
                    Model = "PowerShot G7 X Mark III",
                    FocalLength = 120,
                    DateTimeOriginal = new DateTime(1944, 6, 6)
                },
                1 => new Exif
                {
                    Manufacturer = "Nikon",
                    Model = "D6",
                    FocalLength = 200,
                    DateTimeOriginal = new DateTime(1997, 10, 11)
                },
                2 => new Exif
                {
                    Manufacturer = "Olympus",
                    Model = "OM-D",
                    FocalLength = 150,
                    DateTimeOriginal = new DateTime(1997, 10, 26)
                },
                _ => throw new Exception("Modulo 3 returned something other than 0, 1 or 2.")
            };

            return mockExif;
        }

        private Iptc MockIptc()
        {
            var random = new Random();
            var mockIptc = (random.Next() % 3) switch
            {
                0 => new Iptc
                {
                    Caption = "Meisterwerk",
                    Keywords = "exzellent, großartig",
                    Credit = "Josef Koch",
                    Copyright = "FH Technikum Wien"
                },
                1 => new Iptc
                {
                    Caption = "Gottgleiches Photo",
                    Keywords = "gott, von jesus himself",
                    Credit = "Thomas Wally",
                    Copyright = "FH Technikum Wien"
                },
                2 => new Iptc
                {
                    Caption = "Der Schrei", 
                    Keywords = "geschrei, expressionissmus", 
                    Credit = "Edvard Munch"
                },
                _ => throw new Exception("Modulo 3 returned something other than 0, 1 or 2.")
            };

            return mockIptc;
        }
    }
}