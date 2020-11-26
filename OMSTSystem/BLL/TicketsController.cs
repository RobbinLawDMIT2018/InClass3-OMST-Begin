using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using OMSTSystem.DAL;
using System.ComponentModel;
using OMSTSystem.ViewModels;
using OMSTSystem.Entities;
using FreeCode.Exceptions;
#endregion

namespace OMSTSystem.BLL
{
    [DataObject]
    public class TicketsController
    {
        #region Student Work Here....
        public void Tickets_BuyTickets(TicketRequest request)
        {
            // TODO: Place your code here for processing the TicketRequest transaction
            //this method will do the following
            //  a) validate there is sufficient seats left for the show
            //  b) create a record for each requested ticket
            //  c) this must be done as a transaction


        }
        #endregion

        #region DO NOT MODIFY
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> ListTicketCounts()
        {
            var result =  new List<KeyValueOption<int>>();
            for (int count = 0; count < 10; count++)
                result.Add(new KeyValueOption<int> { Key = count, DisplayText = count.ToString() });
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<RecentTickets> Tickets_NewlyAddedTickets()
        {
            using (var context = new OMSTContext())
            {
                return context.Tickets
                    .Where(x => x.TicketID > (context.Tickets.Max(y => y.TicketID) - 25))
                    .OrderByDescending(x => x.TicketID)
                    .Select(x => new RecentTickets 
                    {
                        TicketID = x.TicketID,
                        ShowTimeID = x.ShowTimeID,
                        TicketCategoryID = x.TicketCategoryID,
                        TicketPrice = x.TicketPrice,
                        TicketPremium = x.TicketPremium
                    })
                    .ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Category> TicketCategory_List()
        {
            using (var context = new OMSTContext())
            {
                return context.TicketCategories.Select(x => new Category { Description = x.Description, TicketPrice = x.TicketPrice }).ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<KeyValueOption<int>> Locations_List()
        {
            using (var context = new OMSTContext())
            {
                return context.Locations.Select(x => new KeyValueOption<int> { Key = x.LocationID, DisplayText = x.Description }).ToList();
            }
        }

        public List<KeyValueOption<int>> ShowTimes_MoviesByLocations(int locationid)
        {
            DateTime fakeDate = DateTime.Parse("2017-12-28");
            using (var context = new OMSTContext())
            {
                var results = (from x in context.ShowTimes
                               where x.StartDate.Year == 2017
                                  && x.StartDate.Month == 12
                                  && x.StartDate.Day == 28
                                  && x.Theatre.LocationID == locationid
                               select new KeyValueOption<int>
                               {
                                   Key = x.MovieID,
                                   DisplayText = x.Movie.Title
                               }).Distinct().OrderBy(x => x.DisplayText);
                return results.ToList();
            }
        }
        public List<MovieShowTimes> ShowTimes_ShowTimesByMoviesByLocations(int locationid, int movieid)
        {
            DateTime fakeDate = DateTime.Parse("2017-12-28");
            using (var context = new OMSTContext())
            {
                var results = (from x in context.ShowTimes
                               where x.StartDate.Year == 2017
                                  && x.StartDate.Month == 12
                                  && x.StartDate.Day == 28
                                  && x.Theatre.LocationID == locationid
                                  && x.MovieID == movieid
                               select new MovieShowTimes
                               {
                                   TheatreNumber = x.Theatre.TheatreNumber,
                                   ShowTimeID = x.ShowTimeID,
                                   Times = x.StartDate
                               }).Distinct().OrderBy(x => x.Times);
                return results.ToList();
            }
        }

        public decimal Movies_GetTicketPrices(int movieid)
        {
            using (var context = new OMSTContext())
            {
                var premiuminfo = (from x in context.Movies
                                   where x.MovieID == movieid
                                   select x.ScreenType).FirstOrDefault();
                decimal premiumticket = 0.00m;
                if (premiuminfo == null)
                {
                    throw new Exception("Movie screentype info missing");
                }
                if (premiuminfo.Premium)
                {
                    if (premiuminfo.ScreenTypeID == 2)
                    {
                        premiumticket = 3.00m;
                    }
                    else
                    {
                        premiumticket = 5.00m;
                    }
                }
                return premiumticket;
            }
        }

        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public Location Locations_Get(int locationid)
        //{
        //    using (var context = new OMSTContext())
        //    {
        //        return context.Locations.Find(locationid);
        //    }
        //}

        #endregion
    }
}
