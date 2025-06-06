namespace WiiZoneNowy.Models
{
    public class GameTag
    {
        public int GameId { get; set; }
        public Game Game { get; set; } = default!;

        public int TagId  { get; set; }
        public Tag Tag    { get; set; } = default!;
    }
}