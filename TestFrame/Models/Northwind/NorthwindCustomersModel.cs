using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFrame.Models.Northwind
{
    public class NorthwindCustomersModel
    {
        public string CustomerID { get; set; } // Max 5 characters
        public string CompanyName { get; set; } // Max 40 characters
        public string ContactName { get; set; } // Max 30 characters
        public string ContactTitle { get; set; } // Max 30 characters
        public string Address { get; set; } // Max 60 characters
        public string City { get; set; } // Max 15 characters
        public string Region { get; set; } // Max 15 characters
        public string PostalCode { get; set; } // Max 10 characters
        public string Country { get; set; } // Max 15 characters
        public string Phone { get; set; } // Max 24 characters
        public string Fax { get; set; } // Max 24 characters
    }
}
