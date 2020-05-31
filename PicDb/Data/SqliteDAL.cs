using log4net;
using PicDb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;

namespace PicDb.Data
{
    class SqliteDAL : IDAL
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(SqliteDAL));
        private readonly string _dbFileName;
        private readonly string _connectionString;

        public SqliteDAL()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Sqlite"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var exception = new NullReferenceException("Connection string to Sqlite database is null or empty.");
                _log.Error(exception);
                throw exception;
            }
            else if (connectionString.Split("=", StringSplitOptions.RemoveEmptyEntries).Length != 2)
            {
                var exception = new FormatException("Connection string to Sqlite database has invalid format. Use something like 'Data Source=picdb.sqlite'");
                _log.Error(exception);
                throw exception;
            }

            _dbFileName = connectionString.Split("=", StringSplitOptions.RemoveEmptyEntries)[1];
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            if (!File.Exists(_dbFileName))
            {
                if (Path.GetDirectoryName(_dbFileName) != string.Empty) Directory.CreateDirectory(Path.GetDirectoryName(_dbFileName));
                var file = File.Create(_dbFileName);
                file.Close();
                _log.Info($"Created Sqlite database {_dbFileName}");
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
                    datetime_original DATETIME,
                    picture_id INTEGER
                )"
            );

            CreateTable("iptc", @"
                CREATE TABLE iptc(
                    id INTEGER PRIMARY KEY,
                    caption TEXT,
                    keywords TEXT,
                    credit TEXT,
                    copyright TEXT,
                    picture_id INTEGER
                )"
            );

            CreateTable("picture", @"
                CREATE TABLE picture(
                    id INTEGER PRIMARY KEY,
                    directory TEXT,
                    file_name TEXT,
                    iptc_id INTEGER,
                    exif_id INTEGER,
                    photographer_id INTEGER,
                    FOREIGN KEY (iptc_id) REFERENCES iptc(id),
                    FOREIGN KEY (exif_id) REFERENCES exif(id),
                    FOREIGN KEY (photographer_id) REFERENCES photographer(id)
                )"
            );
           
            _log.Info("SqliteDAL initilized");
        }

        public Picture GetImage(int id)
        {
            throw new NotImplementedException();
        }

        public Photographer GetPhotographer(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Photographer photographer)
        {
            throw new NotImplementedException();
        }

        public void Delete(Picture picture)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photographer> GetPhotographers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photographer> GetPhotographers(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Picture> GetPictures()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Picture> GetPictures(string searchString)
        {
            throw new NotImplementedException();
        }

        public void Save(Photographer photographer)
        {
            throw new NotImplementedException();
        }

        public void Save(Picture pictures)
        {
            throw new NotImplementedException();
        }

        public void Update(Photographer photographer)
        {
            throw new NotImplementedException();
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
            command.CommandText = $"SELECT count(name) FROM sqlite_master WHERE type='table' AND name='{table}'";

            var exists = false;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    exists = reader.GetBoolean(0);
                }
            }

            if (exists) return;

            _log.Info("Created Table " + table);
            command.CommandText = createTableCommand;
            command.ExecuteNonQuery();
        }
    }
}
