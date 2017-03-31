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
    /// Interaction logic for FinishedAuth.xaml
    /// </summary>
    public partial class FinishedAuth : UserControl
    {
        private readonly bool _success;
        private readonly string _username;
        private readonly int _status;

        public FinishedAuth(bool success, string username, int statusCode)
        {
            InitializeComponent();

            _username = username;
            _success = success;
            _status = statusCode;
            this.Loaded += FinishedAuth_Loaded;
        }

        private void FinishedAuth_Loaded(object sender, RoutedEventArgs e)
        {
            if (_success)
            {
                this.Username.Text = _username;
            }
            else
            {
                this.Success.Visibility = Visibility.Hidden;
                this.Failed.Visibility = Visibility.Visible;
                this.StatusCode.Content = _status;
            }
        }
    }
}
