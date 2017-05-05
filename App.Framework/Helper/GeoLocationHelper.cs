using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vitali.Framework.Json;

namespace Vitali.Framework.Helper
{
    public static class GeoLocationHelper
    {
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::                                                                         :::
        //:::  This routine calculates the distance between two points (given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles (default)                         :::
        //:::                  'K' is kilometers                                      :::
        //:::                  'N' is nautical miles                                  :::
        //:::                                                                         :::
        //:::  Worldwide cities and other features databases with latitude longitude  :::
        //:::  are available at http://www.geodatasource.com                          :::
        //:::                                                                         :::
        //:::  For enquiries, please contact sales@geodatasource.com                  :::
        //:::                                                                         :::
        //:::  Official Web site: http://www.geodatasource.com                        :::
        //:::                                                                         :::
        //:::           GeoDataSource.com (C) All Rights Reserved 2015                :::
        //:::                                                                         :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


        //189.61.220.247
        public static async Task<Address> GetAddressByIp(string ip)
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri(string.Format("http://freegeoip.net/json/{0}", ip));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                dynamic result = JsonUtils.ConvertToObject(response.Content.ReadAsStringAsync().Result);

                return new Address
                {
                    City = result.city,
                    State = result.region_code
                };
            }
        }

        //1309630 - (-22.9274119, -47.0280711)
        //Praça Capital - (-22.8466315, -47.0852875)
        //Console.WriteLine(distance(-22.9274119, -47.0280711, -22.8466315, -47.0852875, 'K'));
        private static double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;

            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));

            dist = Math.Acos(dist);

            dist = rad2deg(dist);

            dist = dist * 60 * 1.1515;

            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }

            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        /// <summary>
        /// Calcula a ditancia linear em KM entre 2 pontos
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public static double distanceInKm(double lat1, double lon1, double lat2, double lon2)
        {
            return distance(lat1, lon1, lat2, lon2, 'K');
        }

        public static long KMToMetters(int km)
        {
            return km * 1000;
        }

        public static double MettersToKM(double meters)
        {
            return meters * 0.001;
        }

        public static async Task<dynamic> GetCoordinate(string address)
        {
            var result = await GetMapsGoogleResponse(address);

            if (result.status == "OK")
            {
                return result.results[0].geometry.location;
            }

            return null;
        }

        public static string PointFromDecimal(decimal lat, decimal lon)
        {
            return string.Format("POINT({0} {1})", lat.ToString(CultureInfo.InvariantCulture), lon.ToString(CultureInfo.InvariantCulture));
        }

        public static async Task<Address> GetAddressByCep(string address)
        {
            var result = await GetMapsGoogleResponse(address);

            if (result.status == "OK")
            {
                string formatted_address = result.results[0].formatted_address;

                string[] formatted_address_splitted = formatted_address.Split(',');

                if (formatted_address_splitted.Length >= 4)
                {
                    string[] formatted_address_city_splitted = formatted_address_splitted[1].Split('-');

                    return new Address
                    {
                        Neighborhood = formatted_address_splitted[0].Trim(),
                        Zipcode = formatted_address_splitted[2].Trim(),
                        Country = formatted_address_splitted[3].Trim(),
                        City = formatted_address_city_splitted[0].Trim(),
                        State = formatted_address_city_splitted[1].Trim(),
                    };
                }
            }

            return null;
        }

        public static async Task<dynamic> GetMapsGoogleResponse(string address)
        {
            using (var client = new HttpClient())
            {
                Uri uri = new Uri(string.Format("http://maps.google.com/maps/api/geocode/json?address={0}", address));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsStringAsync().Result;

                return JsonUtils.ConvertToObject(result);
            }
        }

        public class Address
        {
            public string City { get; set; }
            public string Neighborhood { get; set; }
            public string Zipcode { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
        }
    }
}
