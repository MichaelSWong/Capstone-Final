using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Web.Models
{
    public class Slot
    {
        public int ID { get; set; } = -1;
        public int TournamentID { get; set; } = -1;
        public int PlayerID { get; set; } = -1;
        public int NextSlotID { get; set; } = -1;
        public double Xpos { get; set; }
        public double Ypos { get; set; }
        public int Score { get; set; }
        private User player = null;
        public User Player
        {
            get
            {
                return player;
            }
            set
            {
                if (value != null)
                {
                    PlayerID = value.Id;
                    player = value;
                }
            }
        }
        private Slot nextSlot = null;
        public Slot NextSlot
        {
            get
            {
                return nextSlot;
            }
            set
            {
                if(value != null)
                {
                    NextSlotID = value.ID;
                    nextSlot = value;
                }
            }
        }
    }
}
