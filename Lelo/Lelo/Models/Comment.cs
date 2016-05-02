namespace Lelo.Models
{
    public class Comment : Base
    {
        public string Description { get; set; }

        public virtual LeloTask LeloTask { get; set; }
    }
}