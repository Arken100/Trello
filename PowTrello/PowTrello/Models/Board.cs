using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PowTrello.Models
{
    public class Board : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }


}