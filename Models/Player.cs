using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FRITL_League_2023.Models
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosision { get; set; }
        public int Goal { get; set; }
        public int Asist { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int Save { get; set; }

        public int TeamID { get; set; }
        public virtual Team Team { get; set; }

        //public ICollection<Goals> Goals { get; set; }
    }
}