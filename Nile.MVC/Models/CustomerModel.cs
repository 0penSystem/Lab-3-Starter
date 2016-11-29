using Nile.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nile.MVC.Models
{
    public class CustomerModel
    {

        public CustomerModel()
        {

        }

        public CustomerModel(Customer customer)
        {
            ID = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }

        public Customer toCustomer()
        {
            return new Customer(ID) { FirstName = FirstName, LastName = LastName };
        }


        public int ID { get; set; }

        [Required(AllowEmptyStrings = false), DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false), DisplayName("Last Name")]

        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


    }
}