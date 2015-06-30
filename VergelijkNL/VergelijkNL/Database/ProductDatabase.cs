using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VergelijkNL.Models;

namespace VergelijkNL.Database
{
    public class ProductDatabase : Database
    {

        // Krijg het volledige path van huidige cat tot aan root, gereturned in een string, string met Naam, Link (filtered naam)
        public Dictionary<string, string> getPathByCat(string categoryLink){
            categoryLink = strip(categoryLink);

            List<Dictionary<string, object>> get = getQuery("SELECT naam, link FROM categorie START WITH link = '" + categoryLink + "' CONNECT BY PRIOR hoofd = naam");
            
            // Draai alles om en convert naar stringen
            Dictionary<string, string> ret = new Dictionary<string,string>();

            foreach (Dictionary<string, object> row in get)
                ret.Add(row["naam"].ToString(), row["link"].ToString());

            return ret;
        }

        public Product getProduct(string link)
        {
            link = strip(link);
            List<Dictionary<string, object>> get = getQuery("SELECT id, c.link AS categorie, p.naam, p.link, merk FROM PRODUCT p JOIN CATEGORIE c ON c.naam = p.subcategorie WHERE p.link = '" + link + "'");
            
            // Niets gevonden
            if (get.Count < 1)
                return null;

            // Pak eerste rij
            Dictionary<string, object> row = get[0];
            Product create = new Product(Convert.ToInt32(row["id"]), row["categorie"].ToString(), row["naam"].ToString(), row["link"].ToString());

            // Krijg specificaties en voeg deze toe
            List<Dictionary<string, object>> specs = getQuery("SELECT 'merk' as naam, merk as waarde FROM product WHERE id = " + create.Id + " UNION ALL SELECT naam, waarde || ' ' || eenheid AS waarde FROM SPECIFICATIE s JOIN PRODUCTSSPECIFICATIE ps ON ps.Specificatie = s.naam WHERE product = " + create.Id);
            foreach (Dictionary<string, object> spec in specs)
                create.Specificaties.Add(spec["naam"].ToString(), spec["waarde"].ToString());
        
            // Krijg afbeeldingen
            List<Dictionary<string, object>> images = getQuery("SELECT title, filepath FROM plaatje WHERE product = " + create.Id);
            foreach (Dictionary<string, object> image in images)
                create.Plaatjes.Add(image["title"].ToString(), image["filepath"].ToString());

            // Krijg verkopers
            List<Dictionary<string, object>> verkopers = getQuery("SELECT winkel, prijs, paginaurl, score FROM WINKELVERKOOPT WHERE product = " + create.Id);
            foreach (Dictionary<string, object> verkoper in verkopers)
                create.Verkopers.Add(
                    new WinkelDatabase().getWinkel(Convert.ToInt32(verkoper["winkel"])), 
                    new Verkoop(verkoper["paginaurl"].ToString(), Convert.ToDouble(verkoper["score"]), Convert.ToDouble(verkoper["prijs"])));

            return create;
        }

        // Krijg een lijst met alle categorien op hetzelfde niveau als de huidige categorie of product
        public Dictionary<string, string> getNavListSame(string link)
        {
            link = strip(link);
            if (link == string.Empty || link == null)
                link = "root";

            List<Dictionary<string, object>> get = getQuery("SELECT * FROM ( SELECT naam, link FROM categorie WHERE NVL(hoofd, 'root') = (SELECT NVL(hoofd, 'root') FROM categorie WHERE link = '" + link + "') OR hoofd IS NULL AND 'root' = '" + link + "' UNION ALL SELECT naam, link FROM PRODUCT WHERE SUBCATEGORIE = (SELECT NVL(hoofd, 'root') FROM categorie WHERE link = '" + link + "') OR subcategorie = (SELECT subcategorie FROM PRODUCT WHERE link = '" + link + "')) ORDER BY naam");
            if (get.Count < 1)
                return null;

            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach(Dictionary<string,object> rij in get)
                ret.Add(rij["naam"].ToString(), rij["link"].ToString());

            return ret;
        }

        // Krijg een lijst met alle categorien en producten onder de huidige
        public Dictionary<string, string> getNavListUnder(string link)
        {
            link = strip(link);
            if (link == string.Empty || link == null)
                link = "root";

            List<Dictionary<string, object>> get = getQuery("SELECT * FROM ( SELECT naam, link FROM categorie WHERE NVL(hoofd, 'root') = (SELECT naam FROM CATEGORIE WHERE link = '" + link + "') UNION ALL SELECT naam, link FROM product WHERE SUBCATEGORIE = (SELECT naam FROM CATEGORIE WHERE link = '" + link + "') ) ORDER BY naam");
            if (get.Count < 1)
                return null;

            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach (Dictionary<string, object> rij in get)
                ret.Add(rij["naam"].ToString(), rij["link"].ToString());

            return ret;
        }
    }
}