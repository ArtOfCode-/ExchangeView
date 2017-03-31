using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ExchangeStats.Views.Authentication
{
    /// <summary>
    /// Interaction logic for AuthSidebar.xaml
    /// </summary>
    public partial class AuthSidebar : UserControl
    {
        private readonly string _authUri;

        public AuthSidebar(string authUri)
        {
            InitializeComponent();
            _authUri = authUri;

            this.Loaded += AuthSidebar_Loaded;
            this.AuthLink.Click += AuthLink_Click;
        }

        private void AuthLink_Click(object sender, RoutedEventArgs e)
        {
            UserControl nextContent = new AwaitingCompletion();
            ((MainWindow)Application.Current.MainWindow).ChangeContent(nextContent);
        }

        private void AuthSidebar_Loaded(object sender, RoutedEventArgs e)
        {
            this.AuthLink.TargetUri = _authUri;
        }
    }
}
