using PicDb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicDb.Data
{
    class DALMock : IDAL
    {
        public void Delete(Photographer photographer)
        {
            throw new NotImplementedException();
        }

        public void Delete(Picture picture)
        {
            throw new NotImplementedException();
        }

        public Picture GetPicture(int id)
        {
            throw new NotImplementedException();
        }

        public Photographer GetPhotographer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photographer> GetPhotographers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photographer> GetPhotographers(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Picture> GetPictures()
        {
            throw new NotImplementedException();
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
