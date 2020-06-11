using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    }
}