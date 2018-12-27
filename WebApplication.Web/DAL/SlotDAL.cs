using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;
using System.Data.SqlClient;
using Utilities;

namespace WebApplication.Web.DAL
{
    public class SlotDAL : ISlotDAL
    {
        private readonly string CONN_STRING = "";
        private const string SQL_GET_ONE_ID = "SELECT * FROM slot WHERE id = @slotID;";
        private const string SQL_GET_ALL = "SELECT * FROM slot;";
        private const string SQL_GET_ALL_TOURNAMENT_ID = "SELECT * FROM slot WHERE tournament_id = @tournamentID;";
        private const string SQL_ADD_ONE = "INSERT INTO slot VALUES(@tournamentID, @playerID, @nextSlotID, @Xpos, @Ypos, @score);";
        private const string SQL_UPDATE_SCORE = "UPDATE slot SET score = @score WHERE id = @slotID;";

        public SlotDAL(string connectionString)
        {
            CONN_STRING = connectionString;
        }

        /// <summary>
        /// Gets one slot by the given ID. If none was found then the returned slot will have an ID of -1. Other ID values that were null will also be -1.
        /// </summary>
        /// <param name="id">ID of desired slot.</param>
        /// <returns>One slot by given ID .</returns>
        public Slot GetSlot(int id)
        {
            Slot output = new Slot();
            if (id < 1) return output;

            Dictionary<string, Object> parameters = new Dictionary<string, object>()
            {
                {"@slotID", id },
            };
            List<Slot> slots = SQLUtilities.PerformSQL(CONN_STRING, SQL_GET_ONE_ID, parameters, PopulateList);
            if (slots.Count > 0) output = slots[0];
            else return output;

            return output;
        }

        /// <summary>
        /// Gets all slots from the database.
        /// </summary>
        /// <returns>Lidt of all slots.</returns>
        public List<Slot> GetSlots()
        {
            return AssignInternalObjs(SQLUtilities.PerformSQL(CONN_STRING, SQL_GET_ALL, PopulateList));
        }
        /// <summary>
        /// Gets all the slots by the given tournament ID. Returns an empty list if none found or errored.
        /// </summary>
        /// <param name="tourneyID">The ID of the desired tournament.</param>
        /// <returns>A list of slots from the desired tournament.</returns>
        public List<Slot> GetSlots(int tourneyID)
        {
            if (tourneyID < 0) return new List<Slot>();

            Dictionary<string, Object> parameters = new Dictionary<string, object>()
            {
                { "@tournamentID", tourneyID },
            };
            return SQLUtilities.PerformSQL(CONN_STRING, SQL_GET_ALL_TOURNAMENT_ID, parameters, PopulateList);
        }

        /// <summary>
        /// Creates a slot in the database going by the given Slot object.
        /// </summary>
        /// <param name="slot">THe slot to add to the database.</param>
        /// <returns>Number of rows affected.</returns>
        public int CreateSlot(Slot slot)
        {
            if (slot == null || slot.TournamentID < 1) return 0;

            int rowsAffected = -1;
            Dictionary<string, Object> parameters = new Dictionary<string, object>()
            {
                {"@tournamentID", slot.TournamentID},
                {"@playerID", null },
                {"@nextSlotID", null },
                {"@Xpos", slot.Xpos },
                {"@Ypos", slot.Ypos },
                {"@score", slot.Score },
            };
            if (slot.Player != null) parameters["@playerID"] = slot.Player.Id;
            if (slot.NextSlot != null) parameters["@nextSlotID"] = slot.NextSlot.ID;

            SQLUtilities.PerformSQL<Slot>(CONN_STRING, SQL_ADD_ONE, parameters, out rowsAffected);
            return rowsAffected;
        }
        public int UpdateScore(int slotID, int score)
        {
            if (slotID < 1) return 0;

            int rowsAffected = -1;
            Dictionary<string, Object> parameters = new Dictionary<string, object>()
            {
                {"@slotID", slotID},
                {"@score", score},
            };
            SQLUtilities.PerformSQL<Slot>(CONN_STRING, SQL_UPDATE_SCORE, parameters, out rowsAffected);
            return rowsAffected;
        }

        /// <summary>
        /// Populates a list of models via the SqlDataReader provided.
        /// </summary>
        /// <param name="reader">The SqlDataReader to get the list from.</param>
        /// <returns>List of models.</returns>
        private Slot PopulateList(SqlDataReader reader)
        {
            Slot model = new Slot();

            model.ID = Convert.ToInt32(reader["id"]);
            model.TournamentID = Convert.ToInt32(reader["tournament_id"]);
            if (reader["player_id"].GetType() != typeof(DBNull)) model.PlayerID = Convert.ToInt32(reader["player_id"]);
            if (reader["nextslot_id"].GetType() != typeof(DBNull)) model.NextSlotID = Convert.ToInt32(reader["nextslot_id"]);

            return model;
        }

        /// <summary>
        /// Assigns the objects in the slot object from the database.
        /// </summary>
        /// <param name="slot">The slot to assign the objects in.</param>
        /// <returns>The slot with the objects assigned.</returns>
        public Slot AssignInternalObjs(Slot slot)
        {
            List<Slot> output = new List<Slot>()
            {
                slot,
            };

            return AssignInternalObjs(output)[0];
        }
        /// <summary>
        /// Assigns the objects in each of the slot objects from the database.
        /// </summary>
        /// <param name="slots">The list of slots to assign the objects in.</param>
        /// <returns>The list of slots with the objects assigned.</returns>
        public List<Slot> AssignInternalObjs(List<Slot> slots)
        {
            IUserDAL userDAL = new UserSqlDAL(CONN_STRING);
            foreach (Slot slot in slots)
            {
                slot.Player = userDAL.GetUser(slot.PlayerID);
                slot.NextSlot = GetSlot(slot.NextSlotID);
            }

            return slots;
        }
    }
}
