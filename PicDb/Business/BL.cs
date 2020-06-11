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

        public void SavePicturesFromDir(IEnumerable<string> dirs)
        {
            foreach (var dir in dirs)
            {
                var subStringFile = dir.Split('.');
                var subStringFilename = dir.Split('\\');
                var filename = subStringFilename.Last();
                var type = subStringFile.Last();

                if (type == "png" || type == "gif" || type == "jpg" || type == "jpeg")
                {
                    Picture picture = new Picture();
                    picture.Filename = filename;
                    picture.Directory = dir;
                    _dal.Save(picture);
                }
            }
        }

        public void Save(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Save(photographer);
        }

        public void UpdatePhotographer(Photographer photographer)
        {
            CheckPhotographerValidity(photographer);
            _dal.Update(photographer);
        }

        private void CheckPhotographerValidity(Photographer photographer)
        {
            if (photographer == null) throw new NullReferenceException();
            if (string.IsNullOrWhiteSpace(photographer.Firstname)) throw new Exception("Firstname is empty.");
            if (string.IsNullOrWhiteSpace(photographer.Lastname)) throw new Exception("Lastname is empty");
            if (photographer.Birthdate.HasValue && photographer.Birthdate > DateTime.Now) throw new Exception("Birthdate is not in the past.");
        }
    }
}