using System;
using PicDb.Models;

namespace PicDb.ViewModels.EventArguments
{
    public class PhotographerEventArgs : EventArgs
    {
        public Photographer Photographer { get; set; }
    }
}