using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IPG.Projeto.MVC.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "Nome")]
        public string DisplayName { get; set; }
        [Display(Name = "Data de registo")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "Foto")]
        public string ProfilePicture { get; set; }
        public bool Flagged { get; set; }
        public int From_Council { get; set; }
        public int? Facebook_id { get; set; }

        public ICollection<Problem> Problems { get; set; }
        public ICollection<Comment> Comments { get; set; }  

        public ApplicationUser() // construtor default
        {
            Flagged = false;
            From_Council = 0;
            RegisterDate = DateTime.Now;
        }


    }
}
