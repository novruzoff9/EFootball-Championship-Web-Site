using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FRITL_League_2023.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public int GameID { get; set; }
        public int PlayerID { get; set; }
        public int Goal { get; set; }
        public int Asist { get; set; }
        public int RedCard { get; set; }
        public int Save { get; set; }
    }
}