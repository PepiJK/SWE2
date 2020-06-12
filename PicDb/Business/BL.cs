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
            //TODO: Check if picture already exists in db (compare with FullPath)
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
                        Directory = dir.Substring(0, dir.Length - filename.Length)
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