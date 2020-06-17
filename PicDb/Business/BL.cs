using System;
using System.Collections.Generic;
using System.Linq;
using PicDb.Data;
using PicDb.Models;

namespace PicDb.Business
{
    /// <summary>
    /// Business layer class which contains methods to access the data access layer.
    /// </summary>
    public class BL
    {
        private readonly IDAL _dal = DALFactory.GetDAL();
        
        /// <summary>
        /// Return all photographers from dal.
        /// </summary>
        /// <returns>Photographers as IEnumerable.</returns>
        public IEnumerable<Photographer> GetPhotographers()
        {
            return _dal.GetPhotographers();
        }

        /// <summary>
        /// Returns all pictures from dal.
        /// </summary>
        /// <returns>Pictures as IEnumerable.</returns>
        public IEnumerable<Picture> GetPictures()
        {
            return _dal.GetPictures();
        }
        
        /// <summary>
        /// Returns all pictures from the dal which match the passed search string.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Pictures as IEnumerable.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Photographer> GetPhotographers(string searchString)
        {
            if (searchString == null) throw new ArgumentNullException();
            return _dal.GetPhotographers(searchString);
        }

        /// <summary>
        /// Loops through every string and either saves pictures in the dal if it does not already exist
        /// or just gets the picture from dal based on its directory and filename and append it to a list and returns it.
        /// </summary>
        /// <param name="dirs"></param>
        /// <returns>IEnumerable of Pictures</returns>
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

                    if (!_dal.PictureExists(picture))
                    {
                        _dal.Save(picture);
                    }
                    
                    picture = _dal.GetPicture(picture.Directory, picture.Filename);
                    pictures.Add(picture);
                }
            }

            return pictures;
        }

        /// <summary>
        /// Checks the validity of the passed photographer and save it to the dal if its valid.
        /// </summary>
        /// <param name="photographer"></param>
        public void Save(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Save(photographer);
        }

        /// <summary>
        /// Updates a passed picture in the dal and adds mocked exif and iptc data if it is not set already.
        /// </summary>
        /// <param name="picture"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Update(Picture picture)
        {
            if (picture == null) throw new ArgumentNullException();
            picture.Exif ??= MockExif();
            picture.Iptc ??= MockIptc();
            _dal.Update(picture);
        }

        /// <summary>
        /// Checks the validity of the passed photographer and updates it in the dal if its valid.
        /// </summary>
        /// <param name="photographer"></param>
        public void Update(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Update(photographer);
        }

        /// <summary>
        /// Checks the validity of the passed photographer and deletes it in the dal if its valid.
        /// </summary>
        /// <param name="photographer"></param>
        public void Delete(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Delete(photographer);
        }

        /// <summary>
        /// Deletes a given picture in the dal.
        /// </summary>
        /// <param name="picture"></param>
        public void Delete(Picture picture)
        {
            if (picture == null) throw new ArgumentNullException();
            _dal.Delete(picture);
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