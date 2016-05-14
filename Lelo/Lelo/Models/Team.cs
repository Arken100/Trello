using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lelo.Models
{
    public class Team : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("User")]
        public Guid OwnerId { get; set; }


        public ApplicationUser User { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}