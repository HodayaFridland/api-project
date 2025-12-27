namespace api_project.Models
{
    public class Baskets
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<Gifts> Gifts { get; set; } = new List<Gifts>();
    }
}
