using System.Collections.Generic;

namespace PowTrello.Models
{
    public class TrelloTask : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ColumnBoard ColumnBoard { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}