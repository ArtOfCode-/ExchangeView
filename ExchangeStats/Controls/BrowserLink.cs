using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExchangeStats.Controls
{
    class BrowserLink : Hyperlink
    {
        public static readonly DependencyProperty TargetUriProperty =
            DependencyProperty.Register("TargetUri", typeof(string), typeof(BrowserLink));

        public string TargetUri
        {
            get
            {
                return (string)base.GetValue(TargetUriProperty);
            }
            set
            {
                base.SetValue(TargetUriProperty, (object)value);
            }
        }

        public BrowserLink()
        {
            Click += BrowserLink_Click;
        }

        private void BrowserLink_Click(object sender, RoutedEventArgs e)
        {
            BrowserLink link = (BrowserLink)sender;
            if (!string.IsNullOrEmpty(link.TargetUri))
            {
                Process.Start(link.TargetUri);
            }
        }
    }
}
