using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL
{
    public class TourneyDAL : ITourneyDAL
    {
        private readonly string connectionString;

        public TourneyDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Returns list of all active tournaments
        /// </summary>
        /// <returns></returns>
        public List<Tournament> BrowseTourneys()
        {
            List<Tournament> Tourneys = new List<Tournament>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tournament WHERE active = 1", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tournament temp = new Tournament();
                        temp.ID = Convert.ToInt32(reader["id"]);
                        temp.TournamentActive = Convert.ToBoolean(reader["active"]);
                        temp.TournamentCreateDate = Convert.ToDateTime(reader["create_date"]);
                        temp.TournamentStartDate = Convert.ToDateTime(reader["start_date"]);
                        temp.TournamentName = Convert.ToString(reader["name"]);
                        temp.PLayerCount = Convert.ToInt32(reader["playertotal"]);
                        temp.PlayersString = reader["playerString"].ToString();
                        temp.ScoresString = reader["scoresString"].ToString();
                        Tourneys.Add(temp);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Tourneys;
        }
        /// <summary>
        /// Returns list of active tourneys narrowed by search ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Tournament> SearchTourneys(int id)
        {
            List<Tournament> tournaments = new List<Tournament>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from tournament where id = @id;", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tournament temp = new Tournament();
                        temp.ID = Convert.ToInt32(reader["id"]);
                        temp.TournamentActive = Convert.ToBoolean(reader["active"]);
                        temp.TournamentCreateDate = Convert.ToDateTime(reader["create_date"]);
                        temp.TournamentStartDate = Convert.ToDateTime(reader["start_date"]);
                        temp.TournamentName = Convert.ToString(reader["name"]);
                        temp.PLayerCount = Convert.ToInt32(reader["playertotal"]);
                        temp.PlayersString = reader["playerString"].ToString();
                        temp.ScoresString = reader["scoresString"].ToString();
                        tournaments.Add(temp);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return tournaments;
        }
        /// <summary>
        /// Returns List of active tournaments narrowed by search string
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<Tournament> SearchTourneys(string search)
        {
            List<Tournament> tournaments = new List<Tournament>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from tournament where name like @name;", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@name", "%" + search + "%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tournament temp = new Tournament();
                        temp.ID = Convert.ToInt32(reader["id"]);
                        temp.TournamentActive = Convert.ToBoolean(reader["active"]);
                        temp.TournamentCreateDate = Convert.ToDateTime(reader["create_date"]);
                        temp.TournamentStartDate = Convert.ToDateTime(reader["start_date"]);
                        temp.TournamentName = Convert.ToString(reader["name"]);
                        temp.PLayerCount = Convert.ToInt32(reader["playertotal"]);
                        temp.PlayersString = reader["playerString"].ToString();
                        temp.ScoresString = reader["scoresString"].ToString();
                        tournaments.Add(temp);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return tournaments;
        }

        /// <summary>
        /// Browse all tournaments.
        /// </summary>
        /// <returns></returns>
        public List<Tournament> BrowseOldTourneys()
        {
            return BrowseOldTourneys();
        }
        /// <summary>
        /// Browse all tournaments.
        /// </summary>
        /// <param name="search">The search string.</param>
        /// <returns></returns>
        public List<Tournament> BrowseOldTourneys(string search)
        {
            string browseOld = "select * from tournament where active = 0";
            string searchOld = "select * from tournament where name = @name";
            string input = "";
            if (search == "")
                input = browseOld;
            else
                input = searchOld;
            List<Tournament> tournaments = new List<Tournament>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(input, conn);
                    if (search != "")
                        cmd.Parameters.AddWithValue("@name", search);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tournament temp = new Tournament();
                        temp.TournamentActive = Convert.ToBoolean(reader["active"]);
                        temp.TournamentCreateDate = Convert.ToDateTime(reader["create_date"]);
                        temp.TournamentStartDate = Convert.ToDateTime(reader["start_date"]);
                        temp.TournamentName = Convert.ToString(reader["name"]);
                        tournaments.Add(temp);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return tournaments;
        }

        public int CreateTourney(Tournament tourney)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO tournament VALUES(@name, @isActive, @startDate, @createDate, @totalPlayers, @playerString, @scoresString);", conn);
                    cmd.Parameters.AddWithValue("@name", tourney.TournamentName);
                    cmd.Parameters.AddWithValue("@isActive", true);
                    cmd.Parameters.AddWithValue("@startDate", tourney.TournamentStartDate);
                    cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@totalPlayers", tourney.PLayerCount);
                    cmd.Parameters.AddWithValue("@playerString", tourney.PlayersString);
                    cmd.Parameters.AddWithValue("@scoresString", tourney.ScoresString);

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }


        //todo: distant option, reset bracket to beginning with either no players or the same players
    }
}