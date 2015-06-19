using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VergelijkNL.Models
{
    /* Informatie:
     * -----------
     * Abstracte klasse waarin gegevens worden opgeslagen uit de database voor een Review.
     * Deze abstracte klassen wordt gebruikt voor ProductReview en WinkelReview die hier van af erven om de velden te krijgen.
     * 
     * Wordt later alleen nog uitgelezen om reviews te tonen op het scherm.
     * 
     * Wordt ook gebruikt om nieuwe reviews te maken. Als dit een uitgebreidere opdracht zou zijn, zou hier dan de nieuwe data
     * worden gecontroleerd op voldoend van eisen.
     * 
     * --
     * 
     * Ik heb gebruik gemaakt van een String voor Auteur omdat ik voor deze versie geen gebruik maak van een login / registratie
     * deel omdat dit ook niet gebruikt is in de originele website. Er komt in plaats daarvan een input veld voor Naam
     * en die waarde wordt hij vervolgens gebruikt.
     *                                                                                                                      */

    public abstract class Review
    {
        // Private ID voor database gebruik
        private int id;

        // Reader voor het ID
        public int Id { get { return id; } }

        // Properties
        public string Inhoud { get; set; }
        public Boolean Aanrader { get; set; }
        public List<Dictionary<string, float>> Beoordelingen { get; set; }
        public DateTime Tijdstip { get; set; }
        public string Auteur { get; set; }

        // Constructor
        public Review(int id, string auteur, string inhoud, DateTime verzonden, Boolean aanrader, List<Dictionary<string, float>> beoordeling)
        {
            // Sla alle gegevens op uit de constructor parameters in de instance variabelen.
            this.id = id;

            Auteur = auteur;
            Inhoud = inhoud;
            Tijdstip = verzonden;
            Aanrader = aanrader;

            // Dit is een lijst met beoordelingen, de naam van de beoordeling en de daarvoor gegeven punten. 0.0 - 5.0
            Beoordelingen = beoordeling;
        }
    }
}