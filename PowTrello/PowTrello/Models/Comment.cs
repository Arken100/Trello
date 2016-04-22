using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PowTrello.Models
{
    public class Comment : Base
    {
        public string Description { get; set; }

        public virtual TrelloTask TrelloTask { get; set; }

    }


}