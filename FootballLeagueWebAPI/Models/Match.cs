    using System;

namespace FootballLeagueWebAPI.Models
{
    public class Match : Model
    {
        public Team HomeTeam { get; set; }
        public Team GuestTeam { get; set; }
        public DateTime DateOfMatch { get; set; }
        public int HomeTeamGoals { get; set; }
        public int GuestTeamGoals{ get; set; }
    }
}
