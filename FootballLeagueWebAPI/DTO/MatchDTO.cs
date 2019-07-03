using System;

namespace FootballLeagueWebAPI.DTO
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamCity { get; set; }
        public string GuestTeamName { get; set; }
        public string GuestTeamCity { get; set; }
        public DateTime DateOfMatch { get; set; }
        public int HomeTeamGoals { get; set; }
        public int GuestTeamGoals { get; set; }
    }
}
