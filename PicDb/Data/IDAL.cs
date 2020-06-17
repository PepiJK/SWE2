using System.Collections.Generic;
using PicDb.Models;

namespace PicDb.Data
{
    /// <summary>
    /// Data access layer interface.
    /// </summary>
    public interface IDAL
    {
        /// <summary>
        /// Initializes the dal connection and creates the tables.
        /// </summary>
        public void Initialize();

        /// <summary>
        /// Queries the dal for a photographer by its given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Photographer model.</returns>
        public Photographer GetPhotographer(int id);

        /// <summary>
        /// Queries the dal for a picture by its given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Picture model.</returns>
        public Picture GetPicture(int id);

        /// <summary>
        ///  Queries the dal for the first picture that matches the directory and filename.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="filename"></param>
        /// <returns>Picture model.</returns>
        public Picture GetPicture(string directory, string filename);

        /// <summary>
        /// Queries the dal for all photographers.
        /// </summary>
        /// <returns>IEnumerable of photographers.</returns>
        public IEnumerable<Photographer> GetPhotographers();

        /// <summary>
        /// Queries the dal for photographers who match the search string.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>IEnumerable of photographers.</returns>
        public IEnumerable<Photographer> GetPhotographers(string searchString);

        /// <summary>
        /// Queries the dal for all pictures.
        /// </summary>
        /// <returns>IEnumerable of pictures.</returns>
        public IEnumerable<Picture> GetPictures();

        /// <summary>
        /// Queries the dal for pictures which match the search string.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>IEnumerable of pictures.</returns>
        public IEnumerable<Picture> GetPictures(string searchString);

        /// <summary>
        /// Save a given photographer in the dal.
        /// </summary>
        /// <param name="photographer"></param>
        public void Save(Photographer photographer);

        /// <summary>
        /// Save a given picture in the dal.
        /// </summary>
        /// <param name="picture"></param>
        public void Save(Picture picture);

        /// <summary>
        /// Updates a given photographer in the dal.
        /// </summary>
        /// <param name="photographer"></param>
        public void Update(Photographer photographer);

        /// <summary>
        /// Updates a given picture in the dal
        /// </summary>
        /// <param name="picture"></param>
        public void Update(Picture picture);

        /// <summary>
        /// Deletes a given photographer in the dal.
        /// </summary>
        /// <param name="photographer"></param>
        public void Delete(Photographer photographer);

        /// <summary>
        /// Deletes a given picture in the dal.
        /// </summary>
        /// <param name="picture"></param>
        public void Delete(Picture picture);

        /// <summary>
        /// Check if a picture already exists in the dal.
        /// </summary>
        /// <param name="picture"></param>
        /// <returns>Bool if pictures exists or not.</returns>
        public bool PictureExists(Picture picture);
    }
}
