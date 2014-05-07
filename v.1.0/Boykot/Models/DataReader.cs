using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;

namespace Boykot.Models
{
    public class DataReader
    {
        private Category currentCategory;

        public List<Brand> GetBrands()
        {
            return this.GetByCategories().SelectMany(i => i.Brands).ToList();
        }

        public List<Category> GetByCategories()
        {
            string text = string.Empty;
            var categories = new List<Category>();

            StreamResourceInfo strm = Application.GetResourceStream(new Uri("/Boykot;component/data.txt", UriKind.Relative));
            var reader = new StreamReader(strm.Stream);
            string data = reader.ReadToEnd();

            var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var i in lines)
            {
                var values = i.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                if (values.Count() == 1)
                {
                    if (currentCategory != null)
                    {
                        categories.Add(currentCategory);
                    }
                    currentCategory = new Category { Title = values[0], Brands = new List<Brand>() };
                }

                if (values.Count() == 3)
                {
                    currentCategory.Brands.Add(new Brand
                    {
                        Title = values[0],
                        Type = values[1],
                        Owner = values[2],
                        Logo = ImagesSelector.GetImage(currentCategory.Title),
                        Category = currentCategory.Title
                    });
                }
            }

            return categories;
        }
    }
}
