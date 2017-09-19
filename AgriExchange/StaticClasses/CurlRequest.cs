using AgriExchange.Models.SkyWise;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AgriExchange.StaticClasses
{
    static public class CurlRequest
    {

        public static void Curl(string hourlyTemp, string location)
        {
            List<string> paramaters = new List<string>();
            paramaters.Add("daily-high-temperature");
            paramaters.Add("daily-low-temperature");
            paramaters.Add("daily-precipitation");
            List<object> tempHold = new List<object>();

            //find a way to enter peramater for search
            foreach (var paramater in paramaters)
            {
                
                var client = new RestClient("https://insight.api.wdtinc.com/" + paramater + location);
                var request = new RestRequest(Method.GET);
                request.AddHeader("postman-token", "586e3421-1f96-a0fd-f4d7-5af711fba4e3");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic NjhmODM5MGE6ZDBmNTc2ZjM3MjYxZTk4NWUzZmE4YTk3YTZlMDUyNTg=");
                IRestResponse<WeatherData> response = client.Execute<WeatherData>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var listTempData = response.Data.series;
                    foreach (var item in listTempData)
                    {
                        tempHold.Add(item);
                    }
                }
            }
        }
        public static void Http(string search, string typeSearch)
        {
            var client = new RestClient("http://tropicalfruitandveg.com/api/tfvjsonapi.php?"+ typeSearch + search);
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "3a996033-aeaa-9350-80e7-b7f1ff8c0e90");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic NjhmODM5MGE6ZDBmNTc2ZjM3MjYxZTk4NWUzZmE4YTk3YTZlMDUyNTg=");
            IRestResponse<FruitData> httpResponse = client.Execute<FruitData>(request);

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var convertedData = JsonConvert.DeserializeObject<FruitData>(httpResponse.Content);
                foreach (var item in convertedData.results)
                {
                    var hold = item;
                }

            }
        }
        //possibly make the curl request return an http request may get rid of this class
    }
}