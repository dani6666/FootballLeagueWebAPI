using FootballLeagueWebAPI.DTO;
using FootballLeagueWebAPI.Models;
using System.Collections.Generic;

namespace FootballLeagueWebAPI.Mappings
{
    public static class PlayerMapper
    {
        public static List<PlayerDTO> Map(this List<Player> players)
        {
            List<PlayerDTO> result = new List<PlayerDTO>();

            foreach (Player player in players)
            {
                result.Add(player.Map());
            }

            return result;
        }

        public static PlayerDTO Map(this Player player)
        {
            return new PlayerDTO()
            {
                FirstName = player.FirstName,
                SurName = player.SurName,
                Age = player.Age,
                TeamName = player.Team.Name,
                TeamCity = player.Team.City
            };
        }
    }
}
