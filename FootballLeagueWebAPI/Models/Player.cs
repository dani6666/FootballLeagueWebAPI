
namespace FootballLeagueWebAPI.Models
{
    public class Player : Model
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public Team Team { get; set; }
    }
}
