using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VergelijkNL.Database;
using VergelijkNL.Models;

namespace VergelijkNL
{
    public partial class winkel : System.Web.UI.Page
    {
        WinkelDatabase db = new WinkelDatabase();
        string[] beoordelingen = { "Levering", "Prijzen" };
        Winkel deze;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["winkel"] == null)
                Response.Redirect("/product.aspx");

            int winkelid = Convert.ToInt32(Request.QueryString["winkel"]);

            deze = db.getWinkel(winkelid);

            if(deze == null)
                Response.Redirect("/product.aspx");

            naam.InnerText = deze.Naam;
            url.HRef = "http://" + deze.Website;

            foreach (string key in deze.Info.Keys)
            {
                TableRow rij = new TableRow();
                rij.Controls.Add(new TableCell() { Text = key });
                rij.Controls.Add(new TableCell() { Text = deze.Info[key] });

                fill.Controls.AddAt(0, rij);
            }

            submit.ServerClick += submit_ServerClick;
            content.Controls.Add(new Literal() { Text = "<hr/>" });

            if (HttpContext.Current.Session["name"] != null)
                name.Value = HttpContext.Current.Session["name"].ToString();

            #region recensies
            Panel reviews = new Panel() { ID = "reviews", CssClass = "table-responsive" };

            foreach (WinkelReview r in deze.Reviews)
            {
                Panel review = new Panel() { CssClass = "review" };
                review.Controls.Add(new Label() { Text = r.Auteur.Username + ":", CssClass = "author" });
                review.Controls.Add(new Label() { Text = r.Inhoud, CssClass = "content" });

                review.Controls.Add(new Label() { Text = "Beoordeelt dit product: " + r.Beoordeling, CssClass = "points" });

                string text = "Aangeraden: ";
                if (r.Aanrader)
                    text += "✓";
                else
                    text += "×";

                review.Controls.Add(new Label() { Text = text, CssClass = "advise" });

                if (r.Beoordelingen.Count > 0)
                {
                    // Maak nieuwe tabel
                    Table tReviews = new Table() { CssClass = "table table-striped" };

                    // Maak de header met titels
                    TableHeaderRow revHeader = new TableHeaderRow();
                    revHeader.Controls.Add(new TableHeaderCell() { Text = "Eigenschap" });
                    revHeader.Controls.Add(new TableHeaderCell() { Text = "Beoordeling" });
                    tReviews.Controls.Add(revHeader);

                    foreach (string key in r.Beoordelingen.Keys)
                    {
                        TableRow rij = new TableRow();
                        rij.Controls.Add(new TableCell() { Text = key });
                        rij.Controls.Add(new TableCell() { Text = r.Beoordelingen[key].ToString("##.#") });

                        tReviews.Controls.Add(rij);
                    }

                    review.Controls.Add(tReviews);
                }

                reviews.Controls.AddAt(0, review);

                reviews.Controls.AddAt(1, new Literal() { Text = "<hr/>" });
            }

            content.Controls.Add(reviews);
            #endregion

            if (HttpContext.Current.Session["name"] != null)
                name.Value = HttpContext.Current.Session["name"].ToString();

            #region nieuw
            rate.Controls.Clear();

            string[] beoordelingen = { "Kwaliteit", "Prijs", "Houdbaarheid" };

            foreach (string k in beoordelingen)
            {
                Panel add = new Panel() { CssClass = "form-group" };
                add.Controls.Add(new Label() { Text = k });
                add.Controls.Add(new LiteralControl("<input type=\"number\" class=\"form-control\" min=\"0\" max=\"5\" value=\"3\" step=\"0.5\" id=\"rating\" name=\"" + k + "\" id=\"" + k + "\" required />"));

                rate.Controls.Add(add);
            }
            #endregion
        }

        void submit_ServerClick(object sender, EventArgs e)
        {
            if (Request["name"] == null || Request["name"] == "" || Request["name"].Length < 3)
            {
                Form.Controls.AddAt(0, new Label() { Text = "Vul een naam in van minimaal 3 tekens!", CssClass = "bg-danger full" });
                return;
            }

            if (Request["message"] == null || Request["message"] == "" || Request["message"].Length < 10)
            {
                Form.Controls.AddAt(0, new Label() { Text = "Vul een bericht in van minimaal 10 tekens!", CssClass = "bg-danger full" });
                return;
            }

            double d;
            if (Request["rating"] == null || Request["rating"] == "" || !(double.TryParse(Request["rating"], out d)) || d < 0 || d > 5)
            {
                Form.Controls.AddAt(0, new Label() { Text = "Vul een geldige waarde in voor de beoordeling", CssClass = "bg-danger full" });
                return;
            }

            Dictionary<string, double> beoordeling = new Dictionary<string, double>();
            foreach (string key in beoordelingen)
            {
                double w;
                if (Request[key] != null && Request[key] != "" && double.TryParse(Request[key], out w) && w >= 0 && w <= 5)
                    beoordeling.Add(key, w);
            }

            WinkelReview create = new WinkelReview(-1, Request["name"], Request["message"], DateTime.Now, false, d, beoordeling, deze);
            if (!new ReviewDatabase().writeReview(create))
            {
                Form.Controls.AddAt(0, new Label() { Text = "Er is iets fout gegaan. Review niet verstuurd.", CssClass = "bg-danger full" });
                return;
            }
            else
                Form.Controls.AddAt(0, new Label() { Text = "Review verzonden!", CssClass = "bg-success full" });

        }
    }
}