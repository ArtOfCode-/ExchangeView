using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ExchangeStats.Views;

namespace ExchangeStats
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserControl StartupView { get; set; }

        public static string BaseResourcePath { get; private set; }

        public static Dictionary<string, JObject> AppResources = new Dictionary<string, JObject>();

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

            BaseResourcePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ExchangeStats", "Resources"
            );

            if (!Directory.Exists(BaseResourcePath))
            {
                Directory.CreateDirectory(BaseResourcePath);
            }

            AutoloadResources();
        }

        private static void AutoloadResources()
        {
            string[] files = Directory.GetFiles(BaseResourcePath, "*.auto.json");
            foreach (string file in files)
            {
                LoadResource(Path.GetFileNameWithoutExtension(file));
            }
        }

        public static void LoadResource(string resourceFileName)
        {
            string fileName = resourceFileName + ".json";
            using (TextReader stream = File.OpenText(Path.Combine(BaseResourcePath, fileName)))
            {
                AppResources.Add(resourceFileName, (JObject)JToken.ReadFrom(new JsonTextReader(stream)));
            }
        }

        public static string GetExceptionMessage(string exceptionName)
        {
            JObject resource = App.AppResources["ErrorMessages.auto"];
            return (string)resource[exceptionName]["UserMessage"];
        }

        public static string GetExceptionTechDetails(string exceptionName)
        {
            JObject resource = App.AppResources["ErrorMessages.auto"];
            return (string)resource[exceptionName]["TechnicalDetails"];
        }

        public static object GetResourceObject(string resourceName)
        {
            JObject resource = App.AppResources["Generic.auto"];
            return (object)resource[resourceName];
        }
    }
}
