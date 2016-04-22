using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PowTrello.Models
{
    public class ColumnBoard : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Board Board { get; set; }
        public virtual ICollection<TrelloTask> TrelloTask { get; set; }
    }


}