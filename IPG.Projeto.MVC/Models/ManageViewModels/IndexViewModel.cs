using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG.Projeto.MVC.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Número de contacto")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        //profile -------------------------------
        [Display(Name = "Data de registo")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "Foto de Perfil")]
        public string ProfilePicture { get; set; }
    }
}
