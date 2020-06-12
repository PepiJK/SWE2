using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PicDb.Data
{
	/// <summary>
	/// DAL Factory for providing an IDAL instance.
	/// </summary>
	public class DALFactory
	{
		/// <summary>
		/// Return an IDAL instance which is specified in the app settings.
		/// </summary>
		/// <returns>IDAL instance</returns>
		public static IDAL GetDAL()
		{
			string dal = ConfigurationManager.AppSettings["DAL"];
			Type dalType = Type.GetType(dal);
			return (IDAL)Activator.CreateInstance(dalType);
		}
	}
}
