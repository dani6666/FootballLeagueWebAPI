using FootballLeagueWebAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueWebAPI.Services
{
    public interface ILeagueInputService
    {
        void CreateTeam(TeamRequest teamRequest);
        void RemoveTeamById(int teamId);
        void AddPlayer(PlayerRequest playerRequest);
        void TransferPlayer(int playerId, int newTeamId);
        void RemovePlayerById(int playerId);
        void PlayMatch(MatchRequest matchRequest);
    }
}
