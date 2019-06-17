
namespace FootballLeagueWebAPI.Requests
{
    public class MatchRequest
    {
        public int HomeTeamId { get; set; }
        public int GuestTeamId { get; set; }
        public int HomeTeamGoals { get; set; }
        public int GuestTeamGoals { get; set; }
    }
}
