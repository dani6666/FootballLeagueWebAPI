using FootballLeagueWebAPI.DTO;
using FootballLeagueWebAPI.Models;
using System.Collections.Generic;

namespace FootballLeagueWebAPI.Mappings
{
    public static class TeamMapper
    {
        public static List<TeamDTO> Map(this List<Team> teams)
        {
            List<TeamDTO> result = new List<TeamDTO>();

            foreach (Team team in teams)
            {
                result.Add(team.Map());
            }

            return result;
        }

        public static TeamDTO Map(this Team team)
        {
            return new TeamDTO()
            {
                Name = team.Name,
                City = team.City,
                Points = team.Points,
                Wins = team.Wins,
                Draws = team.Draws,
                Loses = team.Loses
            };
        }
    }
}
