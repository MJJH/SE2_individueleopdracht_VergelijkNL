using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VergelijkNL.Models
{
    /* Informatie:
     * -----------
     * User wordt niet gebruikt, omdat er geen gebruik wordt gemaakt van een login / registratie
     * Dit is de oude omschrijving van deze klasse
     * ::
     * De class user heeft in de huidige vorm van het project geen grote functie. 
     * Op de orignele website van http://vergelijk.nl staat geen mogelijkheid om je te registreren of aan te melden.
     * Als deze in de toekomst toch toegevoegd zou worden aan deze kopie, zou deze klasse verder uitgebreid worden met
     * een id en mogelijk volldige naam. Maar voor nu blijft het bij het opslaan van een gebruikersnaam.
     *                                                                                                                      */

    public class _notUsed_User
    {
        // Properties
        public String Username { get; set; }

        // Constructor
        public _notUsed_User(String username)
        {
            // Sla username op in het object
            Username = username;
        }
    }
}