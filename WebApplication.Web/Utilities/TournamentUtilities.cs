using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.Utilities
{
    public static class TournamentUtilities
    {
        /// <summary>
        /// Constructs a tournament tree and assigns it to the Tree value in the given tournament.
        /// </summary>
        /// <param name="tourney">The tournament to be constructed.</param>
        public static void ConstructAndAssignTree(Tournament tourney)
        {
            tourney.Tree = ConstructTree(tourney);
        }
        /// <summary>
        /// Constructs a tournament tree and returns it.
        /// </summary>
        /// <param name="tourney">The tournament to be constructed.</param>
        /// <returns>The constructed tree.</returns>
        public static List<List<Slot>> ConstructTree(Tournament tourney)
        {
            if(tourney == null || tourney.Slots == null || tourney.Slots.Count < 1 || tourney.PLayerCount < 1)
            {
                return new List<List<Slot>>();
            }

            List<List<Slot>> output = new List<List<Slot>>();
            output.Add(new List<Slot>());

            List<int> addedSlots = new List<int>();
            for(int i = 0; i < tourney.PLayerCount; i++)
            {
                Slot currSlot = tourney.Slots[i];
                output[0].Add(currSlot);
                addedSlots.Add(currSlot.ID);

                int currLevel = 1;
                Slot currNextSlot = currSlot.NextSlot;
                bool isDone = currNextSlot == null && currNextSlot.ID < 0;
                while(!isDone)
                {
                    if (!addedSlots.Contains(currNextSlot.ID))
                    {
                        if (output.Count <= currLevel || output[currLevel] == null || output[currLevel].Count < 1)
                        {
                            output.Add(new List<Slot>());
                        }
                        
                        output[currLevel].Add(currNextSlot);
                        addedSlots.Add(currNextSlot.ID);

                        if (currNextSlot.NextSlot != null && currNextSlot.NextSlot.ID > -1 && !addedSlots.Contains(currNextSlot.NextSlot.ID))
                        {
                            currLevel++;
                            currNextSlot = currNextSlot.NextSlot;
                        }
                        else isDone = true;
                    }
                    else isDone = true;
                }
            }

            return output;
        }
    }
}
