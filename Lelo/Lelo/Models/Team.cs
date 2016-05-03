using System.Collections.Generic;

namespace Lelo.Models
{
    public class Team : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}