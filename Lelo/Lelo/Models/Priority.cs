using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lelo.Models
{
    public class Priority : Base
    {
        public string Name { get; set; }

        public string  Color { get; set; }

        public Board Board { get; set; }

        public virtual ICollection<LeloTask> LeloTasks { get; set; }
    }
}