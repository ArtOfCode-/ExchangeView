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

namespace ExchangeStats.Views
{
    /// <summary>
    /// Interaction logic for ErrorReport.xaml
    /// </summary>
    public partial class ErrorReport : UserControl
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public ErrorReport(string title, string message, string technicalDetails)
        {
            InitializeComponent();
            this.Title = title;
            this.Message = message;

            this.ErrorTitle.Content = this.Title;
            this.ErrorMessage.Text = this.Message;
            this.TechnicalDetails.Text = technicalDetails;
        }
    }
}
