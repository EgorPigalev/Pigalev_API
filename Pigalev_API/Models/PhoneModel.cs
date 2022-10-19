using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pigalev_API.Models
{
    public class PhoneModel
    {
        public int id_phone { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string colour { get; set; }
        public double price { get; set; }
        public string image { get; set; }

        public PhoneModel(Phones phones)
        {
            id_phone = phones.id_phone;
            manufacturer = phones.manufacturer;
            model = phones.model;
            colour = phones.colour;
            price = phones.price;
            image = phones.image;
        }
    }
}