using FootballLeagueWebAPI.DTO;
using FootballLeagueWebAPI.Models;
using System.Collections.Generic;

namespace FootballLeagueWebAPI.Mappings
{
    public static class MatchMapper
    {
        public static List<MatchDTO> Map(this List<Match> matches)
        {
            List<MatchDTO> result = new List<MatchDTO>();

            foreach(Match match in matches)
            {
                result.Add(match.Map());
            }

            return result;
        }

        public static MatchDTO Map(this Match match)
        {
            return new MatchDTO()
            {
                HomeTeamName = match.HomeTeam.Name,
                HomeTeamCity = match.HomeTeam.City,
                GuestTeamName = match.GuestTeam.Name,
                GuestTeamCity = match.GuestTeam.City,
                DateOfMatch = match.DateOfMatch,
                HomeTeamGoals = match.HomeTeamGoals,
                GuestTeamGoals = match.GuestTeamGoals
            };

        }
    }
}
