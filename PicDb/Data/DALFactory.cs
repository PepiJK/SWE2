using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PicDb.Data
{
	public class DALFactory
	{
		public static IDAL GetDAL()
		{
			string dal = ConfigurationManager.AppSettings["DAL"];
			Type dalType = Type.GetType(dal);
			return (IDAL)Activator.CreateInstance(dalType);
		}
	}
}
