using System.Collections.Generic;

namespace FootballLeagueWebAPI.Models
{
    public class Team : Model
    {
        public string Name { get; set; }
        public string City { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public ICollection<Match> HomeMatchesPlayed { get; set; } = new List<Match>();
        public ICollection<Match> GuestMatchesPlayed { get; set; } = new List<Match>();
        public int Points { get; set; } = 0;
        public int Wins { get; set; } = 0;
        public int Draws { get; set; } = 0;
        public int Loses { get; set; } = 0;
        public static int CompareByPonitsScored(Team team1, Team team2)
        {
            return team2.Points.CompareTo(team1.Points);
        }
    }
}
