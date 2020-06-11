using log4net;
using PicDb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace PicDb.Data
{
    class DALSqlite : IDAL
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DALSqlite));
        private readonly string _dbFileName;
        private readonly string _connectionString;

        public DALSqlite()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Sqlite"]?.ConnectionString;
            _dbFileName = connectionString?.Split("=", StringSplitOptions.RemoveEmptyEntries)[1];
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            if (!File.Exists(_dbFileName))
            {
                if (Path.GetDirectoryName(_dbFileName) != string.Empty)
                    Directory.CreateDirectory(Path.GetDirectoryName(_dbFileName));
                var file = File.Create(_dbFileName);
                file.Close();
                Log.Info($"Created Sqlite database {_dbFileName}");
            }

            CreateTable("photographer", @"
                CREATE TABLE photographer(
                    id INTEGER PRIMARY KEY,
                    first_name TEXT,
                    last_name TEXT,
                    birthdate DATE,
                    notes TEXT
                )"
            );

            CreateTable("exif", @"
                CREATE TABLE exif(
                    id INTEGER PRIMARY KEY,
                    model TEXT,
                    lens TEXT,
                    focal_length INTEGER,
                    datetime_original DATETIME
                )"
            );

            CreateTable("iptc", @"
                CREATE TABLE iptc(
                    id INTEGER PRIMARY KEY,
                    caption TEXT,
                    keywords TEXT,
                    credit TEXT,
                    copyright TEXT
                )"
            );

            CreateTable("picture", @"
                CREATE TABLE picture(
                    id INTEGER PRIMARY KEY,
                    directory TEXT,
                    filename TEXT,
                    iptc_id INTEGER,
                    exif_id INTEGER,
                    photographer_id INTEGER,
                    FOREIGN KEY (iptc_id) REFERENCES iptc(id),
                    FOREIGN KEY (exif_id) REFERENCES exif(id),
                    FOREIGN KEY (photographer_id) REFERENCES photographer(id)
                )"
            );

            Log.Info("DALSqlite initialized");
        }

        public Picture GetPicture(int id)
        {
            throw new NotImplementedException();
        }

        public Photographer GetPhotographer(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT id, first_name, last_name, birthdate, notes FROM photographer WHERE id=@id";
            command.Parameters.AddWithValue("id", id);

            Photographer photographer = null;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    photographer = new Photographer
                    {
                        Id = reader.GetInt32(0),
                        Firstname = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Lastname = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Notes = reader.IsDBNull(4) ? null : reader.GetString(4)
                    };
                    if (!reader.IsDBNull(3)) photographer.Birthdate = reader.GetDateTime(3);
                }
            }

            Log.Info("Get Photographer " + photographer?.Lastname);
            return photographer;
        }

        public void Delete(Photographer photographer)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM photographer WHERE id=@id";
            command.Parameters.AddWithValue("id", photographer.Id);

            command.ExecuteNonQuery();
            Log.Info("Deleted Photographer " + photographer.Lastname);
        }

        public void Delete(Picture picture)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photographer> GetPhotographers()
        {
            var photographers = new List<Photographer>();
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT id, first_name, last_name, birthdate, notes FROM photographer";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var photographer = new Photographer
                    {
                        Id = reader.GetInt32(0),
                        Firstname = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Lastname = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Notes = reader.IsDBNull(4) ? null : reader.GetString(4)
                    };
                    if (!reader.IsDBNull(3)) photographer.Birthdate = reader.GetDateTime(3);

                    photographers.Add(photographer);
                }
            }

            Log.Info("Get " + photographers.Count + " Photographers");
            return photographers;
        }

        public IEnumerable<Photographer> GetPhotographers(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Picture> GetPictures()
        {
            var pictures = new List<Picture>();
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT id, directory, filename, iptc_id, exif_id, photographer_id FROM picture";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var newPic = new Picture
                    {
                        Id = reader.GetInt32(0),
                        Directory = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Filename = reader.IsDBNull(2) ? null : reader.GetString(2)
                    };

                    if (!reader.IsDBNull(3))
                    {
                        newPic.IptcId = reader.GetInt32(3);
                        newPic.Iptc = GetIptc(reader.GetInt32(3));
                    }

                    if (!reader.IsDBNull(4))
                    {
                        newPic.ExifId = reader.GetInt32(4);
                        newPic.Exif = GetExif(reader.GetInt32(4));
                    }

                    if (!reader.IsDBNull(5))
                    {
                        newPic.PhotographerId = reader.GetInt32(5);
                        newPic.Photographer = GetPhotographer(reader.GetInt32(5));
                    }

                    pictures.Add(newPic);
                }
            }

            Log.Info("Get " + pictures.Count + " Pictures ");
            return pictures;
        }

        public IEnumerable<Picture> GetPictures(string searchString)
        {
            throw new NotImplementedException();
        }

        public void Save(Photographer photographer)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO photographer (first_name, last_name, birthdate, notes) VALUES (@firstname, @lastname, @birthdate, @notes)";
            command.Parameters.AddWithValue("firstname", photographer.Firstname);
            command.Parameters.AddWithValue("lastname", photographer.Lastname);
            command.Parameters.AddWithValue("birthdate", photographer.Birthdate);
            command.Parameters.AddWithValue("notes", photographer.Notes);

            command.ExecuteNonQuery();
            Log.Info("Inserted new Photographer " + photographer.Lastname);
        }

        public void Save(Picture picture)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            int? iptcId = null;
            int? exifId = null;

            if (picture.Iptc != null)
            {
                command.CommandText = "INSERT INTO iptc (caption, keywords, credit, copyright) VALUES (@caption, @keywords, @credit, @copyright)";
                command.Parameters.AddWithValue("caption", picture.Iptc.Caption);
                command.Parameters.AddWithValue("keywords", picture.Iptc.Keywords);
                command.Parameters.AddWithValue("credit", picture.Iptc.Credit);
                command.Parameters.AddWithValue("copyright", picture.Iptc.Copyright);
                command.ExecuteNonQuery();
                Log.Info("Inserted new Iptc");

                command.CommandText = "SELECT last_insert_rowid() FROM iptc";
                iptcId = Convert.ToInt32(command.ExecuteScalar());
            }

            if (picture.Exif != null)
            {
                command.CommandText = "INSERT INTO exif (model, lens, focal_length, datetime_original) VALUES (@model, @lens, @focal_length, @datetime_original)";
                command.Parameters.AddWithValue("model", picture.Exif.Model);
                command.Parameters.AddWithValue("lens", picture.Exif.Lens);
                command.Parameters.AddWithValue("focal_length", picture.Exif.FocalLength);
                command.Parameters.AddWithValue("datetime_original", picture.Exif.DateTimeOriginal);
                command.ExecuteNonQuery();
                Log.Info("Inserted new Exif");

                command.CommandText = "SELECT last_insert_rowid() FROM exif";
                exifId = Convert.ToInt32(command.ExecuteScalar());
            }

            command.CommandText = "INSERT INTO picture (directory ,filename, iptc_id, exif_id, photographer_id) VALUES (@directory, @filename, @iptc_id, @exif_id, @photographer_id)";
            command.Parameters.AddWithValue("directory", picture.Directory);
            command.Parameters.AddWithValue("filename", picture.Filename);
            command.Parameters.AddWithValue("iptc_id", iptcId);
            command.Parameters.AddWithValue("exif_id", exifId);
            command.Parameters.AddWithValue("photographer_id", picture.PhotographerId);

            command.ExecuteNonQuery();
            Log.Info("Inserted new Picture " + picture.Filename);
        }

        public void Update(Photographer photographer)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "UPDATE photographer SET first_name=@firstname, last_name=@lastname, birthdate=@birthdate, notes=@notes WHERE id=@id";
            command.Parameters.AddWithValue("firstname", photographer.Firstname);
            command.Parameters.AddWithValue("lastname", photographer.Lastname);
            command.Parameters.AddWithValue("birthdate", photographer.Birthdate);
            command.Parameters.AddWithValue("notes", photographer.Notes);
            command.Parameters.AddWithValue("id", photographer.Id);

            command.ExecuteNonQuery();
            Log.Info("Updated Photographer " + photographer.Lastname);
        }

        public void Update(Picture picture)
        {
            throw new NotImplementedException();
        }

        private void CreateTable(string table, string createTableCommand)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT count(name) FROM sqlite_master WHERE type='table' AND name=@table";
            command.Parameters.AddWithValue("table", table);

            var exists = false;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    exists = reader.GetBoolean(0);
                }
            }

            if (exists) return;

            command.CommandText = createTableCommand;
            command.ExecuteNonQuery();
            Log.Info("Created Table " + table);
        }

        private Iptc GetIptc(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT id, caption, keywords, credit, copyright FROM iptc WHERE id=@id";
            command.Parameters.AddWithValue("id", id);

            Iptc iptc = null;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    iptc = new Iptc
                    {
                        Id = reader.GetInt32(0),
                        Caption = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Keywords = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Credit = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Copyright = reader.IsDBNull(4) ? null : reader.GetString(4)
                    };
                }
            }

            return iptc;
        }

        private Exif GetExif(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT id, model, lens, focal_length, datetime_original FROM exif WHERE id=@id";
            command.Parameters.AddWithValue("id", id);

            Exif exif = null;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    exif = new Exif
                    {
                        Id = reader.GetInt32(0),
                        Model = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Lens = reader.IsDBNull(2) ? null : reader.GetString(2),
                    };
                    if (!reader.IsDBNull(3)) exif.FocalLength = reader.GetInt32(3);
                    if (!reader.IsDBNull(4)) exif.DateTimeOriginal = reader.GetDateTime(4);
                }
            }

            return exif;
        }
    }
}