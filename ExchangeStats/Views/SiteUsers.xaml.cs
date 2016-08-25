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

using ExchangeStats.Contracts;

namespace ExchangeStats.Views
{
    /// <summary>
    /// Interaction logic for SiteUsers.xaml
    /// </summary>
    public partial class SiteUsers : UserControl
    {
        public Site DisplaySite { get; set; }

        public SiteUsers(Site displaySite)
        {
            InitializeComponent();
            this.DisplaySite = displaySite;
        }
    }
}
