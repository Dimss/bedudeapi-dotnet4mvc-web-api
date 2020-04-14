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


        [Route("load")]
        [HttpGet]
        public void LoadCoronaData()
        {
            string constr = RoleEnvironment.GetConfigurationSettingValue("MongoDBConnectionString");
            var Client = new MongoClient(constr);
            var db = Client.GetDatabase("CoronaStatusDb");
            var collection = db.GetCollection<CoronaStatus>("CoronaStatus");
            var coronaBaseApi = RoleEnvironment.GetConfigurationSettingValue("CoronaAPI");
            foreach (var statusName in new List<string>() { "confirmed", "recovered", "deaths" })
            {
                var apiUrl = coronaBaseApi + statusName;
                var json = new WebClient().DownloadString(apiUrl);
                List<CoronaStatus> coronaStatues = JsonConvert.DeserializeObject<List<CoronaStatus>>(json);
                var existingStatuses = collection.Find(new BsonDocument()).ToList();
                foreach (var status in coronaStatues)
                {
                    var insertNewStatus = true;
                    foreach (var existingStatus in existingStatuses)
                    {
                        if (status.Date == existingStatus.Date && status.Status == existingStatus.Status)
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
