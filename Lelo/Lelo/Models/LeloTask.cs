using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lelo.Models
{
    public class LeloTask : Base
    {
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        public int? Position { get; set; }



        public int? TaskListId { get; set; }

        [Display(Name="Lista")]
        public TaskList TaskList { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}