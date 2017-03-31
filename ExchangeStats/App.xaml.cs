using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExchangeStats.Contracts;
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
        public static readonly int ClientId = 7402;

        public static UserControl StartupView { get; set; }

        public static User SignedInUser { get; set; }

        public static Guid AuthenticatingGuid { get; set; }

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

            JObject userSignedIn = GetResourceObject("User") as JObject;
            if (userSignedIn == null)
            {
                SignedInUser = null;
                return;
            }

            JToken uid = userSignedIn.GetValue("UserId");
            if (uid == null)
            {
                SignedInUser = null;
                return;
            }

            string userUri = StackApi.CreateRequestUri("users/{id}", new Dictionary<string, string>
            {
                {"id", uid.ToString()}
            });
            UserResponse resp = StackApi.FireRequest<UserResponse>(userUri);
            SignedInUser = resp.Users.First();
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
            return resource[resourceName];
        }

        public static void SetSignedInUser(User user)
        {
            SignedInUser = user;
            ((MainWindow) Current.MainWindow).SetSignedInUser(user);
        }
    }
}
