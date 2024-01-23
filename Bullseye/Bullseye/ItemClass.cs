using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    public class ItemClass
    {
       

        public int ItemID { get; set; }
        public string Name { get; set; }
        public int Sku { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Weight { get; set; }
        public int CaseSize { get; set; }
        public double CostPrice { get; set; }
        public double RetailPrice { get; set; }
        public int SupplierID { get; set; }
        public int Active { get; set; }
        public byte[] Image { get; set; }
        public string Notes { get; set; }

        public ItemClass(string name, int sku, string description, string category, 
            double weight, int caseSize, double costPrice, double retailPrice, int supplierID, 
            int active, byte[] image, string notes) 
        {
            //ItemID = itemID;
            Name = name;
            Sku = sku;
            Description = description;
            Category = category;
            Weight = weight;
            CaseSize = caseSize;
            CostPrice = costPrice;
            RetailPrice = retailPrice;
            SupplierID = supplierID;
            Active = active;
            Image = image;
            Notes = notes;
        }

        public ItemClass(int itemID,string name, int sku, string description, string category,
          double weight, int caseSize, double costPrice, double retailPrice, int supplierID,
          int active, byte[] image,string notes)
        {
            ItemID = itemID;
            Name = name;
            Sku = sku;
            Description = description;
            Category = category;
            Weight = weight;
            CaseSize = caseSize;
            CostPrice = costPrice;
            RetailPrice = retailPrice;
            SupplierID = supplierID;
            Active = active;
            Image = image;
            Notes = notes;
        }

    }
}
