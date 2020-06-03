using System.Collections.Generic;
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
    }
}