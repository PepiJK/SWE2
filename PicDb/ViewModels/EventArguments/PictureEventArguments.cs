using System;
using PicDb.Models;

namespace PicDb.ViewModels.EventArguments
{
    public class PictureEventArguments : EventArgs
    {
        
        public Picture Picture { get; set; }
    }
}