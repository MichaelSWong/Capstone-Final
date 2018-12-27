using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class Tournament
    {
        public int ID { get; set; }
        public List<User> Admins { get; set; } 
        public List<User> TO { get; set; }
        public List<User> Players { get; set; }
        public bool TournamentActive { get; set; } = false;
        public DateTime TournamentStartDate { get; set; }
        public DateTime TournamentCreateDate { get; set; }
        public string TournamentName { get; set; }
        public int PLayerCount { get; set;}
        public List<Slot> Slots { get; set; }
        public List<List<Slot>> Tree { get; set; }
        public string PlayersString { get; set; } = "";
        public string ScoresString { get; set; } = "";
        public bool IsAdmin { get; set; }
        //TODO: nice to have placements list.
        //TODO: update database with additional columns startdate/createDate
        


    }
}
