﻿using PicDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDb.Data
{
    public class DALMock : IDAL
    {
        private List<Picture> _pictures = new List<Picture>
        {
            new Picture
            {
                Id = 1,
                Directory = "C:\\Users\\wally\\Pictures\\",
                Filename = "Bauerngarten mit Sonnenblumen.png",
                ExifId = 1,
                IptcId = 1,
                PhotographerId = 1
            },
            
            new Picture
            {
                Id = 2,
                Directory = "C:\\Users\\wally\\Pictures\\",
                Filename = "Adele Bloch-Bauer I.png",
                ExifId = 2,
                IptcId = 2,
                PhotographerId = 2
            },
            
            new Picture
            {
                Id = 2,
                Directory = "C:\\Users\\wally\\Pictures\\",
                Filename = "Dame mit lila Schal.jpg",
                ExifId = 2,
                IptcId = 2,
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
            throw new NotImplementedException();
        }

        public void Delete(Picture picture)
        {
            throw new NotImplementedException();
        }

        public bool PictureExists(Picture picture)
        {
            throw new NotImplementedException();
        }

        public Picture GetPicture(int id)
        {
            return _pictures.FirstOrDefault(p => p.Id == id);
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Save(Picture pictures)
        {
            throw new NotImplementedException();
        }

        public void Update(Photographer photographer)
        {
            throw new NotImplementedException();
        }

        public void Update(Picture picture)
        {
            throw new NotImplementedException();
        }
    }
}
