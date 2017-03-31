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

namespace ExchangeStats.Controls
{
    public class PlaceholderTextBox : TextBox
    {
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PlaceholderTextBox));

        public string PlaceholderText
        {
            get
            {
                return (string)base.GetValue(PlaceholderTextProperty);
            }
            set
            {
                base.SetValue(PlaceholderTextProperty, (object)value);
            }
        }

        public PlaceholderTextBox()
        {
            GotFocus += TextBox_GotFocus;
            LostFocus += TextBox_LostFocus;
            Loaded += TextBox_Loaded;
        }

        public void TextBox_LostFocus(object sender, EventArgs e)
        {
            SetPlaceholder();
        }

        public void TextBox_Loaded(object sender, EventArgs e)
        {
            SetPlaceholder();
        }

        public void TextBox_GotFocus(object sender, EventArgs e)
        {
            if (this.Text == this.PlaceholderText)
            {
                this.Text = "";
                this.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        public void SetPlaceholder()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = this.PlaceholderText;
                this.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
