using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VergelijkNL.Models
{
    /* Informatie:
     * -----------
     * Product review is een review die gemaakt voor een product in tegenstelling tot een winkel.
     * Stamt af van de abstract class Review en haalt daar alle variabelen van. 
     * Deze parent zal aangemaakt worden in de constructor
     *                                                                                                                      */
    public class ProductReview : Review
    {
        // Properties
        public Product Voor { get; set; }
        public Boolean Bezit { get; set; }

        // Constructor
        public ProductReview (int id, string auteur, string inhoud, DateTime verzonden, Boolean aanrader, List<Dictionary<string, float>> beoordeling, Product voor, Boolean bezit) 
            // Constructor voor parent
            : base(id, auteur, inhoud, verzonden, aanrader)
        {
            // Sla de overige parameters op in het gemaakte object
            Voor = voor;
            Bezit = bezit;
        }
    }
}