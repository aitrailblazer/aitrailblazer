public class CopilotSpecial
    {
        public int Id { get; set; } = 0;

        public string? Name { get; set; } = string.Empty;

        public string? PageUrl { get; set; } = string.Empty;

        public decimal BasePrice { get; set; } = 0.0m;

        public string ShortDescription { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string GetFormattedBasePrice() => BasePrice.ToString("0.00");
    }
