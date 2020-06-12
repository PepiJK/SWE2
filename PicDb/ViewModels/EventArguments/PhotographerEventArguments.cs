using System;
using PicDb.Models;

namespace PicDb.ViewModels.EventArguments
{
    /// <summary>
    /// Event handler arguments for the photographers list vm to notify parent that a new photographer has been selected.
    /// </summary>
    public class PhotographerEventArgs : EventArgs
    {
        /// <summary>
        /// The selected photographer in the photographer list.
        /// </summary>
        public Photographer Photographer { get; set; }
    }
}