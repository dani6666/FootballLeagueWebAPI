using FootballLeagueWebAPI.DTO;
using FootballLeagueWebAPI.Mappings;
using FootballLeagueWebAPI.Models;
using FootballLeagueWebAPI.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace FootballLeagueWebAPI.Services
{
    public class LeagueOutputService : ILeagueOutputService
    {
        private readonly TeamRepository _teamRepository;
        private readonly PlayerRepositiory _playerRepositiory;
        private readonly MatchRepository _matchRepository;
        public LeagueOutputService(TeamRepository teamRepository, PlayerRepositiory playerRepositiory, MatchRepository matchRepositiory)
        {
            _teamRepository = teamRepository;
            _playerRepositiory = playerRepositiory;
            _matchRepository = matchRepositiory;
        }

        public List<TeamDTO> GetLeagueTable()
        {
            var teams = _teamRepository.GetAll()
                .ToList();

            teams.Sort(Team.CompareByPonitsScored);

            return teams.Map();
        }

        public List<MatchDTO> GetAllMatches()
        {
            var matches = _matchRepository.GetAll().ToList();

            return matches.Map();
        }

        public TeamDTO GetTeamById(int id)
        {
            return _teamRepository.GetById(id).Map();
        }

        public List<PlayerDTO> GetAllPlayersOfTheTeam(int teamId)
        {
            var players = _playerRepositiory.GetAll().Where(p => p.Team.Id == teamId).ToList();

            return players.Map();
        }
    }
}
