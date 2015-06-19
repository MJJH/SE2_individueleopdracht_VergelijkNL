using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VergelijkNL.Models
{
    /* Informatie:
     * -----------
     * Een product is het belangrijkste onderdeel van dit gehele systeem. Hierin worden gegevens over producten opgeslagen
     * zoals een naam, een URL (dit is een naam zonder spaties of vreemde tekens) en het merk van dit product.
     * 
     * Ook worden winkels opgeslagen met het aantal kosten dat die winkel dit product verkoopt.
     * 
     * Verdere specificaties worden opgeslagen in een lijst met een key naam, en een samengevoegde waarde + eenheid.
     *                                                                                                                      */
    public class Product
    {
        // id voor later database
        private int id;

        // Reader voor Id
        public int Id { get { return id; } }

        public string Parent { get; set; }
        public string Naam { get; set; }
        public string Link { get; set; }

        public Dictionary<string, string> Specificaties { get; set; }

        // Lijst van alle plaatjes met als sleutel de titel en value het path naar het bestand
        public Dictionary<string, string> Plaatjes { get; set; }

        // Lijst van alle verkopende winkels en bijbehorende informatie
        public Dictionary<Winkel, Verkoop> Verkopers { get; set; }

        // Constructor
        public Product(int id, string parent, string naam, string link)
        {
            this.id = id;
            Parent = parent;
            Naam = naam;
            Link = link;
            Specificaties = new Dictionary<string, string>();
            Plaatjes = new Dictionary<string, string>();
            Verkopers = new Dictionary<Winkel, Verkoop>();
            
        }
    }
}