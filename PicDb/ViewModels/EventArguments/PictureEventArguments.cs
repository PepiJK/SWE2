using System;
using PicDb.Models;

namespace PicDb.ViewModels.EventArguments
{
    /// <summary>
    /// Event handler arguments for the pictures list vm to notify parent that a new picture has been selected.
    /// </summary>
    public class PictureEventArguments : EventArgs
    {
        /// <summary>
        /// The selected picture in the pictures list.
        /// </summary>
        public Picture Picture { get; set; }
    }
}