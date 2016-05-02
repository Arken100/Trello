using Lelo.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lelo.Models
{
    public class Board : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<TaskList> ColumnBoards { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}


