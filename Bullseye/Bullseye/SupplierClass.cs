using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    public class SupplierClass
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }
        public string Notes { get; set; }

        public SupplierClass(int supplierID, string name, string address1, string address2, string city, string country, string province, string postalcode,
            string phone, string  contact, string notes)
        {
            SupplierID = supplierID;
            Name = name;
            Address1 = address1;
            Address2 = address2;
            City = city;
            Country = country;
            Province = province;
            PostalCode = postalcode;
            Phone = phone;
            Contact = contact;
            Notes = notes;
        }


    }
}
