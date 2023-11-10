namespace Globomantics.Backend.Models
{
    public record House
    {
        public int Id { get; set; }
        public string? Address { get; init; }
        public string? Country { get; init; }
        public string? Description { get; init; }
        public int Price { get; init; }
        public string? Photo { get; init; }
    }
}