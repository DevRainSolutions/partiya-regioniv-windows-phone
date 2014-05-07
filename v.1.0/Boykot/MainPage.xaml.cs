using Boykot.Models;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Telerik.Windows.Data;

namespace Boykot
{
    public partial class MainPage 
    {
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var reader = new DataReader();
            Update();
            this.Brands.ItemsSource = reader.GetBrands();

            GroupListPicker.ItemsSource = new List<string>
            {
                "За алфавітом",
                "По категоріях"
            };
        }

        // search
        private void ApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Search.xaml", UriKind.RelativeOrAbsolute));
        }

        // filter
        private void ApplicationBarIconButton2_OnClick(object sender, EventArgs e)
        {
            GroupListPicker.IsExpanded = true;
        }

        // facebook
        private void ApplicationBarIconButton4_OnClick(object sender, EventArgs e)
        {
            var task = new WebBrowserTask {Uri = new Uri("https://www.facebook.com/BoycotteInUkraine")};
            task.Show();
        }

        // email
        private void ApplicationBarIconButton3_OnClick(object sender, EventArgs e)
        {
            var reader = new DataReader();
            var brands = reader.GetBrands();
            var sb = new StringBuilder("Список брендів:");

            sb.AppendLine();
            sb.AppendLine();

            foreach (var i in brands)
            {
                sb.AppendLine(i.Title);
                sb.AppendLine(i.Description);
                sb.AppendLine();
            }

            var task = new EmailComposeTask { Subject = "Список брендів Партії Регіонів", Body = sb.ToString() };
            task.Show();
        }

        private void GroupPicker_SelectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GroupListPicker.SelectedIndex == -1)
            {
                return;
            }

            Update();
        }

        private void Update()
        {
            Brands.GroupDescriptors.Clear();
            Brands.SortDescriptors.Clear();

            var group = new GenericGroupDescriptor<Brand, string>();
            group.SortMode = ListSortMode.Ascending;
            group.KeySelector = (i) => GroupListPicker.SelectedIndex <= 0 ? GetGroup(i.Title) : GetCategory(i.Category);

            Brands.GroupHeaderTemplate = GroupListPicker.SelectedIndex <= 0 ?
                Application.Current.Resources["GroupHeaderTemplate"] as DataTemplate :
                Application.Current.Resources["GroupHeaderWideTemplate"] as DataTemplate;

            Brands.GroupPickerItemTemplate = GroupListPicker.SelectedIndex <= 0 ?
                Application.Current.Resources["GroupItemTemplate"] as DataTemplate :
                Application.Current.Resources["GroupItemWideTemplate"] as DataTemplate;

            Brands.GroupDescriptors.Add(group);

            var sort = new GenericSortDescriptor<Brand, string>
            {
                SortMode = ListSortMode.Ascending,
                KeySelector = i => i.Title
            };

            Brands.SortDescriptors.Add(sort);
        }


        private string GetCategory(string category)
        {
            return ImagesSelector.GetImage(category);
        }

        private string GetGroup(string title)
        {
            var group = title.Substring(0, 1).ToLowerInvariant();
            const string chars = "0123456789~!@#$%^&*()_+«";
            return chars.Contains(@group) ? "#" : @group;
        }

        private void ApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {
            var task = new MarketplaceReviewTask();
            task.Show();
        }

        private void ApplicationBarMenuItem2_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ApplicationBarMenuItem3_OnClick(object sender, EventArgs e)
        {
            var task = new EmailComposeTask {To = "just.ukrainian@gmail.com", Subject = "Відгук на програму"};
            task.Show();
        }
    }
}