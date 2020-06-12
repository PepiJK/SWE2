using System.Collections.Generic;
using PicDb.Models;

namespace PicDb.ViewModels.EventArguments
{
    /// <summary>
    /// Event handler arguments for the main window vm to notify the picture list vm of the loaded pictures.
    /// </summary>
    public class PicturesEventArguments
    {
        /// <summary>
        /// The loaded pictures from the directory.
        /// </summary>
        public List<Picture> Pictures{ get; set; }
    }
}