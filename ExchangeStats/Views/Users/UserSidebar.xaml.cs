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

namespace ExchangeStats.Views.Users
{
    /// <summary>
    /// Interaction logic for UserSidebar.xaml
    /// </summary>
    public partial class UserSidebar : UserControl
    {
        public User DisplayUser { get; private set; }

        public UserSidebar(User user)
        {
            InitializeComponent();
            DisplayUser = user;

            Username.Text = user.DisplayName;
            GoldBadges.Content = user.BadgeCounts.Gold;
            SilverBadges.Content = user.BadgeCounts.Silver;
            BronzeBadges.Content = user.BadgeCounts.Bronze;
            Reputation.Content = user.Reputation;
        }
    }
}
