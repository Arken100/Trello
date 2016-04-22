using System.Collections.Generic;

namespace PowTrello.Models
{
    public class Team : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Board> Boards { get; set; }
    }
}