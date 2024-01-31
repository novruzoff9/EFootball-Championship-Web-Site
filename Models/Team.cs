using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FRITL_League_2023.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamShortName { get; set; }
        public string Controller { get; set; }
        public string TeamLogo { get; set; }
        public string Group { get; set; }
        public int Game { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public int RedCard { get; set; }
        public int GoalFor { get; set; }
        public int GoalAgainst { get; set; }
        public int Point { get; set; }
        public int Stage { get; set; }


        public ICollection<Player> Players { get; set; }
    }
}