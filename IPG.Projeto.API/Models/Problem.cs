using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IPG.Projeto.API.Models
{
    [DataContract] // não fazer serialize das proriedades privadas
    public class Problem
    {
        [DataMember] // este será serialize
        public int Id { get; set; }
        [DataMember] // este será serialize
        //Onde se Encontra
        public double Latitude { get; set; }
        [DataMember] // este será serialize
        public double Longitude { get; set; }
        [DataMember] // este será serialize
        public int State { get; set; }


        //Detalhes do problema
        [Required]
        [StringLength(180)]
        [DataMember] // este será serialize
        public string Text { get; set; }
        [Required]
        [StringLength(500)]
        [DataMember] // este será serialize
        public string Detail { get; set; }
        [DataMember] // este será serialize
        public string Photo { get; set; }
        [DataMember] // este será serialize
        public DateTime ReportDate { get; set; }
        [DataMember] // este será serialize
        public int Counter { get; set; }
        public bool Deleted { get; set; }
        public bool Flagged { get; set; }
        public bool Public { get; set; }
        public int SendFailCount { get; set; }
        [DataMember] // este será serialize
        public DateTime LastUpdate { get; set; }

        //Quem envia
        [DataMember] // este será serialize
        public bool Anonymous { get; set; }
   
        //public string ApplicationUserID { get; set; }

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
        [DataMember] // este será serialize
        public int CouncilID { get; set; }
        [DataMember] // este será serialize
        public Council Council { get; set; }

        //Navigation
        [ForeignKey("User")]
        public string ApplicationUserID { get; set; }
        [DataMember] // este será serialize
        public ApplicationUser User { get; set; }

    }
}
