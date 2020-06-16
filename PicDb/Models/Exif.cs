using System;
using System.Collections.Generic;
using System.Text;

namespace PicDb.Models
{
	public class Exif
	{
		public int Id { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public int? FocalLength { get; set; }
		public DateTime? DateTimeOriginal { get; set; }
	}
}
