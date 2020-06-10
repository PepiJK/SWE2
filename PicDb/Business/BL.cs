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

        public void Save(string[] dirs)
        {
            foreach (var dir in dirs)
            {
                string[] subStringFile = dir.Split('.');
                string[] subStringFilename = dir.Split('\\');
                string filename = subStringFilename.Last();
                string type = subStringFile.Last();

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