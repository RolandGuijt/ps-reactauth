using Globomantics.Backend.Models;

namespace Globomantics.Backend.Repositories
{
    public class BidRepository
    {
        private List<Bid> bids = new()
        {
            new Bid { Id = 1, HouseId = 1, BidderName = "Sonia Reading", Amount = 200000 },
            new Bid { Id = 2, HouseId = 1, BidderName = "Dick Johnson", Amount = 202400 },
            new Bid { Id = 3, HouseId = 2, BidderName = "Mohammed Vahls", Amount = 302400 },
            new Bid { Id = 4, HouseId = 2, BidderName = "Jane Williams", Amount = 310500 },
            new Bid { Id = 5, HouseId = 2, BidderName = "John Kepler", Amount = 315400 },
            new Bid { Id = 6, HouseId = 3, BidderName = "Bill Mentor", Amount = 201000 },
            new Bid { Id = 7, HouseId = 4, BidderName = "Melissa Kirk", Amount = 410000 },
            new Bid { Id = 8, HouseId = 4, BidderName = "Scott Max", Amount = 450000 },
            new Bid { Id = 9, HouseId = 4, BidderName = "Christine James", Amount = 470000 },
            new Bid { Id = 10, HouseId = 5, BidderName = "Omesh Carim", Amount = 450000 },
            new Bid { Id = 11, HouseId = 5, BidderName = "Charlotte Max", Amount = 150000 },
            new Bid { Id = 12, HouseId = 5, BidderName = "Marcus Scott", Amount = 170000 },
        };

        public List<Bid> GetBids(int houseId) =>
            bids.Where(b => b.HouseId == houseId).ToList();

        public void Add(Bid bid)
        {
            bid.Id = bids.Max(b => b.Id) + 1;
            bids.Add(bid);
        }
    }
}