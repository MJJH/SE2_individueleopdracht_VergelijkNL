using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VergelijkNL.Database;
using VergelijkNL.Models;

namespace VergelijkNL
{
    public partial class product : System.Web.UI.Page
    {
        private string currentPath;

        // Wordt alleen gevuld als path een product is
        private Product currentProduct;

        private ProductDatabase db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new ProductDatabase();

            if (Request.QueryString["path"] != null)
            {
                // Vind of je bij een product of categorie bent
                setUp();

                // Vul het volledige path aan
                createPath();
                
                // Als het om een product gaat, geeft dan alle plaatjes en gegevens van deze producten
                if (currentProduct != null)
                    fillInfo();
            }

            createNav();
        }

        // Kijk of bestand een product of category is, en als het een product is pak dan zijn category
        private void setUp()
        {
            // Als product niet gevonden
            if ((currentProduct = db.getProduct(Request.QueryString["path"])) != null)
            {
                currentPath = currentProduct.Parent;
            }
            else
            {
                currentPath = Request.QueryString["path"];
            }

        }

        // Vult de path aan met het gehele path van root tot huidige categorie of product.
        private void createPath()
        {
            Dictionary<string, string> paths = db.getPathByCat(currentPath);

            HyperLink homelink = new HyperLink() { ID = "home", CssClass = "pathUrl", NavigateUrl = "/product.aspx", Text = "Vergelijk.nl" };

            foreach (string key in paths.Keys)
            {
                HyperLink link = new HyperLink() { ID = paths[key], CssClass = "pathUrl", NavigateUrl = "?path=" + paths[key], Text = key };

                if (paths[key] == Request.QueryString["path"])
                    link.CssClass += " current";

                // Link toevoegen met 'hoogste' eerst
                path.Controls.AddAt(0, link);
                
                // Voeg een schijder toe
                path.Controls.AddAt(1, new Label() { Text = " / " }); 
            }

            // Voeg product naam toe
            if (currentProduct != null)
            {
                HyperLink pLink = new HyperLink() { ID = currentProduct.Link, CssClass = "pathUrl current", NavigateUrl = "?path=" + currentProduct.Link, Text = currentProduct.Naam };
                path.Controls.Add(pLink);
            }

            // Link toevoegen met 'hoogste' eerst
            path.Controls.AddAt(0, homelink);
            // Voeg een schijder toe
            path.Controls.AddAt(1, new Label() { Text = " / " }); 
        }

        private void fillInfo()
        {
            content.Attributes.Remove("class");
            content.InnerHtml = String.Empty;

            #region basic
            // Naam
            Panel pName = new Panel() { ID = "Naam" };
            Label name = new Label() { Text = currentProduct.Specificaties["merk"] + " " + currentProduct.Naam };
            content.Controls.Add(pName);
            pName.Controls.Add(name);

#endregion

            #region afbeeldingen
            // Plaatjes
            Panel images = new Panel() { ID = "Afbeeldingen" };
            foreach (string title in currentProduct.Plaatjes.Keys)
                images.Controls.Add(new Image() { AlternateText = title, ImageUrl = currentProduct.Plaatjes[title], CssClass = "img-responsive img-thumbnail" });
            content.Controls.Add(images);
            content.Controls.Add(images);
            #endregion

            content.Controls.Add(new Literal() { Text = "<hr/>" });

            #region verkoop
            // Info
            Panel verkopers = new Panel() { ID = "Verkopers", CssClass = "table-responsive" };
            Table tVerkopers = new Table() { CssClass = "table table-hover" };
            
            // Add table titles
            TableHeaderRow header = new TableHeaderRow();
            header.Controls.Add(new TableHeaderCell() { Text = "Winkel" });
            header.Controls.Add(new TableHeaderCell() { Text = "Ga" });
            header.Controls.Add(new TableHeaderCell() { Text = "Score" });
            header.Controls.Add(new TableHeaderCell() { Text = "Prijs" });
            tVerkopers.Controls.Add(header);

            foreach (Winkel verkoper in currentProduct.Verkopers.Keys)
            {
                Verkoop v = currentProduct.Verkopers[verkoper];
                TableRow rij = new TableRow();

                // Website - Link naar website informatie pagina
                HyperLink link = new HyperLink() { NavigateUrl = "/winkel.aspx?winkel=" + verkoper.Id, Text = verkoper.Naam };
                TableCell cWinkel = new TableCell();
                cWinkel.Controls.Add(link);

                // Link naar pagina van website met product
                HyperLink product = new HyperLink() { NavigateUrl = "http://" + verkoper.Website + "/" + v.Url, Text = "Bekijk op website" };
                TableCell cProduct = new TableCell();
                cProduct.Controls.Add(product);

                // Score van het product
                TableCell score = new TableCell();
                score.Controls.Add(new Label() { Text = v.Score.ToString() });

                // Verkoopprijs van het product voor deze winkel
                TableCell prijs = new TableCell();
                prijs.Controls.Add(new Label() { Text = "&euro;" + v.Price });

                rij.Controls.Add(cWinkel);
                rij.Controls.Add(cProduct);
                rij.Controls.Add(score);
                rij.Controls.Add(prijs);
                tVerkopers.Controls.Add(rij);
            }

            verkopers.Controls.Add(tVerkopers);
            content.Controls.Add(verkopers);
            #endregion

            content.Controls.Add(new Literal() { Text = "<hr/>" });

            #region specs
            // Maak nieuwe tabel
            Panel specs = new Panel() { ID = "specificaties", CssClass = "table-responsive" };
            Table tSpecs = new Table() { CssClass = "table table-striped" };

            // Maak de header met titels
            TableHeaderRow specHeader = new TableHeaderRow();
            specHeader.Controls.Add(new TableHeaderCell() { Text = "Eigenschap" });
            specHeader.Controls.Add(new TableHeaderCell() { Text = "Waarde" });
            tSpecs.Controls.Add(specHeader);

            foreach (string key in currentProduct.Specificaties.Keys)
            {
                TableRow rij = new TableRow();
                rij.Controls.Add(new TableCell() { Text = key });
                rij.Controls.Add(new TableCell() { Text = currentProduct.Specificaties[key] });

                tSpecs.Controls.Add(rij);
            }

            specs.Controls.Add(tSpecs);
            content.Controls.Add(specs);
            #endregion
        }

        private void createNav()
        {
            // Maak de navigatie voor de dingen op hetzelfde niveau
            Dictionary<string, string> listsame = db.getNavListSame(currentPath);
            if(listsame == null)
                listsame = db.getNavListSame(null);

            foreach (string key in listsame.Keys)
            {
                HyperLink link = new HyperLink() { Text = " " + key + " ", NavigateUrl = "?path=" + listsame[key] };
                    if(listsame[key] == currentPath)
                        link.CssClass = "current";

                sameHier.Controls.Add(link);
            }

            // Maak de navigatie voor de dingen onder dit niveau
            Dictionary<string, string> listchild = db.getNavListUnder(currentPath);

            if (listchild == null)
                return;
            
            foreach (string key in listchild.Keys)
            {
                HyperLink link = new HyperLink() { Text = " " + key + " ", NavigateUrl = "?path=" + listchild[key] };
                if (currentProduct != null && listchild[key] == currentProduct.Link)
                    link.CssClass = "current";
                children.Controls.Add(link);
            }
        }
    }
}