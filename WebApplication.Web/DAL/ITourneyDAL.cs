using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public interface ITourneyDAL
    {
        List<Tournament> BrowseTourneys();
        List<Tournament> SearchTourneys(int id);
        List<Tournament> SearchTourneys(string search);
        List<Tournament> BrowseOldTourneys();
        List<Tournament> BrowseOldTourneys(string search);
        int CreateTourney(Tournament tourney);
    }
}
