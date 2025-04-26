using System;

namespace Auction_Management_System.Entities
{
    public class WatchlistItem
    {
        public int WatchlistID { get; set; }
        public int UserID { get; set; }
        public int AuctionID { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}