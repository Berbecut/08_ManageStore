using System;

namespace ManageStores.Core.Models
{
    public class Address
    {
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public GoogleGeoLocation Position { get; set; }

        public Address()
        {
            Position = new GoogleGeoLocation();
        }
    }
}
