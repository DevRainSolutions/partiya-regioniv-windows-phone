namespace Boykot
{
    using Boykot.Models;
    using Microsoft.Phone.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using Telerik.Windows.Data;

    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            this.DataContext = new LocalStorageCachable<string>(new Uri("https://script.google.com/macros/s/AKfycbxWbLBL6_7FJkb5fPj6PdyE45EoOCwgFVaAH6H0QmMcgiP8EVo6/exec"));

            GroupListPicker.ItemsSource = new List<string>
            {
                "За алфавітом",
                "По категоріях",
                "По містам",
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
            var task = new WebBrowserTask { Uri = new Uri("https://www.facebook.com/BoycotteInUkraine") };
            task.Show();
        }

        // email
        private void ApplicationBarIconButton3_OnClick(object sender, EventArgs e)
        {
            /*
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
             */
        }

        private void GroupPicker_SelectionChange(object sender, SelectionChangedEventArgs e)
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
            group.KeySelector = (i) => Get(GroupListPicker.SelectedIndex, i);

            Brands.GroupHeaderTemplate = GetGroupHeaderTemplate(GroupListPicker.SelectedIndex);
            Brands.GroupPickerItemTemplate = GetGroupPickerItemTemplate(GroupListPicker.SelectedIndex); 

            Brands.GroupDescriptors.Add(group);

            var sort = new GenericSortDescriptor<Brand, string>
            {
                SortMode = ListSortMode.Ascending,
                KeySelector = i => i.Title
            };

            Brands.SortDescriptors.Add(sort);
        }

        private DataTemplate GetGroupHeaderTemplate(int index)
        {
            var name = string.Empty;

            switch (index)
            {
                case 1:
                    name = "GroupHeaderWideTemplate";
                    break;
                default:
                    name = "GroupHeaderTemplate";
                    break;
            }

            return Application.Current.Resources[name] as DataTemplate;
        }

        private DataTemplate GetGroupPickerItemTemplate(int index)
        {
            var name = string.Empty;

            switch (index)
            {
                case 1:
                    name = "GroupItemWideTemplate";
                    break;
                default:
                    name = "GroupItemTemplate";
                    break;
            }

            return Application.Current.Resources[name] as DataTemplate;
        }

        private string Get(int index, Brand i)
        {
            switch (index)
            {
                case -1:
                case 0:
                    return GetGroup(i.Title);
                case 1:
                    return GetCategory(i.Category);
                case 2:
                    return GetGroup2(i.GeoFormatted);
                default:
                    return GetGroup(i.Title);
            }
        }

        private string GetCategory(string category)
        {
            return ImagesSelector.GetImage(category);
        }

        private string GetGroup(string title)
        {
            var group = title.Substring(0, 1).ToLowerInvariant();
            var chars = "0123456789~!@#$%^&*()_+«";
            return chars.Contains(@group) ? "#" : @group;
        }

        private string GetGroup2(string title)
        {
            if (title == "Всеукраїнська") return "#";
            var group = title.Substring(0, 1).ToLowerInvariant();
            var chars = "0123456789~!@#$%^&*()_+«";
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
            var task = new EmailComposeTask { To = "just.ukrainian@gmail.com", Subject = "Відгук на програму" };
            task.Show();
        }
    }
}