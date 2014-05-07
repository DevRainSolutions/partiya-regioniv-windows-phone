using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Boykot.Models
{
    [DataContract]
    public class Brand
    {
        private string logo;

        [DataMember(Name = "Торгова марка")]
        public string Title { get; set; }

        [DataMember(Name = "Опис")]
        public string Description { get; set; }

        [DataMember(Name = "Реальний власник")]
        public string Owner { get; set; }

        [DataMember(Name = "Причетність")]
        public string Implication { get; set; }

        [DataMember(Name = "Альтернатива. Чим замінити:")]
        public string Alt { get; set; }

        [DataMember(Name = "Географія")]
        public List<string> Geo { get; set; }

        public string DescriptionFormatted
        {
            get
            {
                string text = !string.IsNullOrWhiteSpace(Owner) ? Owner + ", " + Description : Description;

                if (!string.IsNullOrWhiteSpace(Implication))
                {
                    text += ", " + Implication;
                }

                return text;
            }
        }

        public string GeoFormatted
        {
            get
            {
                if (Geo == null)
                {
                    return string.Empty;
                }

                return string.Join(", ", Geo);
            }
        }

        [DataMember(Name = "Категорія")]
        public string Category { get; set; }

        [DataMember(Name = "Сайт")]
        public string Website { get; set; }

        [DataMember(Name = "Логотип")]
        public string Logo
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(logo))
                {
                    return logo;
                }

                return "/Images/logo.png";
            }

            set { logo = value; }
        }

        [DataMember]
        public string Type { get; set; }
    }
}