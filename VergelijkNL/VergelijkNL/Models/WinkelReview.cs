using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VergelijkNL.Models
{
    /* Informatie:
     * -----------
     * Winkel review is een review die gemaakt voor een Winkel in tegenstelling tot een product.
     * Stamt af van de abstract class Review en haalt daar alle variabelen van. 
     * Deze parent zal aangemaakt worden in de constructor
     *                                                                                                                      */
    public class WinkelReview : Review
    {
        // Properties
        public Winkel Voor { get; set; }

        // Constructor
        public WinkelReview(int id, string auteur, string inhoud, DateTime verzonden, Boolean aanrader, double beoordeling, Dictionary<string, double> beoordelingen, Winkel voor) 
            // Constructor voor parent
            : base(id, auteur, inhoud, verzonden, aanrader, beoordeling, beoordelingen)
        {
            // Sla de overige parameters op in het gemaakte object
            Voor = voor;
        }
    }
}