using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeDudeApiWebRole.Models
{
    public class CoronaStatus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public decimal Cases  { get; set; }

        public string Status { get; set; }

        public string Date { get; set; }

    }
}