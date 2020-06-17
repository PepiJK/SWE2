namespace PicDb.Models
{
	public class Picture
	{
		public int Id { get; set; }
		public string Filename { get; set; }
		public string Directory { get; set; }

		public int? IptcId { get; set; }
		public Iptc Iptc { get; set; }
		public int? ExifId { get; set; }
		public Exif Exif { get; set; }
		public int? PhotographerId { get; set; }
		public Photographer Photographer { get; set; }

		public string FullPath => Directory + Filename;
	}
}
