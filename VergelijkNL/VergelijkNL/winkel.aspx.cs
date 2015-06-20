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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["winkel"] == null)
                Response.Redirect("/product.aspx");

            int winkelid = Convert.ToInt32(Request.QueryString["winkel"]);

            Winkel deze = db.getWinkel(winkelid);

            naam.InnerText = deze.Naam;
            url.HRef = "http://" + deze.Website;

            foreach (string key in deze.Info.Keys)
            {
                TableRow rij = new TableRow();
                rij.Controls.Add(new TableCell() { Text = key });
                rij.Controls.Add(new TableCell() { Text = deze.Info[key] });

                fill.Controls.AddAt(0, rij);
            }

        }
    }
}