using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.Models;
using WebApplication.Web.DAL;
using WebApplication.Web.Utilities;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Web.Controllers
{
    public class TourneyController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private ISlotDAL slotdal;
        private ITourneyDAL tourneyDAL;
        private IUserDAL userDAL;
        string addedPlayersKey = "addedPlayers";

        /// <summary>
        /// Sets up Dals for controller
        /// </summary>
        /// <param name="tDal"></param>
        /// <param name="sdal"></param>
        /// <param name="uDAl"></param>
        public TourneyController(IHttpContextAccessor cA, ITourneyDAL tDal, ISlotDAL sdal, IUserDAL uDAl)
        {
            contextAccessor = cA;
            slotdal = sdal;
            tourneyDAL = tDal;
            userDAL = uDAl;
        }

        ISession Session => contextAccessor.HttpContext.Session;

        public IActionResult Index()
        {
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, "");
            return View("Browse", new GenericSearch<Tournament>());
        }
        /// <summary>
        /// displays list of active tourneys
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult Browse(GenericSearch<Tournament> search)
        {
            search.Object = (search.SearchStr == null || search.SearchStr == "") ? tourneyDAL.BrowseTourneys() : tourneyDAL.SearchTourneys(search.SearchStr);
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, "");
            return View(search);
        }

        public IActionResult CreateSlotsOLD()
        {
            Tournament tourney = new Tournament()
            {
                Players = GetAddedUsers(),
            };
            tourney.PLayerCount = tourney.Players.Count;
            Session.SetString(addedPlayersKey, "");
            if (tourney.Players.Count > 0) return View(tourney);
            else return RedirectToAction("SearchUser");
        }
        public IActionResult CreateSlots()
        {
            Tournament tourney = new Tournament()
            {
                Players = GetAddedUsers(),
            };
            foreach(User user in tourney.Players)
            {
                tourney.PlayersString += (tourney.PlayersString == "") ? user.Username : " " + user.Username;
            }

            tourney.IsAdmin = true;
            return View("SlotsView_UsingCodeTube", tourney);
        }
        public IActionResult SlotsView(int id)
        {
            Tournament tourney = tourneyDAL.SearchTourneys(id)[0];
            tourney.Slots = slotdal.GetSlots(tourney.ID);
            tourney.Slots = slotdal.AssignInternalObjs(tourney.Slots);
            TournamentUtilities.ConstructAndAssignTree(tourney);
            if(tourney.Slots != null && tourney.Slots.Count > 0) tourney.PlayersString = GetPlayersString(tourney);
            if (tourney.Slots != null && tourney.Slots.Count > 0) tourney.ScoresString = GetScoresString(tourney);
            tourney.IsAdmin = false;
            return View("SlotsView_UsingCodeTube", tourney);
        }
        public IActionResult CreateLocalTourney()
        {
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, "");
            return View();
        }
        public IActionResult CreateTournament()
        {
            Tournament tourney = new Tournament()
            {
                ID = 2,
                PLayerCount = 4,

            };
            tourney.Slots = slotdal.GetSlots(tourney.ID);
            tourney.Slots = slotdal.AssignInternalObjs(tourney.Slots);
            TournamentUtilities.ConstructAndAssignTree(tourney);
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, "");
            return View();
        }
        public IActionResult SaveTournament(Tournament tourney)
        {
            tourney.ScoresString = tourney.ScoresString.Replace(',', ' ');
            tourney.TournamentCreateDate = DateTime.Now;
            tourney.TournamentStartDate = DateTime.Now;

            int playerCount = 0;
            string[] players = tourney.PlayersString.Split(' ');
            foreach (string player in players)
            {
                if (player != "null") playerCount++;
            }
            tourney.PLayerCount = playerCount;

            tourneyDAL.CreateTourney(tourney);
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, "");
            return RedirectToAction("Index", "Tourney");
        }
        public IActionResult ReorderPlayers(int id, int goingUp)
        {
            if(id <= 0) return RedirectToAction("CreateSlots", "Tourney");

            List<User> addedPlayers = GetAddedUsers();
            if (goingUp == 0)
            {
                for (int i = 0; i < addedPlayers.Count; i++)
                {
                    if (id == addedPlayers[i].Id && i + 1 < addedPlayers.Count)
                    {
                        User chosenUser = addedPlayers[i];
                        addedPlayers[i] = addedPlayers[i + 1];
                        addedPlayers[i + 1] = chosenUser;
                        break;
                    }
                }

                ReplaceAddedUsers(addedPlayers);
            }
            else if(goingUp == 1)
            {
                for (int i = addedPlayers.Count - 1; i >= 0; i--)
                {
                    if (id == addedPlayers[i].Id && i - 1 >= 0)
                    {
                        User chosenUser = addedPlayers[i];
                        addedPlayers[i] = addedPlayers[i - 1];
                        addedPlayers[i - 1] = chosenUser;
                        break;
                    }
                }

                ReplaceAddedUsers(addedPlayers);
            }

            return RedirectToAction("CreateSlots", "Tourney");
        }

        public IActionResult SearchUser(string searchStr)
        {
            SearchAddUsers userSearch = new SearchAddUsers();
            if (searchStr == null || searchStr == "")
            {
                userSearch.userSearch.Object = userDAL.SearchUsers("");
            }
            else
            {
                userSearch.userSearch.SearchStr = searchStr;
                userSearch.userSearch.Object = userDAL.SearchUsers(searchStr);
            }

            userSearch.players = GetAddedUsers();
            return View(userSearch);
        }
        public IActionResult RemoveAddedPlayer(int id)
        {
            string addedUserIDs = (!String.IsNullOrEmpty(Session.GetString(addedPlayersKey))) ? Session.GetString(addedPlayersKey) : "";
            if (addedUserIDs.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().Contains(id.ToString()))
            {
                addedUserIDs = addedUserIDs.Replace(id.ToString(), "");
            }
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, addedUserIDs);
            return RedirectToAction("SearchUser");
        }
        public IActionResult AddUser(int id)
        {
            User addedUser = userDAL.GetUser(id);
            string addedUserIDs = (!String.IsNullOrEmpty(Session.GetString(addedPlayersKey))) ? Session.GetString(addedPlayersKey) : "";
            if (addedUser != null && !addedUserIDs.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().Contains(addedUser.Id.ToString()))
            {
                addedUserIDs += (addedUserIDs != null && addedUserIDs.Length > 0) ? " " + addedUser.Id : addedUser.Id.ToString();
            }
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, addedUserIDs);
            return RedirectToAction("SearchUser");
        }

        private List<User> GetAddedUsers()
        {
            string addedUserIDs = (!String.IsNullOrEmpty(Session.GetString(addedPlayersKey))) ? Session.GetString(addedPlayersKey) : "";
            List<User> users = new List<User>();
            if (addedUserIDs != null && addedUserIDs.Length > 0)
            {
                string[] addedUserIDsArray = addedUserIDs.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string id in addedUserIDsArray)
                {
                    try
                    {
                        users.Add(userDAL.GetUser(int.Parse(id)));
                    }
                    catch (InvalidCastException)
                    {
                        throw;
                    }
                }
            }

            return users;
        }
        private void ReplaceAddedUsers(List<User> newUserList)
        {
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, "");
            string addedUserIDs = "";
            for (int i = 0; i < newUserList.Count; i++)
            {
                addedUserIDs += (i == 0) ? newUserList[i].Id.ToString() : " " + newUserList[i].Id.ToString();
            }
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, addedUserIDs);
        }

        public IActionResult Rankings(Tournament tourney)
        {
            contextAccessor.HttpContext.Session.SetString(addedPlayersKey, "");
            return View(tourney.Players);
        }

        private string GetPlayersString(Tournament tourney)
        {
            string output = "";

            for(int i = 0; i < tourney.PLayerCount; i++)
            {
                Slot slot = tourney.Slots[i];
                output += (output == "") ? slot.Player.Username : " " + slot.Player.Username;
            }

            return output;
        }
        private string GetScoresString(Tournament tourney)
        {
            string output = "";

            for(int i = 0; i < tourney.Slots.Count - 1; i++)
            {
                Slot slot = tourney.Slots[i];
                output += (output == "") ? slot.Score.ToString() : " " + slot.Score.ToString();
            }

            return output;
        }
        public ActionResult LocalNumPlayer(int players)
        {

            return View(players);
        }
    }
}