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


        public LocationClass() { }
        public LocationClass(int siteID, string name, string provinceID, string address, string address2, string city, string country, string postalCode, string phone, string dayOfWeek, int distanceFromWH, string notes)
        {
            SiteID = siteID;
            Name = name;
            Province = provinceID;
            Address = address;
            Address2 = address2;
            City = city;
            Country = country;
            PostalCode = postalCode;
            Phone = phone;
            DayOfWeek = dayOfWeek;
            DistanceFromWH = distanceFromWH;
            Notes = notes;

        }   
    }//end of class
}
