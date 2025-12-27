namespace api_project.Models
{
    public class Gifts
    {
        public int Id { get; set; }
        public string GiftName { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public int NumOfPurchases { get; set; }
        public int Price { get; set; }
        public int DonorId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

    }
}
