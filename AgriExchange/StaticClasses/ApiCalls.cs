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
            List<seriesItem> tempHold = new List<seriesItem>();
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
                        tempHold.Add(item);
                    }
                }
            }
            foreach (var item in tempHold)
            {
                if (item.validDate == tempHold[0].validDate)
                {
                    DayOne.Add(item);
                }
                else if (item.validDate == tempHold[1].validDate)
                {
                    DayTwo.Add(item);
                }
                else if (item.validDate == tempHold[2].validDate)
                {
                    DayThree.Add(item);
                }
                else if (item.validDate == tempHold[3].validDate)
                {
                    DayFour.Add(item);
                }

                else if (item.validDate == tempHold[4].validDate)
                {
                    DayFive.Add(item);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                //  WHERE THE EXCEPTION IS BEING THROWN
                try
                {
                    var day = (from data in context.Forcasts where data.ID == i + 1 select data).First();
                    day.Date = tempHold[i].validDate;
                    if (i == 0)
                    {
                        day.HighTemp = (float)int.Parse(DayOne[0].value);
                        day.LowTemp = (float)int.Parse(DayOne[1].value);
                        day.Percipitation = (float)int.Parse(DayOne[3].value);
                    }
                    else if (i == 1)
                    {
                        day.HighTemp = (float)int.Parse(DayTwo[0].value);
                        day.LowTemp = (float)int.Parse(DayTwo[1].value);
                        day.Percipitation = (float)int.Parse(DayTwo[3].value);
                    }
                    else if (i == 2)
                    {
                        day.HighTemp = (float)int.Parse(DayThree[0].value);
                        day.LowTemp = (float)int.Parse(DayThree[1].value);
                        day.Percipitation = (float)int.Parse(DayThree[3].value);
                    }
                    else if (i == 3)
                    {
                        day.HighTemp = (float)int.Parse(DayFour[0].value);
                        day.LowTemp = (float)int.Parse(DayFour[1].value);
                        day.Percipitation = (float)int.Parse(DayFour[3].value);
                    }
                    else if (i == 4)
                    {
                        day.HighTemp = (float)int.Parse(DayFive[0].value);
                        day.LowTemp = (float)int.Parse(DayFive[1].value);
                        day.Percipitation = (float)int.Parse(DayFive[3].value);
                    }
                    context.Forcasts.Add(day);
                    context.SaveChanges();
                }
                catch
                {
                    Forcast forcast = new Forcast();
                    forcast.ID = i + 1;
                    forcast.Date = tempHold[i].validDate;
                    context.Forcasts.Add(forcast);
                    context.SaveChanges();
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
        //possibly make the curl request return an http request may get rid of this class
    }
}