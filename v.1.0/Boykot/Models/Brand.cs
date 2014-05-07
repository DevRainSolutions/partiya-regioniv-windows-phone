namespace Boykot.Models
{
    public class Brand
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }

        public string Logo { get; set; }

        public string Description
        {
            get { return Owner + ", " + Type.ToLowerInvariant(); }
        }

        public string Category { get; set; }
    }
}
