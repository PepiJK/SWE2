using PicDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PicDb.Data
{
    /// <summary>
    /// Data access layer class for mocking a database.
    /// </summary>
    public class DALMock : IDAL
    {
        private int _currentPicId = 3;
        private int _currentPhotographerId = 3;
        private int _currentIptcId = 1;
        private int _currentExifId = 2;
        
        private List<Picture> _pictures = new List<Picture>
        {
            new Picture
            {
                Id = 1,
                Directory = "C:\\Users\\wally\\Pictures\\",
                Filename = "Bauerngarten mit Sonnenblumen.png",
                ExifId = 1,
                Exif = new Exif
                {
                    Id = 1,
                    Manufacturer = "Canon"
                },
                IptcId = 1,
                Iptc = new Iptc
                {
                    Id = 1,
                    Caption = "yoyo"
                },
                PhotographerId = 1
            },
            
            new Picture
            {
                Id = 2,
                Directory = "C:\\Users\\wally\\Pictures\\",
                Filename = "Adele Bloch-Bauer I.png",
                ExifId = 2,
                Exif = new Exif
                {
                  Id  = 2,
                  DateTimeOriginal = new DateTime(2020, 3, 1)
                },
                PhotographerId = 2
            },
            
            new Picture
            {
                Id = 3,
                Directory = "C:\\Users\\wally\\Pictures\\",
                Filename = "Dame mit lila Schal.jpg",
                PhotographerId = 2
            }
        };

        private List<Photographer> _photographers = new List<Photographer>
        {
            new Photographer
            {
                Id = 1,
                Birthdate = new DateTime(1997, 10, 11),
                Firstname = "Thomas",
                Lastname = "Wally",
                Notes = "hat sich eineinhalb Mal das Kreuzabnd gerissen"
            },
            
            new Photographer
            {
                Id = 2,
                Birthdate = new DateTime(1997, 10, 26),
                Firstname = "Josef",
                Lastname = "Koch",
                Notes = "seine Eltern sind Winzer"
            },
            
            new Photographer
            {
                Id = 3,
                Birthdate = new DateTime(1983, 4, 21),
                Firstname = "Gertrude",
                Lastname = "Wagner",
                Notes = "hatte bereits im Alter von fünf Jahren eine Kamera"
            }
        };


        public void Delete(Photographer photographer)
        {
            _photographers.Remove(photographer);
        }

        public void Delete(Picture picture)
        {
            _pictures.Remove(picture);
        }

        public bool PictureExists(Picture picture)
        {
            return _pictures.FirstOrDefault(p => p.FullPath == picture.FullPath) != null;
        }

        public Picture GetPicture(int id)
        {
            return _pictures.FirstOrDefault(p => p.Id == id);
        }

        public Picture GetPicture(string directory, string filename)
        {
            return _pictures.FirstOrDefault(p => p.FullPath == directory + filename);
        }

        public Photographer GetPhotographer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photographer> GetPhotographers()
        {
            return _photographers;
        }

        public IEnumerable<Photographer> GetPhotographers(string searchString)
        {
            var cleanedSearchString = searchString.ToLower().Trim();
            return _photographers.Where(p => p.FullName.ToLower().Trim().Contains(cleanedSearchString));
        }

        public IEnumerable<Picture> GetPictures()
        {
            return _pictures;
        }

        public IEnumerable<Picture> GetPictures(string searchString)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Save(Photographer photographer)
        {
            photographer.Id = ++_currentPhotographerId;
            _photographers.Add(photographer);
        }

        public void Save(Picture picture)
        {
            picture.Id = ++_currentPicId;
            _pictures.Add(picture);
        }

        public void Update(Photographer photographer)
        {
            _photographers.Remove(_photographers.FirstOrDefault(p => p.Id == photographer.Id));
            _photographers.Add(photographer);
            _photographers = _photographers.OrderBy(p => p.Id).ToList();
        }

        public void Update(Picture picture)
        {
            if (picture.ExifId == null && picture.Exif != null)
            {
                var exifId =  ++_currentExifId;
                picture.ExifId = exifId;
                picture.Exif.Id = exifId;
            }

            if (picture.IptcId == null && picture.Iptc != null)
            {
                var iptcId =  ++_currentIptcId;
                picture.IptcId = iptcId;
                picture.Iptc.Id = iptcId;
            }
            
            _pictures.Remove(_pictures.FirstOrDefault(p => p.Id == picture.Id));
            _pictures.Add(picture);
            _pictures = _pictures.OrderBy(p => p.Id).ToList();
        }
    }
}
