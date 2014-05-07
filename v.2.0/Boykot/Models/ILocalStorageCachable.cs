using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using RestSharp;

namespace Boykot.Models
{
    public interface ILocalStorageCachable<T>
    {
        void SaveFile(string text);
        void SaveFileFromUri();
        string ReadFile();
    }

    public class LocalStorageCachable<T> : ILocalStorageCachable<T>, INotifyPropertyChanged
    {
        public bool IsConnectionExists
        {
            get
            {
                return (Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType 
                    != Microsoft.Phone.Net.NetworkInformation.NetworkInterfaceType.None);
            }
        }

        private Uri uri;
        private string fileName = "file.txt";
        private IEnumerable<T> items;

        public bool Loading { get; set; }

        public IEnumerable<T> Items
        {
            get { return items; }
        }

        private static Brand ParseBrand(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(Brand));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (Brand)serializer.ReadObject(ms);
            }
        }

        private static List<Category> ParseBrandsByCategory(string json)
        {
            var brands = (from i in ParseBrands(json)
                group i by i.Category
                into g
                select new Category
                {
                    Title = g.Key,
                    Brands = g.ToList(),
                }).ToList();

            return brands;
        }

        private static List<Brand> ParseBrands(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Brand>));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (List<Brand>)serializer.ReadObject(ms);
            }
        }

        public List<Brand> Brands { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public LocalStorageCachable(Uri uri)
        {
            this.uri = uri;

            var text = ReadFile();

            if (!string.IsNullOrEmpty(text))
            {
                this.Brands = ParseBrands(text);
                NotifyPropertyChanged("Brands");
            }

            if (!string.IsNullOrEmpty(text))
            {
                this.Categories = ParseBrandsByCategory(text);
                NotifyPropertyChanged("Categories");    
            }

            if (this.IsConnectionExists && this.uri != null)
            {
                this.SaveFileFromUri();
            }
            else
            {
                MessageBox.Show("Відсутній інтернет з'єднання. Можливо у вас застаріла база даних.", "Інформація", MessageBoxButton.OK);
            }
        }

        public void SaveFile(string text)
        {
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            using (var writeFile = new StreamWriter(new IsolatedStorageFileStream(this.fileName, FileMode.Create, FileAccess.Write, myIsolatedStorage)))
            {
                writeFile.WriteLine(text);
                writeFile.Close();
            }
        }

        public void SaveFileFromUri()
        {
            this.Loading = true;
            NotifyPropertyChanged("Loading");

            var client = new RestClient(this.uri.ToString());
            var request = new RestRequest("", Method.GET);

            client.ExecuteAsync<List<T>>(request, response =>
            {
                this.items = response.Data;
                SaveFile(response.Content);

                var brands = new List<Brand>();

                if (this.Items != null && this.Items.Any())
                {
                    brands.AddRange(this.Items.Select(i => ParseBrand(i.ToString())));

                    this.Brands = brands;
                    this.Loading = false;

                    NotifyPropertyChanged("Items");
                    NotifyPropertyChanged("Brands");
                    NotifyPropertyChanged("Categories");
                    NotifyPropertyChanged("Loading");
                }
            });
        }

        public string ReadFile()
        {
            string text = string.Empty;

            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isolatedStorage.FileExists(this.fileName))
                {
                    var reader = new StreamReader(new IsolatedStorageFileStream(fileName, FileMode.Open, isolatedStorage));
                    text = reader.ReadToEnd();
                    reader.Close();
                }
            }

            return text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
