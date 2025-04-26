using System;

namespace Auction_Management_System.Entities
{
    public class Bid
    {
        public int BidID { get; set; }
        public int AuctionID { get; set; }
        public int UserID { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; } = DateTime.Now;
    }
}