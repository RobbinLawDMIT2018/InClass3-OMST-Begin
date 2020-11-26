using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using OMSTSystem.BLL;
using OMSTSystem.ViewModels;
#endregion

namespace OMSTWebsite.Assessments
{
    public partial class PurchaseTickets : System.Web.UI.Page
    {
        #region  Student Work Here....
        protected void Buy_Click(object sender, EventArgs e)
        {
            // TODO: Enter your code here for the Buy_Click() behaviour
            //your code must 
            // a) check at least one ticket is purchased by Teen, Adult or Senior
            //       (Theatre rule: NO unattended children or infants)
            // b) collect the following data ShowTimeID, Premium Ticket price, and for each
            //       ticket category (the category and number of tickets) using the ViewModel TicketRequest
            // c) pass the data for processing to Tickets_BuyTickets() under control of the
            //       user control error handler.
            // d) display a success message if ticket buying was completed.



        }
        #endregion

        #region DO NOT MODIFY
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTicketCounts();
            }
        }
        protected void LoadTicketCounts()
        {
            for (int x = 0; x < 20; x++)
            {
                InfantTickets.Items.Insert(x, x.ToString());
                ChildTickets.Items.Insert(x, x.ToString());
                TeenTickets.Items.Insert(x, x.ToString());
                AdultTickets.Items.Insert(x, x.ToString());
                SeniorTickets.Items.Insert(x, x.ToString());
            }
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void Clear_Click(object sender, EventArgs e)
        {

            ClearShowTimeTicketsPanel();
            MoviePanel.Visible = false;
            MovieList.DataSource = null;
            MovieList.DataBind();
            LocationList.SelectedIndex = -1;
        }

        protected void ClearShowTimeTicketsPanel()
        {
            ShowTimesTicketsPanel.Visible = false;
            InfantTickets.SelectedIndex = 0;
            ChildTickets.SelectedIndex = 0;
            TeenTickets.SelectedIndex = 0;
            AdultTickets.SelectedIndex = 0;
            SeniorTickets.SelectedIndex = 0;
            TotalPrice.Text = "0.00";
            TicketPremium.Text = "0.00";
            Buy.Enabled = false;
        }

        protected void LocationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearShowTimeTicketsPanel();
            MessageUserControl.TryRun(() =>
            {
                var sysmgr = new TicketsController();
                var movies = sysmgr.ShowTimes_MoviesByLocations(int.Parse(LocationList.SelectedValue));
                if (movies == null)
                {
                    throw new Exception("No movies schedule at this location at this date.");
                }
                else
                {
                    MovieList.DataSource = movies;
                    MovieList.DataTextField = nameof(KeyValueOption<int>.DisplayText);
                    MovieList.DataValueField = nameof(KeyValueOption<int>.DisplayValue);
                    MovieList.DataBind();
                    MoviePanel.Visible = true;
                }
            });
        }

        protected void MovieList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearShowTimeTicketsPanel();
            string locationid = LocationList.SelectedValue;
            string movieid = MovieList.SelectedValue;
            List<MovieShowTimes> showtimes;
            MessageUserControl.TryRun(() =>
            {
                var tcsysmgr = new TicketsController();
                showtimes = tcsysmgr.ShowTimes_ShowTimesByMoviesByLocations(int.Parse(locationid),
                                                                        int.Parse(movieid));
                if (showtimes == null)
                {
                    throw new Exception("Movies schedule at this location have changed, select location again.");
                }
                else
                {
                    decimal ticketpremium = tcsysmgr.Movies_GetTicketPrices(int.Parse(movieid));
                    TicketPremium.Text = string.Format("{0:0.00}", ticketpremium);

                    ShowTimeList.DataSource = showtimes;
                    ShowTimeList.DataTextField = "ScheduleTime";
                    ShowTimeList.DataValueField = "ShowTimeID";
                    ShowTimeList.DataBind();

                    var ticketcategories = tcsysmgr.TicketCategory_List();
                    foreach (var item in ticketcategories)
                    {
                        switch (item.Description)
                        {
                            case "Infant":
                                {
                                    InfantPrice.Text = item.TicketPrice.ToString("0.00");
                                    break;
                                }
                            case "Child":
                                {
                                    ChildPrice.Text = item.TicketPrice.ToString("0.00");
                                    break;
                                }
                            case "Teen":
                                {
                                    TeenPrice.Text = item.TicketPrice.ToString("0.00");
                                    break;
                                }
                            case "Adult":
                                {
                                    AdultPrice.Text = item.TicketPrice.ToString("0.00");
                                    break;
                                }
                            case "Senior":
                                {
                                    SeniorPrice.Text = item.TicketPrice.ToString("0.00");
                                    break;
                                }

                        }
                    }
                    TotalPrice.Text = "$ 0.00";
                    ShowTimesTicketsPanel.Visible = true;
                }
            });
        }

        protected void ShowTimeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Buy.Enabled = true;
        }

        protected void ChangeTotal(object sender, EventArgs e)
        {
            var infant = InfantTickets.SelectedValue.ToInt() * InfantPrice.Text.ToMoney();
            var child = ChildTickets.SelectedValue.ToInt() * (ChildPrice.Text.ToMoney() + TicketPremium.Text.ToMoney());
            var teen = TeenTickets.SelectedValue.ToInt() * (TeenPrice.Text.ToMoney() + TicketPremium.Text.ToMoney());
            var adult = AdultTickets.SelectedValue.ToInt() * (AdultPrice.Text.ToMoney() + TicketPremium.Text.ToMoney());
            var senior = SeniorTickets.SelectedValue.ToInt() * (SeniorPrice.Text.ToMoney() + TicketPremium.Text.ToMoney());

            decimal totalprice = infant + child + teen + adult + senior;
            TotalPrice.Text = totalprice.ToString("C");
        }
        #endregion
    }
    public static class InputParseExtensions
    {
        public static int ToInt(this string self) => int.Parse(self);
        public static decimal ToMoney(this string self) => decimal.Parse(self, System.Globalization.NumberStyles.Currency);
    }
}