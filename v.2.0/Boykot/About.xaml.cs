namespace Boykot
{
    using System;
    using System.Windows;
    using Microsoft.Phone.Tasks;

    public partial class About
    {
        public About()
        {
            InitializeComponent();
        }

       
        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            var task = new WebBrowserTask { Uri = new Uri("http://bit.ly/BoikotForm") };
            task.Show();
        }
    }
}