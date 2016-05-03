using Lelo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lelo.Models
{
    public class Board : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }
        public int TeamId { get; set; }


        public Team Team { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<TaskList> TaskList { get; set; }
    }
}


