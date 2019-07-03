using FootballLeagueWebAPI.Models;
using FootballLeagueWebAPI.Repositories;
using FootballLeagueWebAPI.Requests;
using System;

namespace FootballLeagueWebAPI.Services
{
    public class LeagueInputService : ILeagueInputService
    {
        private readonly TeamRepository _teamRepository;
        private readonly PlayerRepositiory _playerRepositiory;
        private readonly MatchRepository _matchRepository;
        private (int Team, int Player, int Match) StartingId = (0, 0, 0);

        public LeagueInputService(TeamRepository teamRepository, PlayerRepositiory playerRepositiory, MatchRepository matchRepositiory)
        {
            _teamRepository = teamRepository;
            _playerRepositiory = playerRepositiory;
            _matchRepository = matchRepositiory;

            StartingId = (
                _teamRepository.GetStartingId(),
                _playerRepositiory.GetStartingId(),
                _matchRepository.GetStartingId()
                );
        }

        public void CreateTeam(TeamRequest teamRequest)
        {
            Team team = new Team
            {
                Id = StartingId.Team++,
                Name = teamRequest.Name,
                City = teamRequest.City
            };

            _teamRepository.Add(team);
        }

        public void RemoveTeamById(int teamId)
        {
            if(!_teamRepository.IdExists(teamId))
            {
                throw new ArgumentException();
            }

            _teamRepository.RemoveById(teamId);
        }

        public void AddPlayer(PlayerRequest playerRequest)
        {
            Player player = new Player
            {
                Id = StartingId.Player++,
                FirstName = playerRequest.FirstName,
                SurName = playerRequest.SurName,
                Age = playerRequest.Age,
                Team = _teamRepository.GetById(playerRequest.TeamId)
            };

            _playerRepositiory.Add(player);

            _teamRepository.AddPlayer(playerRequest.TeamId, player);
        }

        public void TransferPlayer(int playerId, int newTeamId)
        {
            if(!_playerRepositiory.IdExists(playerId) || 
                !_teamRepository.IdExists(newTeamId))
            {
                throw new ArgumentException();
            }

            _teamRepository.RemovePlayer(playerId);

            _teamRepository.AddPlayer(newTeamId, _playerRepositiory.GetById(playerId));
        }

        public void RemovePlayerById(int playerId)
        {
            if(!_playerRepositiory.IdExists(playerId))
            {
                throw new ArgumentException();
            }

            _playerRepositiory.RemoveById(playerId);
        }

        public void PlayMatch(MatchRequest matchRequest)
        {
            if(matchRequest.GuestTeamGoals<0 || matchRequest.HomeTeamGoals<0 || 
                !_teamRepository.IdExists(matchRequest.HomeTeamId) || 
                !_teamRepository.IdExists(matchRequest.GuestTeamId) ||
                matchRequest.GuestTeamId == matchRequest.HomeTeamId)
            {
                throw new ArgumentException();
            }

            Match match = new Match
            {
                Id = StartingId.Match++,
                HomeTeam = _teamRepository.GetById(matchRequest.HomeTeamId),
                GuestTeam = _teamRepository.GetById(matchRequest.GuestTeamId),
                DateOfMatch = DateTime.Now,
                HomeTeamGoals = matchRequest.HomeTeamGoals,
                GuestTeamGoals = matchRequest.GuestTeamGoals
            };


            bool? hasHomeTeamWon, hasGuestTeamWon;
            if(match.HomeTeamGoals > match.GuestTeamGoals)
            {
                hasHomeTeamWon = true;
                hasGuestTeamWon = false;
            }
            else if(match.HomeTeamGoals == match.GuestTeamGoals)
            {
                hasHomeTeamWon = null;
                hasGuestTeamWon = null;
            }
            else
            {
                hasHomeTeamWon = false;
                hasGuestTeamWon = true;
            }


            _matchRepository.Add(match);

            _teamRepository.AddMatchPlayed(matchRequest.HomeTeamId, match, hasHomeTeamWon, true);
            _teamRepository.AddMatchPlayed(matchRequest.GuestTeamId, match, hasGuestTeamWon, false);
        }
    }
}
