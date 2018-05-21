using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPG.Projeto.MVC.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; } 

        // propriedades
        public bool Anonymous { get; set; }
        [StringLength(500)]
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime CommentDate { get; set; }
        public string Photo { get; set; }
        public int Council { get; set; }

        //Navigation
        [ForeignKey("User")]
        public string ApplicationUserID { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Problem")]
        public int ProblemID { get; set; }
        public Problem Problem { get; set; }

        public Comment()
        {
            Anonymous = false;
            CommentDate = DateTime.Now;
        }
    }
}