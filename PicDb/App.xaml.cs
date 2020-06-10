using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using PicDb.Data;
using PicDb.Models;

namespace PicDb
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static readonly ILog _log = LogManager.GetLogger(typeof(App));
		private readonly IDAL _dal = DALFactory.GetDAL();

		protected override void OnStartup(StartupEventArgs e)
		{
			_log.Info("=============  PicDb Start  =============");
			_dal.Initialize();

			
			// DAL Tests
			
			_dal.Save(new Photographer
			{
				Firstname = "Josef",
				Lastname = "Koch",
				Birthdate = DateTime.Today,
				Notes = "fdalfj\rdkafs\rdfksjaflk"
			});

			/*
			var photographers = _dal.GetPhotographers();

			_dal.Save(new Picture
			{
				Filename = "test2.jpg",
				Directory = "test/test2.jpg",
				Exif = new Exif
				{
					Model = "Canon",
					Lens = "guad",
					FocalLength = 10
				},
				PhotographerId = photographers.First().Id
			});
			
			var pics = _dal.GetPictures();

			_dal.Update(new Photographer
			{
				Id = photographers.First().Id,
				Firstname = "Pepi",
				Birthdate = DateTime.Now
			});

			var photographers2 = _dal.GetPhotographers();

			_dal.Delete(photographers.First());

			var photographers3 = _dal.GetPhotographers();
			*/

			base.OnStartup(e);
		}
	}
}
