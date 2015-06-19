using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VergelijkNL.Models
{
    /* Informatie:
     * -----------
     * De winkel klasse bevat naast een naam en een website ook een logo en nog veel meer andere gegevens.
     *                                                                                                                      */
    public class Winkel
    {
        // Id om later nog te gebruiken voor de database
        private int id;

        // Id reader
        public int Id { get { return id; } }

        public string Naam { get; set; }
        public string LogoPath { get; set; }
        public string Website { get; set; }

        public List<Dictionary<string, string>> Info { get; set; }

        // Constructor
        public Winkel (int id, string naam, string logo, string url, List<Dictionary<string, string>> info){
            this.id = id;

            Naam = naam;
            LogoPath = logo;
            Website = url;
            Info = info;
        }
    }
}