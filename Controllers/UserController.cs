using FRITL_League_2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace FRITL_League_2023.Controllers
{
    public class UserController : Controller
    {
        Context c = new Context();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Groups(string group)
        {
            var teams = c.Teams.ToList();
            var team = new Team();
            var groupteams = new List<Team>();
            teams = teams.Where(x => x.Group == group).ToList();
            foreach (var item in teams)
            {
                var games = c.Games.Where(x => x.Team2ID == item.TeamID ||
                x.Team1ID == item.TeamID).ToList();
                team = item;
                team.Game = 6;
                team.GoalAgainst = 0;
                team.GoalFor = 0;
                team.Win = 0;
                team.Draw = 0;
                team.Lose = 0;
                games = games.GetRange(0, 6);
                foreach (var game in games)
                {
                    if(game.Team1ID == item.TeamID)
                    {
                        team.GoalFor += game.Team1Score; 
                        team.GoalAgainst += game.Team2Score;
                        if(game.Team1Score > game.Team2Score)
                        {
                            team.Win += 1;
                        }
                        if (game.Team1Score == game.Team2Score)
                        {
                            team.Draw += 1;
                        }
                        if (game.Team1Score < game.Team2Score)
                        {
                            team.Lose += 1;
                        }
                    }
                    else
                    {
                        team.GoalFor += game.Team2Score;
                        team.GoalAgainst += game.Team1Score;
                        if (game.Team1Score < game.Team2Score)
                        {
                            team.Win += 1;
                        }
                        if (game.Team1Score == game.Team2Score)
                        {
                            team.Draw += 1;
                        }
                        if (game.Team1Score > game.Team2Score)
                        {
                            team.Lose += 1;
                        }
                    }
                }
                groupteams.Add(team);
            }
            groupteams = groupteams.OrderBy(x => x.GoalFor).ToList();
            groupteams = groupteams.OrderBy(x => x.Point).ToList();
            groupteams = Enumerable.Reverse(groupteams).ToList();
            ViewBag.group = group;
            return PartialView(groupteams);
        }

        public PartialViewResult BestGames()
        {
            var games = c.Games.ToList();
            List<Game> allgames = new List<Game>();
            List<Game> bestgames = new List<Game>();

            foreach (var game in games)
            {
                int goal1 = game.Team1Score;
                int goal2 = game.Team2Score;
                var newgame = new Game();
                newgame.GameID = game.GameID;
                newgame.Team1Score = goal1 + goal2;
                allgames.Add(newgame);
            }

            allgames = allgames.OrderBy(x => x.Team1Score).ToList();
            allgames = Enumerable.Reverse(allgames).ToList();

            bestgames.Add(games.FirstOrDefault(x => x.GameID == allgames[0].GameID));
            bestgames.Add(games.FirstOrDefault(x => x.GameID == allgames[1].GameID));
            bestgames.Add(games.FirstOrDefault(x => x.GameID == allgames[2].GameID));

            return PartialView(bestgames);
            
        }

        public PartialViewResult BestScorers()
        {
            var players = c.Players.OrderBy(x=>x.Goal).ToList();
            players = Enumerable.Reverse(players).ToList();
            players = players.GetRange(0, 10);
            return PartialView(players);
        }
        public PartialViewResult BestAsisters()
        {
            var players = c.Players.OrderBy(x => x.Asist).ToList();
            players = Enumerable.Reverse(players).ToList();
            players = players.GetRange(0, 10);
            return PartialView(players);
        }
        public PartialViewResult MostRedCards()
        {
            var players = c.Players.OrderBy(x => x.RedCard).ToList();
            players = Enumerable.Reverse(players).ToList();
            players = players.GetRange(0, 10);
            return PartialView(players);
        }
        public PartialViewResult MostSaves()
        {
            var players = c.Players.OrderBy(x => x.Save).ToList();
            players = Enumerable.Reverse(players).ToList();
            players = players.GetRange(0, 10);
            return PartialView(players);
        }

        public PartialViewResult BestPlayers()
        {
            var scorer = c.Players.OrderBy(x => x.Goal).ToList().Last();
            var asister = c.Players.OrderBy(x => x.Asist).ToList().Last();
            var save = c.Players.OrderBy(x => x.Save).ToList().Last();
            var redcard = c.Players.OrderBy(x => x.RedCard).ToList().Last();
            var players = new List<Player>();
            players.Add(scorer);
            players.Add(asister);
            players.Add(save);
            players.Add(redcard);
            return PartialView(players);
        }
        
        public PartialViewResult GameGoals(int id)
        {
            var events = c.Events.Where(x => x.GameID == id && x.Goal > 0).ToList();
            ViewBag.gameid = id;
            return PartialView(events);
        }
        public PartialViewResult GameAsists(int id)
        {
            var events = c.Events.Where(x => x.GameID == id && x.Asist > 0).ToList();
            ViewBag.gameid = id;
            return PartialView(events);
        }
        public PartialViewResult GameRedCards(int id)
        {
            var events = c.Events.Where(x => x.GameID == id && x.RedCard > 0).ToList();
            ViewBag.gameid = id;
            return PartialView(events);
        }

        public PartialViewResult TeamPlayers(int id)
        {
            var players = c.Players.Where(x => x.TeamID == id).ToList();
            players = players.OrderBy(x => x.Asist).ToList();
            players = players.OrderBy(x => x.Goal).ToList();
            players = Enumerable.Reverse(players).ToList();
            return PartialView(players);
        }
        public PartialViewResult TeamGames(int id)
        {
            var games = c.Games.Where(x => x.Team1ID == id || x.Team2ID == id).ToList();
            return PartialView(games);
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Games()
        {
            var games = c.Games.Where(x => x.Tour.Length == 1).ToList();
            games = games.OrderBy(x=>x.Tour).ToList();
            var finals = c.Games.Where(x => x.Tour.Length > 1).ToList();
            games.AddRange(finals);

            return View(games);
        }

        public ActionResult Standings()
        {
            return View();
        }

        public PartialViewResult FinalsGame(string tour, int stage)
        {
            var games = c.Games.Where(x => x.Tour == tour).ToList();
            List<Game> stagegames = new List<Game>();
            foreach (var item in games)
            {
                var team = c.Teams.FirstOrDefault(x => x.TeamID == item.Team1ID);
                if (team.Stage == stage)
                {
                    stagegames.Add(item);
                }
            }
            if(stagegames.Count == 0)
            {
                Game game = new Game();
                var teams = c.Teams.Where(x => x.Stage == stage).ToList();
                teams = teams.OrderBy(x => x.Stage).ToList();
                teams = Enumerable.Reverse(teams).ToList();
                game.Team1ID = teams.First().TeamID;
                game.Team2ID = teams.Last().TeamID;
                stagegames.Add(game);
            }
            return PartialView(stagegames);
        }

        public PartialViewResult SemiFinalist(int stage)
        {
            var games = c.Games.Where(x => x.Tour == "1/4 FINAL").ToList();
            List<Game> stagegames = new List<Game>();
            foreach (var item in games)
            {
                var team = c.Teams.FirstOrDefault(x => x.TeamID == item.Team1ID);
                if (team.Stage == stage)
                {
                    stagegames.Add(item);
                }
            }
            Team semifinalist = new Team();
            if (stagegames.Count > 0)
            {
                int team1id = stagegames.First().Team1ID;
                int team2id = stagegames.First().Team2ID;
                var team1 = c.Teams.FirstOrDefault(x => x.TeamID == team1id);
                var team2 = c.Teams.FirstOrDefault(x => x.TeamID == team2id);
                int team1goal = 0, team2goal = 0;
                team1goal += stagegames.First().Team1Score;
                team2goal += stagegames.First().Team2Score;
                team1goal += stagegames.Last().Team2Score;
                team2goal += stagegames.Last().Team1Score;
                if (team1goal > team2goal)
                {
                    semifinalist = team1;
                }
                else if(team1goal < team2goal)
                {
                    semifinalist = team2;
                }
                else
                {
                    semifinalist.TeamID = -1;
                }
            }
            else
            {
                semifinalist.TeamID = -1;
            }
            return PartialView(semifinalist);
        }


        public ActionResult Finals()
        {
            var teams = c.Teams.Where(x => x.Stage > 0).ToList();
            teams = teams.OrderBy(x => x.Stage).ToList();
            teams = Enumerable.Reverse(teams).ToList();
            return View(teams);
        }

        public ActionResult MatchDetails(int id)
        {
            var game = c.Games.FirstOrDefault(x => x.GameID == id);
            return View(game);
        }

        public ActionResult TeamDetails(int id)
        {
            var team = c.Teams.FirstOrDefault(x => x.TeamID == id);
            return View(team);
        }

        public ActionResult Bests()
        {
            return View();
        }
    }
}