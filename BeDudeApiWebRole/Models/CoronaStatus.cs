﻿using System;
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

        public decimal Confirmed { get; set; }

        public decimal Deaths { get; set; }

        public decimal Recovered { get; set; }

        public decimal Active { get; set; }

        public string Date { get; set; }

    }
}