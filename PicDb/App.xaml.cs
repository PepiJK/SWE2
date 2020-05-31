using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using PicDb.Data;

namespace PicDb
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(App));
        private readonly SqliteDAL _dal = new SqliteDAL();

        protected override void OnStartup(StartupEventArgs e)
        {
            _log.Info("=============  PicDb Start  =============");
            _dal.Initialize();
            base.OnStartup(e);
        }
    }
}
