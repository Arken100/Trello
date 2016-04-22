using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowTrello.Models
{
    public class Board : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<ColumnBoard> ColumnBoards { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}


