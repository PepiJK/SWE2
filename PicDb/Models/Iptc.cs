using System;
using System.Collections.Generic;
using System.Text;

namespace PicDb.Models
{
    public class Iptc
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Keywords { get; set; }
        public string Credit { get; set; }
        public string Copyright { get; set; }

        public int PictureId { get; set; }
        public Picture Picture { get; set; }
    }
}
