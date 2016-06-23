using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lelo.Models
{
    public class Priority : Base
    {
        [Display(Name ="Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Kolor")]
        public string  Color { get; set; }

        public Board Board { get; set; }

        public virtual ICollection<LeloTask> LeloTasks { get; set; }
    }
}