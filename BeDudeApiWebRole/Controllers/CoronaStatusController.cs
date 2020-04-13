using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using BeDudeApiWebRole.Models;
using Newtonsoft.Json;
using Microsoft.Azure;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace BeDudeApiWebRole.Controllers
{
    [RoutePrefix("api/corona")]
    public class CoronaStatusController : ApiController
    {

        [Route("status")]
        [HttpGet]
        public object GetCoronaStatues()
        {
            string constr = RoleEnvironment.GetConfigurationSettingValue("MongoDBConnectionString");
            var Client = new MongoClient(constr);
            var db = Client.GetDatabase("CoronaStatusDb");
            var collection = db.GetCollection<CoronaStatus>("CoronaStatus").Find(new BsonDocument()).ToList();
            return Json(collection);
        }

        [Route("chart")]
        [HttpGet]
        public object GetCoronaChart()
        {
            
            string constr  = RoleEnvironment.GetConfigurationSettingValue("MongoDBConnectionString");
            var Client = new MongoClient(constr);
            var db = Client.GetDatabase("CoronaStatusDb");
            var collection = db.GetCollection<CoronaStatus>("CoronaStatus").Find(new BsonDocument()).ToList();
            List<CoronaChart> coronaStatuChartData = new List<CoronaChart>();
            foreach (var coronaStatus in collection)
            {
                var date = coronaStatus.Date.Substring(0, coronaStatus.Date.IndexOf("T"));
                coronaStatuChartData.Add(new CoronaChart(date, coronaStatus.Confirmed, "configmed"));
                coronaStatuChartData.Add(new CoronaChart(date, coronaStatus.Active, "active"));
                coronaStatuChartData.Add(new CoronaChart(date, coronaStatus.Deaths, "deaths"));
                coronaStatuChartData.Add(new CoronaChart(date, coronaStatus.Recovered, "recovered"));
            }

            return Json(coronaStatuChartData);

        }


        [Route("load")]
        [HttpGet]
        public void LoadCoronaData()
        {
            string constr = RoleEnvironment.GetConfigurationSettingValue("MongoDBConnectionString");
            var Client = new MongoClient(constr);
            var db = Client.GetDatabase("CoronaStatusDb");
            var collection = db.GetCollection<CoronaStatus>("CoronaStatus");
            var json = new WebClient().DownloadString(RoleEnvironment.GetConfigurationSettingValue("CoronaAPI"));
            List<CoronaStatus> coronaStatues = JsonConvert.DeserializeObject<List<CoronaStatus>>(json);
            var existingStatuses = collection.Find(new BsonDocument()).ToList();
            if (existingStatuses.Count == 0)
            {
                collection.InsertMany(coronaStatues);
            }
            else
            {
                foreach (var status in coronaStatues)
                {
                    var insertNewStatus = true;
                    foreach (var existingStatus in existingStatuses)
                    {
                        if (status.Date == existingStatus.Date)
                        {
                            insertNewStatus = false;
                        }

                    }

                    if (insertNewStatus)
                    {
                        collection.InsertOne(status);
                    }
                }
            }
        }
    }
}
