using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VergelijkNL.Models
{
    public class Verkoop
    {
        public string Url { get; set; }
        public double Score { get; set; }
        public double Price { get; set; }

        // Constructor
        public Verkoop(string url, double score, double price)
        {
            Url = url;
            Score = Math.Round(score, 1);
            Price = Math.Round(price, 2);
        }
    }
}