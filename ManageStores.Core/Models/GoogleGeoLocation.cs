using ManageStores.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace ManageStores.Core.Models
{
    [Serializable()]
    public class GoogleGeoLocation
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        private const string key = "AIzaSyDckHHiC7vxIIBg3NYH1pr9R2ATloLzV-w";
        public GoogleGeoLocation()
        {
            Latitude = "";
            Longitude = "";
        }

        public GoogleGeoLocation(string latitude, string longitude)
        {
            Latitude = StringHelper.ShortenText(latitude, 15);
            Longitude = StringHelper.ShortenText(longitude, 15);
        }

        public bool HasCoordinates
        {
            get
            {
                if (Latitude.Length > 0 && Longitude.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public GoogleGeoLocation GetCoordinates(Address address)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" 
                + address.ZipCode + address.StreetAddress + address.City + address.Country + "&sensor=false&key=" + key;

            WebClient client = new WebClient();
            JObject result = JObject.Parse(client.DownloadString(url));
            JToken status;
            result.TryGetValue("status", out status);
            client.Dispose();

            if (status.ToString() == "Result_null")
            {
                return new GoogleGeoLocation();
            }


            var geoCood = result.SelectToken("results[0].geometry.location");

            string lat = geoCood.SelectToken("lat").ToString();
            string lng = geoCood.SelectToken("lng").ToString();
            Latitude = lat;
            Longitude = lng;
            return new GoogleGeoLocation(lat.Replace(',', '.'), lng.Replace(',', '.'));
        }
    }
}

