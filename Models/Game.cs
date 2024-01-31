using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FRITL_League_2023.Models
{
    public class Game
    {
        [Key]
        public int GameID { get; set; }
        public string Tour { get; set; }

        public int Team1ID { get; set; }
        public virtual Team Team1 { get; set; }
        public int Team1Score { get; set; }
        public int Team1Asist { get; set; }
        public int Team1RedCard { get; set; }
        public int Team1Possession { get; set; }
        public int Team1Foul { get; set; }
        public int Team1Shot { get; set; }
        public int Team1ShotonTarget { get; set; }
        public int Team1Corner { get; set; }
        public int Team1Pass { get; set; }
        public int Team1Stealing { get; set; }

        public int Team2ID { get; set; }
        public virtual Team Team2 { get; set; }
        public int Team2Score { get; set; }
        public int Team2Asist { get; set; }
        public int Team2RedCard { get; set; }
        public int Team2Possession { get; set; }
        public int Team2Foul { get; set; }
        public int Team2Shot { get; set; }
        public int Team2ShotonTarget { get; set; }
        public int Team2Corner { get; set; }
        public int Team2Pass { get; set; }
        public int Team2Stealing { get; set; }

        //public ICollection<Goals> Goals { get; set; }
    }
}