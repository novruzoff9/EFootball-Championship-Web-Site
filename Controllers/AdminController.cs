using FRITL_League_2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace FRITL_League_2023.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTeam(Team item)
        {
            c.Teams.Add(item);
            c.SaveChanges();
            return RedirectToAction("Main");
        }

        [HttpGet]
        public ActionResult AddPlayer()
        {
            List<SelectListItem> teams = (from x in c.Teams.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.TeamName,
                                              Value = x.TeamID.ToString()
                                          }).ToList();
            ViewBag.teams = teams;
            return View();
        }

        [HttpPost]
        public ActionResult AddPlayer(Player item)
        {
            c.Players.Add(item);
            c.SaveChanges();
            return RedirectToAction("Main");
        }

        [HttpGet]
        public ActionResult AddGame()
        {
            List<SelectListItem> teams = (from x in c.Teams.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.TeamName,
                                              Value = x.TeamID.ToString()
                                          }).ToList();
            ViewBag.teams = teams;
            return View();
        }

        [HttpPost]
        public ActionResult AddGame(Game p)
        {
            c.Games.Add(p);
            var team1 = c.Teams.Find(p.Team1ID);
            var team2 = c.Teams.Find(p.Team2ID);
            if (p.Tour.IsInt())
            {
                team1.Game = team1.Game + 1;
                var team1score = team1.GoalFor;
                team1.GoalFor = team1score + p.Team1Score;
                team1.GoalAgainst = team1.GoalAgainst + p.Team2Score;
                team1.RedCard = team1.RedCard + p.Team1RedCard;
                team2.Game = team2.Game + 1;
                var team2score = team2.GoalFor;
                team2.GoalFor = team2score + p.Team2Score;
                team2.GoalAgainst = team2.GoalAgainst + p.Team1Score;
                team2.RedCard = team2.RedCard + p.Team2RedCard;
                if (p.Team1Score > p.Team2Score)
                {
                    team1.Win = team1.Win + 1;
                    team1.Point = team1.Point + 3;
                    team2.Lose = team2.Lose + 1;
                }
                else if (p.Team1Score == p.Team2Score)
                {
                    team1.Draw = team1.Draw + 1;
                    team1.Point = team1.Point + 1;
                    team2.Draw = team2.Draw + 1;
                    team2.Point = team2.Point + 1;
                }
                else if (p.Team1Score < p.Team2Score)
                {
                    team1.Lose = team1.Lose + 1;
                    team2.Win = team2.Win + 1;
                    team2.Point = team2.Point + 3;
                }
            }
            else
            {
                team1.Game = team1.Game + 1;
                var team1score = team1.GoalFor;
                team1.GoalFor = team1score + p.Team1Score;
                team1.GoalAgainst = team1.GoalAgainst + p.Team2Score;
                team1.RedCard = team1.RedCard + p.Team1RedCard;
                team2.Game = team2.Game + 1;
                var team2score = team2.GoalFor;
                team2.GoalFor = team2score + p.Team2Score;
                team2.GoalAgainst = team2.GoalAgainst + p.Team1Score;
                team2.RedCard = team2.RedCard + p.Team2RedCard;
                if (p.Team1Score > p.Team2Score)
                {
                    team1.Win = team1.Win + 1;
                    team2.Lose = team2.Lose + 1;
                }
                else if (p.Team1Score == p.Team2Score)
                {
                    team1.Draw = team1.Draw + 1;
                    team2.Draw = team2.Draw + 1;
                }
                else if (p.Team1Score < p.Team2Score)
                {
                    team1.Lose = team1.Lose + 1;
                    team2.Win = team2.Win + 1;
                }
            }
            c.SaveChanges();
            return RedirectToAction("Main");
        }

        public ActionResult Games()
        {
            var games = c.Games.OrderBy(x => x.Tour).ToList();
            return View(games);
        }

        public ActionResult GamesList()
        {
            var games = c.Games.OrderBy(x => x.Tour).ToList();
            return View(games);
        }

        [HttpGet]
        public ActionResult AddEvent(int id)
        {
            var game = c.Games.FirstOrDefault(x => x.GameID == id);
            var team1 = game.Team1ID;
            var team2 = game.Team2ID;
            var allplayers = c.Players.ToList();
            List<Player> usefulplayers = new List<Player>();
            foreach (var item in allplayers)
            {
                if(item.TeamID == team1 || item.TeamID == team2)
                {
                    usefulplayers.Add(item);
                }
            }
            List<SelectListItem> players = (from x in usefulplayers
                                            select new SelectListItem
                                          {
                                              Text = x.PlayerName,
                                              Value = x.PlayerID.ToString()
                                          }).ToList();

            ViewBag.players = players;
            ViewBag.gameid = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddEvent(Event item)
        {
            c.Events.Add(item);
            var player = c.Players.FirstOrDefault(x => x.PlayerID == item.PlayerID);
            player.Goal = player.Goal + item.Goal;
            player.Asist = player.Asist + item.Asist;
            player.RedCard = player.RedCard + item.RedCard;
            player.Save = player.Save + item.Save;
            c.SaveChanges();
            return RedirectToAction("Games");
        }

        [HttpGet]
        public ActionResult UpdateGame(int id)
        {
            List<SelectListItem> teams = (from x in c.Teams.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.TeamName,
                                              Value = x.TeamID.ToString()
                                          }).ToList();
            ViewBag.teams = teams;
            var game = c.Games.FirstOrDefault(x => x.GameID == id);
            return View(game);
        }

        [HttpPost]
        public ActionResult UpdateGame(Game game)
        {
            var newgame = c.Games.FirstOrDefault(x => x.GameID == game.GameID);
            newgame.Tour = game.Tour;
            newgame.Team1Shot = game.Team1Shot;
            newgame.Team2Shot = game.Team2Shot;
            newgame.Team1ShotonTarget = game.Team1ShotonTarget;
            newgame.Team2ShotonTarget = game.Team2ShotonTarget;
            newgame.Team1Asist = game.Team1Asist;
            newgame.Team2Asist = game.Team2Asist;
            newgame.Team1Foul = game.Team1Foul;
            newgame.Team2Foul = game.Team2Foul;
            newgame.Team1RedCard = game.Team1RedCard;
            newgame.Team2RedCard = game.Team2RedCard;
            newgame.Team1Stealing = game.Team1Stealing;
            newgame.Team2Stealing = game.Team2Stealing;
            newgame.Team1Pass = game.Team1Pass;
            newgame.Team2Pass = game.Team2Pass;
            newgame.Team1Corner = game.Team1Corner;
            newgame.Team2Corner = game.Team2Corner;
            newgame.Team1Possession = game.Team1Possession;
            newgame.Team2Possession = game.Team2Possession;
            c.SaveChanges();
            return RedirectToAction("GamesList");
        }
    }
}