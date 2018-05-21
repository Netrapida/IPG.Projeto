using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IPG.Projeto.MVC.Models
{
    public class Council
    {
        [Key]
        public int CouncilID { get; set; }         
        public int Id { get; set; }
        public string Name { get; set; }
        public int Reported { get; set; }
        public int ReportedFix { get; set; }
        public bool Deleted { get; set; }
        public string ExternalUrl { get; set; }

        //V_1
        //Contacto único -- normalizar
        public string Email { get; set; }


        //Navigation
        public ICollection<Problem> Problems { get; set; }



    }
}