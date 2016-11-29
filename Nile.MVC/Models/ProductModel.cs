using Nile.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nile.MVC.Models
{
    public class ProductModel
    {

        public int ID { get; set; }

        [Required(AllowEmptyStrings = false), DisplayName("Name")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue), DisplayName("Price"), DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [DisplayName("Is Discontinued")]
        public bool IsDiscontinued { get; set; }


        public ProductModel()
        {

        }

        public ProductModel(Product product)
        {
            ID = product.Id;
            Name = product.Name;
            UnitPrice = product.UnitPrice;
            IsDiscontinued = product.Discontinued;
        }

        public Product toProduct()
        {
            return new Product(ID)
            {
                Name = Name,
                Discontinued = IsDiscontinued,
                UnitPrice = UnitPrice
            };
        }

    }
}