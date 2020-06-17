using System.Collections.Generic;
using PicDb.Models;

namespace PicDb.Data
{
    public interface IDAL
    {
        /// <summary>
        /// Initializes the database connection and creates the tables.
        /// </summary>
        public void Initialize();

        /// <summary>
        /// Queries the database for a Photographer by its given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Photographer model</returns>
        public Photographer GetPhotographer(int id);

        /// <summary>
        /// Queries the database for a Picture by its given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Picture model</returns>
        public Picture GetPicture(int id);

        /// <summary>
        ///  Queries the database for the first picture that matches the directory and filename.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Picture GetPicture(string directory, string filename);

        /// <summary>
        /// Queries the database for all Photographers.
        /// </summary>
        /// <returns>List of Photographer models</returns>
        public IEnumerable<Photographer> GetPhotographers();

        /// <summary>
        /// Queries the database for Photographers who match the search string.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>List of Photographer models</returns>
        public IEnumerable<Photographer> GetPhotographers(string searchString);

        /// <summary>
        /// Queries the database for all Pictures.
        /// </summary>
        /// <returns>List of Picture models</returns>
        public IEnumerable<Picture> GetPictures();

        /// <summary>
        /// Queries the database for Pictures which match the search string.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>List of Picture models</returns>
        public IEnumerable<Picture> GetPictures(string searchString);

        /// <summary>
        /// Save a given Photographer in the database.
        /// </summary>
        /// <param name="photographer"></param>
        public void Save(Photographer photographer);

        /// <summary>
        /// Save a given Picture in the database.
        /// </summary>
        /// <param name="picture"></param>
        public void Save(Picture picture);

        /// <summary>
        /// Updates a given Photographer in the database.
        /// </summary>
        /// <param name="photographer"></param>
        public void Update(Photographer photographer);

        /// <summary>
        /// Updates a given Picture in the database
        /// </summary>
        /// <param name="picture"></param>
        public void Update(Picture picture);

        /// <summary>
        /// Deletes a given Photographer in the database.
        /// </summary>
        /// <param name="photographer"></param>
        public void Delete(Photographer photographer);

        /// <summary>
        /// Deletes a given Picture in the database.
        /// </summary>
        /// <param name="picture"></param>
        public void Delete(Picture picture);

        /// <summary>
        /// Check if a Picture already exists in the database.
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public bool PictureExists(Picture picture);
    }
}
