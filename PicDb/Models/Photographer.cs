using System;
using System.Collections.Generic;
using System.Text;

namespace PicDb.Models
{
	public class Photographer
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public DateTime? Birthdate { get; set; }
		public string Notes { get; set; }

		public List<Picture> Pictures { get; set; }
	}
}
