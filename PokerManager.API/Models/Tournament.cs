namespace PokerManager.API.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public int MaxPlayers { get; set; }
        public decimal BuyIn { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}