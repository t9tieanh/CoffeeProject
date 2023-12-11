using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaffeeShop
{
    public class Product
    {
        private string id;
        private string name;
        private int price;
        private int quatity ;
        private LoaiProduct loaiProduct;
        public Product(string id, string name,  int price, LoaiProduct loaiProduct ,int quatity)
        {
            Id = id;
            Name = name;
            Price = price;
            LoaiProduct = loaiProduct;
            Quatity = quatity;
        }
        public Product() { }
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Price { get => price; set => price = value; }
        public LoaiProduct LoaiProduct { get => loaiProduct; set => loaiProduct = value; }
        public int Quatity { get => quatity; set => quatity = value; }
    }
}