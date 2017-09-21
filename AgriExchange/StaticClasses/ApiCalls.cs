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
using AgriExchange.Models;
using AgriExchange.Models.ApiDataClasses;

namespace AgriExchange.StaticClasses
{
    static public class ApiCalls
    {

        public static void WeatherApi(string hourlyTemp, string location)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<string> paramaters = new List<string>();
            paramaters.Add("daily-high-temperature");
            paramaters.Add("daily-low-temperature");
            paramaters.Add("daily-precipitation");
            List<seriesItem> allData = new List<seriesItem>();
            List<seriesItem> DayOne = new List<seriesItem>();
            List<seriesItem> DayTwo = new List<seriesItem>();
            List<seriesItem> DayThree = new List<seriesItem>();
            List<seriesItem> DayFour = new List<seriesItem>();
            List<seriesItem> DayFive = new List<seriesItem>();

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
                        allData.Add(item);
                    }
                }
            }
            foreach (var item in allData)
            {
                if (item.validDate == allData[0].validDate)
                {
                    DayOne.Add(item);
                }
                else if (item.validDate == allData[1].validDate)
                {
                    DayTwo.Add(item);
                }
                else if (item.validDate == allData[2].validDate)
                {
                    DayThree.Add(item);
                }
                else if (item.validDate == allData[3].validDate)
                {
                    DayFour.Add(item);
                }

                else if (item.validDate == allData[4].validDate)
                {
                    DayFive.Add(item);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    var day = (from data in context.Forcasts where data.ID == i + 1 select data).First();
                    day.Date = allData[i].validDate;
                    if (i == 0)
                    {
                        day.Date = DayOne[0].validDate;
                        day.HighTemp = float.Parse(DayOne[0].value);
                        day.LowTemp = float.Parse(DayOne[1].value);
                        day.Percipitation = float.Parse(DayOne[2].value);
                    }
                    else if (i == 1)
                    {
                        day.Date = DayTwo[0].validDate;
                        day.HighTemp = float.Parse(DayTwo[0].value);
                        day.LowTemp = float.Parse(DayTwo[1].value);
                        day.Percipitation = float.Parse(DayTwo[2].value);
                    }
                    else if (i == 2)
                    {
                        day.Date = DayThree[0].validDate;
                        day.HighTemp = float.Parse(DayThree[0].value);
                        day.LowTemp = float.Parse(DayThree[1].value);
                        day.Percipitation = float.Parse(DayThree[2].value);
                    }
                    else if (i == 3)
                    {
                        day.Date = DayFour[0].validDate;
                        day.HighTemp = float.Parse(DayFour[0].value);
                        day.LowTemp = float.Parse(DayFour[1].value);
                        day.Percipitation = float.Parse(DayFour[2].value);
                    }
                    else if (i == 4)
                    {
                        day.Date = DayFive[0].validDate;
                        day.HighTemp = float.Parse(DayFive[0].value);
                        day.LowTemp = float.Parse(DayFive[1].value);
                        day.Percipitation = float.Parse(DayFive[2].value);
                    }
                    context.SaveChanges();
                }
                catch
                {
                    Forcast forcast = new Forcast();
                    forcast.ID = i + 1;
                    forcast.Date = allData[i].validDate;
                    context.Forcasts.Add(forcast);
                    context.SaveChanges();
                    WeatherApi(hourlyTemp, location);
                }
            }
        }
        
    

        public static void FruitApi(string search, string typeSearch)
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

        public static void GeoLocationApi(string convertedAddress)
        {            

            var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/json?address=" + convertedAddress + "&key=AIzaSyCCt_tk8Is_0wRtffA3H0YHZEs_8ZwRO3U");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "d0687862-1bdc-4966-e25f-ad919249e058");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic QUl6YVN5Q0N0X3RrOElzXzB3UnRmZkEzSDBZSFpFc184WndSTzNVOg==");
            IRestResponse<GeoLocationData> response = client.Execute<GeoLocationData>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var lattitude = response.Data.results[0].geometry[0].location[0].lat;
                var longitude = response.Data.results[0].geometry[0].location[0].lng;
            }
        }
    }
}