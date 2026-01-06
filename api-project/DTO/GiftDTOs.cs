using api_project.Models;

namespace api_project.DTO
{
    public class GiftCreateDTO()
    {
        public string GiftName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryName { get; set; }
        public Categories? category { get; set; }

    }
    public class GiftUpdateDto()
    {
        public string GiftName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryName { get; set; }
        public Categories? category { get; set; }

    }

    public class GiftReadDTO() {
            public int Id { get; set; }
            public string GiftName { get; set; }
            public decimal Price { get; set; }
            public string ?IMageUrl { get; set; } 
    }
}
