namespace PowTrello.Models
{
    public class Comment : Base
    {
        public string Description { get; set; }

        public virtual TrelloTask TrelloTask { get; set; }
    }
}