using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExchangeStats.Contracts;

namespace ExchangeStats.Views.Authentication
{
    /// <summary>
    /// Interaction logic for AwaitingCompletion.xaml
    /// </summary>
    public partial class AwaitingCompletion : UserControl
    {
        public AwaitingCompletion()
        {
            InitializeComponent();

            this.FinishedAuth.MouseLeftButtonDown += FinishedAuth_MouseLeftButtonDown;
        }

        private void FinishedAuth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.FinishedAuth.Text = "Working...";

            HttpWebRequest request = WebRequest.Create($"https://auth.artofcode.co.uk/auth/token?key={App.AuthenticatingGuid}") as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ((MainWindow)Application.Current.MainWindow).ChangeContent(new FinishedAuth(false, "", (int)response.StatusCode));
                    return;
                }

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DSSToken));
                DSSToken tokenResponse = serializer.ReadObject(response.GetResponseStream()) as DSSToken;

                if (tokenResponse.AccessToken != null)
                {
                    string networkUserUri = StackApi.CreateRequestUri("me/associated", new Dictionary<string, string>
                    {
                        {"access_token", tokenResponse.AccessToken}
                    });
                    NetworkUserResponse accountUsers = StackApi.FireRequest<NetworkUserResponse>(networkUserUri);
                    NetworkUser displayUser = accountUsers.NetworkUsers.First();

                    List<Site> sites = ((MainWindow) Application.Current.MainWindow).GetSitesList();
                    string apiSiteParam = sites.First(x => x.Name == displayUser.SiteName).ApiSiteParameter;
                    string userUri = StackApi.CreateRequestUri("me", new Dictionary<string, string>
                    {
                        {"site", apiSiteParam},
                        {"access_token", tokenResponse.AccessToken}
                    });
                    UserResponse users = StackApi.FireRequest<UserResponse>(userUri);
                    User signInUser = users.Users.First();

                    App.SetSignedInUser(signInUser);
                    App.AuthenticatingGuid = Guid.Empty;
                    ((MainWindow)Application.Current.MainWindow).ChangeContent(new FinishedAuth(true, $"{signInUser.DisplayName} ({displayUser.AccountId})", 200));
                    Console.WriteLine(signInUser.DisplayName, displayUser.AccountId);
                }
                else
                {
                    ((MainWindow)Application.Current.MainWindow).ChangeContent(new FinishedAuth(false, "", -1));
                }
            }
        }
    }
}
