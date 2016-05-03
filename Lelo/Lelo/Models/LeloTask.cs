using System.Collections.Generic;

namespace Lelo.Models
{
    public class LeloTask : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }



        public int? TaskListId { get; set; }


        public TaskList TaskList { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}