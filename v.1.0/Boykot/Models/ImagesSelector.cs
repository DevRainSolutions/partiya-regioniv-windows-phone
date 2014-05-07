using System.Collections.Generic;
using System.Linq;

namespace Boykot.Models
{
    public static class ImagesSelector
    {
        static List<ImageItem> list = new List<ImageItem>
            {
                new ImageItem
                {
                    Category = "Фінансові послуги", Image="appbar.currency.dollar.png"
                },
                new ImageItem
                {
                    Category = "Медицина", Image="appbar.medical.pulse.png"
                },
                new ImageItem
                {
                    Category = "Продукти харчування", Image="appbar.food.png"
                },
                new ImageItem
                {
                    Category = "Телекомунікаційні послуги", Image = "appbar.radar.png"
                },
                new ImageItem
                {
                    Category = "Мережі заправних станцій", Image="appbar.gas.png"
                },
                new ImageItem
                {
                    Category = "Телебачення", Image = "appbar.tv.png"
                },
                new ImageItem
                {
                    Category = "Транспорт", Image = "appbar.bus.png"
                },
                new ImageItem
                {
                    Category = "Товари для дому", Image = "appbar.barcode.png"
                },
                new ImageItem
                {
                    Category = "Торгівля", Image = "appbar.suitcase.png"
                },
                new ImageItem
                {
                    Category = "Інтернет", Image = "appbar.connection.wifi.png"
                },
                new ImageItem
                {
                    Category = "Преса", Image = "appbar.camera.png"
                },
                new ImageItem
                {
                    Category = "Радіо", Image = "appbar.microphone.png"
                },
                new ImageItem
                {
                    Category = "Інформагентсва", Image = "appbar.information.circle.png"
                },
                new ImageItem
                {
                    Category = "Спорт та розваги", Image = "sport.png"
                },

            };

        public static string GetImage(string category)
        {
            const string path = "dark";
            var item = list.FirstOrDefault(i => i.Category.ToLowerInvariant() == category.ToLowerInvariant());
            return "/Images/" + path + "/" + (item != null ? item.Image : "appbar.map.png");
        }
    }


    public class ImageItem
    {
        public string Category { get; set; }
        public string Image { get; set; }
    }
}