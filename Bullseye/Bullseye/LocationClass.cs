using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Bullseye
{
    public class LocationClass
    {

        public int SiteID { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string DayOfWeek { get; set; }
        public int DistanceFromWH { get; set; }
        public string Notes { get; set; }


        public LocationClass[] Locations => new LocationClass[]
 {
        new LocationClass { SiteID = 1, Name = "Truck", Province = "NB", Address = "1063 Bayside Drive", Address2 = "", City = "Saint John", Country = "Canada", PostalCode = "E2J4Y2",
                            Phone = "5066966236", DayOfWeek = "SUNDAY", DistanceFromWH = 0, Notes = "" },
        new LocationClass { SiteID = 2, Name = "Warehouse", Province = "NB", Address = "438 Grandview Avenue", Address2 = "", City = "Saint John", Country = "Canada", PostalCode = "E2J4M9",
                            Phone = "5066966228", DayOfWeek = "SATURDAY", DistanceFromWH = 0, Notes = "" },
        new LocationClass { SiteID = 3, Name = "Bullseye Corporate Headquarters", Province = "NB", Address = "950 Grandview Avenue", Address2 = "", City = "Saint John", Country = "Canada", PostalCode = "E2L3V1",
                            Phone = "5066966666", DayOfWeek = "SATURDAY", DistanceFromWH = 0, Notes = "" },
        new LocationClass { SiteID = 4, Name = "Saint John Retail", Province = "NB", Address = "519 Westmorland Road", Address2 = "", City = "Saint John", Country = "Canada", PostalCode = "E2J3W9",
                            Phone = "5066966229", DayOfWeek = "MONDAY", DistanceFromWH = 5, Notes = "" },
        new LocationClass { SiteID = 5, Name = "Sussex Retail", Province = "NB", Address = "565 Main Street", Address2 = "", City = "Sussex", Country = "Canada", PostalCode = "E4E7H4",
                            Phone = "5066966230", DayOfWeek = "FRIDAY", DistanceFromWH = 74, Notes = "" },
        new LocationClass { SiteID = 6, Name = "Moncton Retail", Province = "NB", Address = "1380 Mountain Road", Address2 = "Unit 46", City = "Moncton", Country = "Canada", PostalCode = "E1C2T8",
                            Phone = "5066966231", DayOfWeek = "TUESDAY", DistanceFromWH = 150, Notes = "" },
        new LocationClass { SiteID = 7, Name = "Dieppe Retail", Province = "NB", Address = "477 Paul Street", Address2 = "", City = "Dieppe", Country = "Canada", PostalCode = "E1A4X5",
                            Phone = "5066966232", DayOfWeek = "TUESDAY", DistanceFromWH = 157, Notes = "" },
        new LocationClass { SiteID = 8, Name = "Oromocto Retail", Province = "NB", Address = "273 Restigouche Road", Address2 = "", City = "Oromocto", Country = "Canada", PostalCode = "E2V2H1",
                            Phone = "5066966233", DayOfWeek = "WEDNESDAY", DistanceFromWH = 96, Notes = "" },
        new LocationClass { SiteID = 9, Name = "Fredericton Retail", Province = "NB", Address = "1381 Regent Street", Address2 = "Unit Y200A", City = "Fredericton", Country = "Canada", PostalCode = "E3C1A2",
                    Phone = "5066966234", DayOfWeek = "WEDNESDAY", DistanceFromWH = 116, Notes = "" },
          new LocationClass { SiteID = 10, Name = "Miramichi Retail", Province = "NB", Address = "2441 King George Highway", Address2 = "", City = "Miramichi", Country = "Canada", PostalCode = "E1V6W2",
                            Phone = "5066966235", DayOfWeek = "THURSDAY", DistanceFromWH = 270, Notes = "" },

 };


        public LocationClass() { }



    }//end of class
}
