using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using ExchangeStats.Views;

namespace ExchangeStats
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserControl StartupView { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StackApi.RequestKey = "WmNN*LGf5mFL9JYq9Q3hnQ((";
            ApiCache.BaseCachePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ExchangeStats", "Cache"
            );
            StartupView = new WelcomeScreen();

            if (!Directory.Exists(ApiCache.BaseCachePath))
            {
                Directory.CreateDirectory(ApiCache.BaseCachePath);
                using (FileStream stream = File.Create(Path.Combine(ApiCache.BaseCachePath, "CacheInfo.dat")))
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
    }
}
