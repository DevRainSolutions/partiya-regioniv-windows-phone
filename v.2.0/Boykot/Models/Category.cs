using System.Collections.Generic;

namespace Boykot.Models
{
    public class Category
    {
        public string Title { get; set; }

        public List<Brand> Brands { get; set; }
    }
}
