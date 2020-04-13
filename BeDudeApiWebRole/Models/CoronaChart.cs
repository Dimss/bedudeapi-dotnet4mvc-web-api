using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeDudeApiWebRole.Models
{
    public class CoronaChart
    {

        public CoronaChart(string date, decimal quantity, string quantityType)
        {
            this.date = date;
            this.quantity = quantity;
            this.quantityType = quantityType;

        }

        public string date { get; set; }

        public decimal quantity { get; set; }

        public string quantityType { get; set; }
    }
}