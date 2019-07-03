using FootballLeagueWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueWebAPI.Services
{
    public interface ILeagueOutputService
    {
        List<TeamDTO> GetLeagueTable();
        List<MatchDTO> GetAllMatches();
        TeamDTO GetTeamById(int id);
        List<PlayerDTO> GetAllPlayersOfTheTeam(int teamId);
    }
}
