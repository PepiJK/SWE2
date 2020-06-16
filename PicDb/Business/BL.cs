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

                    if (!_dal.PictureExists(picture)) _dal.Save(picture);

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
            if (picture == null) throw new NullReferenceException();
            picture.Exif ??= MockExif();
            picture.Iptc ??= MockIptc();
            _dal.Update(picture);
        }

        public void Update(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Update(photographer);
        }

        private void CheckPhotographerValidity(Photographer photographer)
        {
            if (photographer == null) throw new NullReferenceException();
            if (string.IsNullOrWhiteSpace(photographer.Firstname)) throw new Exception("Firstname is empty.");
            if (string.IsNullOrWhiteSpace(photographer.Lastname)) throw new Exception("Lastname is empty");
            if (photographer.Birthdate.HasValue && photographer.Birthdate > DateTime.Now)
                throw new Exception("Birthdate is not in the past.");
        }

        private Exif MockExif()
        {
            Random random = new Random();
            Exif mockExif = null;
            
            switch (random.Next() % 3)
            {
                case 0:
                    mockExif = new Exif
                    {
                        Manufacturer = "Canon",
                        Model = "PowerShot G7 X Mark III",
                        FocalLength = 120,
                        DateTimeOriginal = new DateTime(1944, 6, 6)
                    };
                    break;
                case 1:
                    mockExif = new Exif
                    {
                        Manufacturer = "Nikon",
                        Model = "D6",
                        FocalLength = 200,
                        DateTimeOriginal = new DateTime(1997, 10, 11)
                    };
                    break;
                case 2:
                    mockExif = new Exif
                    {
                        Manufacturer = "Olympus",
                        Model = "OM-D",
                        FocalLength = 150,
                        DateTimeOriginal = new DateTime(1944, 10, 26)
                    };
                    break;
            }

            return mockExif;
        }

        private Iptc MockIptc()
        {
            Random random = new Random();
            Iptc mockIptc = null;
            
            switch (random.Next() % 3)
            {
                case 0:
                    mockIptc = new Iptc
                    {
                        Caption = "Meisterwerk",
                        Keywords = "exzellent, großartig",
                        Credit = "Josef Koch",
                        Copyright = "FH Technikum Wien"
                    };
                    break;
                case 1:
                    mockIptc= new Iptc
                    {
                        Caption = "Gottgleiches Photo",
                        Keywords = "gott, von jesus himself",
                        Credit = "Thomas Wally",
                        Copyright = "FH Technikum Wien"
                    };
                    break;
                case 2:
                    mockIptc = new Iptc
                    {
                        Caption = "Der Schrei",
                        Keywords = "geschrei, expressionissmus",
                        Credit = "Edvard Munch",
                        Copyright = ""
                    };
                    break;
            }

            return mockIptc;
        }
    }
}