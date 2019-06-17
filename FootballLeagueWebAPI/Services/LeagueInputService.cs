using FootballLeagueWebAPI.Models;
using FootballLeagueWebAPI.Repositories;
using FootballLeagueWebAPI.Requests;
using System;

namespace FootballLeagueWebAPI.Services
{
    public class LeagueInputService
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
            _teamRepository.RemovePlayer(playerId);

            _teamRepository.AddPlayer(newTeamId, _playerRepositiory.GetById(playerId));
        }

        public void RemovePlayerById(int playerId)
        {
            _playerRepositiory.RemoveById(playerId);
        }

        public void PlayMatch(MatchRequest matchRequest)
        {
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
