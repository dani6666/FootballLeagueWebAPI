using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FootballLeagueWebAPI.DTO;
using FootballLeagueWebAPI.EntityFramework;
using FootballLeagueWebAPI.Mappings;
using FootballLeagueWebAPI.Models;
using FootballLeagueWebAPI.Repositories;
using FootballLeagueWebAPI.Services;
using Moq;
using NUnit.Framework;

namespace FootballLeagueWebAPI.Tests.Services
{
    [TestFixture]
    class LeagueOutputServiceTest
    {
        private List<Team> _teams;
        private List<TeamDTO> _mappedTeams;
        private List<Models.Match> _matches;
        private List<MatchDTO> _mappedMatches;
        private List<Player> _players;
        private List<PlayerDTO> _mappedPlayers;
        private List<PlayerDTO> _mappedPlayerOfTeam1;

        private Mock<TeamRepository> _teamRepository;
        private Mock<PlayerRepositiory> _playerRepository;
        private Mock<MatchRepository> _matchRepository;

        private ILeagueOutputService _leagueOutputService;

        [SetUp]
        public void SetUp()
        {
            _teams = new List<Team>
            {
                new Team
                {
                    Id = 0
                },
                new Team
                {
                    Id = 1
                },
                new Team
                {
                    Id = 2
                },
            };
            _mappedTeams = _teams.Map();

            _matches = new List<Models.Match>
            {
                new Models.Match
                {
                    HomeTeam = _teams[0],
                    GuestTeam = _teams[1]
                },
                new Models.Match
                {
                    HomeTeam = _teams[1],
                    GuestTeam = _teams[2]
                }
            };
            _mappedMatches = _matches.Map();

            _players = new List<Player>
            {
                new Player
                {
                    Team = _teams[0]
                },
                new Player
                {
                    Team = _teams[0]
                },
                new Player
                {
                    Team = _teams[1]
                },
                new Player
                {
                    Team = _teams[1]
                },
                new Player
                {
                    Team = _teams[0]
                },
                new Player
                {
                    Team = _teams[1]
                },
            };
            _mappedPlayers = _players.Map();
            _mappedPlayerOfTeam1 = new List<PlayerDTO>
            {
                _mappedPlayers[2],
                _mappedPlayers[3],
                _mappedPlayers[5]
            };


            _teamRepository = new Mock<TeamRepository>(new LeagueContext(new Microsoft.EntityFrameworkCore.DbContextOptions<LeagueContext>()));
            _teamRepository.Setup(x => x.GetAll()).Returns(_teams);
            _teamRepository.Setup(x => x.GetById(1)).Returns(_teams[1]);
            _teamRepository.Setup(x => x.GetById(-50)).Returns<Team>(null);

            _playerRepository = new Mock<PlayerRepositiory>(new LeagueContext(new Microsoft.EntityFrameworkCore.DbContextOptions<LeagueContext>()));
            _playerRepository.Setup(x => x.GetAll()).Returns(_players);

            _matchRepository = new Mock<MatchRepository>(new LeagueContext(new Microsoft.EntityFrameworkCore.DbContextOptions<LeagueContext>()));
            _matchRepository.Setup(x => x.GetAll()).Returns(_matches);

            _leagueOutputService = new LeagueOutputService(_teamRepository.Object, _playerRepository.Object, _matchRepository.Object);
        }

        #region GetLeagueTable()
        [Test]
        public void GetLeagueTable_ShouldReturnAllTeamsConverted()
        {
            _leagueOutputService.GetLeagueTable().Should().BeOfType(typeof(List<TeamDTO>));
            _leagueOutputService.GetLeagueTable().Should().BeEquivalentTo(_mappedTeams);
        }

        [Test]
        public void GetLeagueTable_ShouldReturnTeamsInPointsDescendingOrder()
        {
            _leagueOutputService.GetLeagueTable().Should().BeInDescendingOrder(t => t.Points);
        }
        #endregion

        #region GetAllMatches()
        [Test]
        public void GetAllMatches_ShouldReturnAllMatchesConverted()
        {
            _leagueOutputService.GetAllMatches().Should().BeOfType(typeof(List<MatchDTO>));
            _leagueOutputService.GetAllMatches().Should().BeEquivalentTo(_mappedMatches);
        }
        #endregion

        #region GetTeamById(int id)
        [Test]
        public void GetTeamById_ShouldReturnRightTeamConverted()
        {
            _leagueOutputService.GetTeamById(1).Should().BeOfType(typeof(TeamDTO));
            _leagueOutputService.GetTeamById(1).Id.Should().Be(_mappedTeams[1].Id);
        }

        [Test]
        public void GetTeamById_ReturnsNull_WhenIdIsNegative()
        {
            _leagueOutputService.GetTeamById(-50).Should().BeNull();
        }

        [Test]
        public void GetTeamById_ReturnsNull_WhenIdIsTooHigh()
        {
            _leagueOutputService.GetTeamById(-50).Should().BeNull();
        }
        #endregion

        #region GetAllPlayersOfTheTeam(int id)
        [Test]
        public void GetAllPlayersOfTheTeam_ShouldReturnConvertedPlayers()
        {
            _leagueOutputService.GetAllPlayersOfTheTeam(1).Should().BeOfType(typeof(List<PlayerDTO>));
        }

        [Test]
        public void GetAllPlayersOfTheTeam_ShouldReturnRightNumberOfPlayers()
        {
            _leagueOutputService.GetAllPlayersOfTheTeam(1).Count.Should().Be(3);
        }

        [Test]
        public void GetAllPlayersOfTheTeam_ShouldReturnOnlyPlayersFromThisTeam()
        {
            foreach (PlayerDTO player in _leagueOutputService.GetAllPlayersOfTheTeam(1))
            {
                _mappedPlayerOfTeam1.Select(t => t.Id).Contains(player.Id).Should().BeTrue();
            }
        }

        [Test]
        public void GetAllPlayersOfTheTeam_ShouldReturnAllPlayersFromThisTeam()
        {
            _leagueOutputService.GetAllPlayersOfTheTeam(1).Should().BeEquivalentTo(_mappedPlayerOfTeam1);
        }

        [Test]
        public void GetAllPlayersOfTheTeam_ReturnsEmptyList_WhenTeamHasNoPlayers()
        {
            _leagueOutputService.GetAllPlayersOfTheTeam(2).Should().BeEmpty();
        }

        [Test]
        public void GetAllPlayersOfTheTeam_ReturnsEmptyList_WhenTeamIdIsNeagtive([Range(-100,-1)] int teamId)
        {
            _leagueOutputService.GetAllPlayersOfTheTeam(teamId).Should().BeEmpty();
        }

        [Test]
        public void GetAllPlayersOfTheTeam_ReturnsEmptyList_WhenTeamIdIsTooHigh([Range(3, 100)] int teamId)
        {
            _leagueOutputService.GetAllPlayersOfTheTeam(teamId).Should().BeEmpty();
        }
        #endregion
    }
}
