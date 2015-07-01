using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VergelijkNL.Models;

namespace VergelijkNL.Database
{
    public class WinkelDatabase : Database
    {
        public Winkel getWinkel(int id)
        {
            List<Dictionary<string, object>> get = getQuery("SELECT naam, logourl, websiteurl from winkel where id = " + id);
            if (get.Count < 1)
                return null;

            // Pak bovenste rij (en enigste rij)
            Dictionary<string, object> row = get[0];
            Winkel create = new Winkel(id, row["naam"].ToString(), row["logourl"].ToString(), row["websiteurl"].ToString());

            // Pak extra informatie
            List<Dictionary<string, object>> specs = getQuery("SELECT 'Adres' AS naam, straat || ' ' || nummer || ', ' || postcode || ' ' || stad AS waarde FROM WINKEL WHERE id = " + create.Id + " UNION ALL SELECT 'Mailadres' AS naam, EMailAdres AS Waarde FROM WINKEL WHERE id = " + create.Id + " UNION ALL SELECT 'Oprichtjaar' AS naam, TO_CHAR(oprichtjaar) AS Waarde FROM WINKEL WHERE id = " + create.Id + " UNION ALL SELECT i.naam AS naam, wei.waarde AS waarde FROM INFOCATEGORIE i JOIN WINKELEXTRAINFO wei ON wei.extranaam = i.naam WHERE wei.winkel = " + create.Id);
            foreach (Dictionary<string, object> spec in specs)
                create.Info.Add(spec["naam"].ToString(), spec["waarde"].ToString());

            create.Reviews = new ReviewDatabase().getReviews(create);

            return create;
        }
    }
}