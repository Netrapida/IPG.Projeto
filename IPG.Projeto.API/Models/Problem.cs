using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IPG.Projeto.API.Models
{
    public class Problem
    {    
        public int Id { get; set; }

        //Onde se Encontra
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int State { get; set; }

        //Detalhes do problema
        [Required]
        [StringLength(180)]
        public string Text { get; set; }
        [Required]
        [StringLength(500)]
        public string Detail { get; set; }
        public string Photo { get; set; }
        public DateTime ReportDate { get; set; }
        public int Counter { get; set; }
        public bool Deleted { get; set; }
        public bool Flagged { get; set; }
        public bool Public { get; set; }
        public int SendFailCount { get; set; }

        //Quem envia
        public bool Anonymous { get; set; }
        public string ApplicationUserID { get; set; }

        public Problem()// construtor default
        {
            ReportDate = DateTime.Now;
            Anonymous = false;
            Counter = 0;
            Deleted = false;
            Flagged = false;
            Public = true;
            SendFailCount = 0;
        }

        [ForeignKey("Council")]
        public int CouncilID { get; set; }
        public Council Council { get; set; }

    }
}
