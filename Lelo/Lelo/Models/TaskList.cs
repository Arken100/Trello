using Lelo.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lelo.Models
{
    public class TaskList : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public Guid BoardId { get; set; }
        public Board Board { get; set; }
        public virtual ICollection<LeloTask> LeloTasks { get; set; }

      
    }
}