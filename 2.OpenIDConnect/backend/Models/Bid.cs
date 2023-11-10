namespace Globomantics.Backend.Models
{
    public record Bid
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public string BidderName { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}