using Lelo.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lelo.Models
{
    public class TaskList : Base
    {
        [Display(Name="Nazwa listy")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [ForeignKey("Board")]
        public int? BoardId { get; set; }

        public Board Board { get; set; }

        public int? Position { get; set; }


        public virtual ICollection<LeloTask> LeloTasks { get; set; }

      
    }
}