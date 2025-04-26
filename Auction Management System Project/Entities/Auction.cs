using System;

namespace Auction_Management_System.Entities
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "ACTIVE";
        public int CreatorID { get; set; }
    }
}