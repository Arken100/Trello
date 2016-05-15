using Lelo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lelo.Models
{
    public class Board : Base
    {

        [Display(Name = "Nazwa tablicy")]
        public string Title { get; set; }
        [Display(Name= "Opis")]
        public string Description { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [ForeignKey("Team")]
        public int? TeamId { get; set; }

        [Display(Name ="Team")]
        public Team Team { get; set; }

        [Display(Name = "Właściciel")]
        public ApplicationUser User { get; set; }

        public virtual ICollection<TaskList> TaskLists { get; set; }
    }
}


