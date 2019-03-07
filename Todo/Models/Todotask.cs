using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class Todotask
    {
        public Todotask()
        {
            this.UserId = null;
            this.UpdatedDT = new DateTime(1980, 1, 1, 0, 0, 0, 0);
        }

        public Todotask(string userId)
        {
            this.UserId = userId;
            this.UpdatedDT = new DateTime(1980, 1, 1, 0, 0, 0, 0);
        }

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "A Task Name is required")]
        [Display(Name = "Task Name")]
        public string Name { get; set; }

        [Display(Name = "Task Description")]
        [StringLength(160)]
        public string Description { get; set; }

        [Display(Name = "Completed")]
        public bool Done { get; set; }

        [Required]
        [Display(Name = "Last Updated")]
        public DateTime UpdatedDT { get; set; }

    }

}