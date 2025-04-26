using System;

namespace Auction_Management_System.Entities
{
    public class Winner
    {
        public int WinnerID { get; set; }
        public int AuctionID { get; set; }
        public int UserID { get; set; }
        public decimal WinningBid { get; set; }
        public DateTime DeclaredTime { get; set; } = DateTime.Now;
    }
}