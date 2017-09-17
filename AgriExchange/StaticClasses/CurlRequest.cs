using AgriExchange.Models.SkyWise;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.StaticClasses
{
    static public class CurlRequest
    {
        public static void thing()
        {
            var client = new RestClient("https://insight.api.wdtinc.com/daily-high-temperature/35.191/-97.439");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "586e3421-1f96-a0fd-f4d7-5af711fba4e3");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic NjhmODM5MGE6ZDBmNTc2ZjM3MjYxZTk4NWUzZmE4YTk3YTZlMDUyNTg=");
            IRestResponse<WeatherData> response = client.Execute<WeatherData>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(response.Data.startDate);
                Console.WriteLine(response.Data.endDate);
            }
        }
        //possibly make the curl request return an http request may get rid of this class
    }
}