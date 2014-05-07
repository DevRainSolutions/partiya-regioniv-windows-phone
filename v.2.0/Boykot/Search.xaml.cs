using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Boykot.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Boykot
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            radTextBox.LostFocus += radTextBox_LostFocus;
            
            this.Loaded += Page1_Loaded;
        }

        
        private void CheckForEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Check();
            }
        }

        void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            radTextBox.Focus();
        }

        
        private void RadTextBox_ActionButtonTap(object sender, EventArgs e)
        {
            Check();
        }

        
        private void Check()
        {
            /*
            var search = radTextBox.Text.ToLowerInvariant().Trim();

            var reader = new DataReader();
            var item = reader.GetBrands().FirstOrDefault(i => i.Title.ToLowerInvariant() == search);

            if (string.IsNullOrWhiteSpace(search))
            {
                LayoutRoot.Background = new SolidColorBrush(Colors.Transparent);

                result.Text = string.Empty;
                result2.Text = string.Empty;
                return;
            }

            if (item != null)
            {
                LayoutRoot.Background = new SolidColorBrush(Colors.Red);

                result.Text = item.Title + " належить або контролюється регіоналами!";
                result2.Text = item.Description;

            }
            else
            {
                LayoutRoot.Background = new SolidColorBrush(Colors.Green);

                result.Text = radTextBox.Text.Trim() + " не належить і не контролюється регіоналами :-)";
                result2.Text = "Можна купувати або користуватися послугами цього бренду";
            }*/
        }

        
        void radTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Check();
        }
    }
}