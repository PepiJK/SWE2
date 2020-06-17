using System;

namespace PicDb.Models
{
	public class Photographer
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public DateTime? Birthdate { get; set; }
		public string Notes { get; set; }

		public string FullName => Firstname + " " + Lastname;
	}
}
