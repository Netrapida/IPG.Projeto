using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPG.Projeto.MVC.Models
{
    public class Problem
    {   //Id's 
        [Key]
        public int Id { get; set; }     
        //propriedades
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        [Required][StringLength(180)]
        public string Text { get; set; }    
        [Required][StringLength(500)]
        public string Detail { get; set; }
        public string Photo { get; set; }
        public bool Anonymous { get; set; }
        [Required]
        public DateTime ReportDate { get; set; }
        public DateTime? LastUpdate { get; set; } // alterar para mandatario
        public DateTime? WhenSentDate { get; set; }
        public bool Flagged { get; set; }
        public int SendFailCount { get; set; }
        public string SenDFailReason { get; set; }
        public DateTime? SendFailDate { get; set; }
        public int Counter { get; set; }
        
        //V_6
        public bool Public { get; set; }    
        public int State { get; set; }
        public bool Deleted { get; set; }


        //Navigation
        [ForeignKey("User")]
        public string ApplicationUserID { get; set; }
        public ApplicationUser User { get; set; }
        
        [ForeignKey("Council")]
        public int CouncilID { get; set; }  
        public Council Council { get; set; }    

        public ICollection<Comment> Comments { get; set; }

        public Problem()// construtor default
        {
            ReportDate = DateTime.Now;
            Flagged = false;
            Counter = 0;
            SendFailCount = 0;
            Anonymous = false;
        }
    }
}