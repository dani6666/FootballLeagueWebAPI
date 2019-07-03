using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FootballLeagueWebAPI.DTO;
using FootballLeagueWebAPI.EntityFramework;
using FootballLeagueWebAPI.Mappings;
using FootballLeagueWebAPI.Models;
using FootballLeagueWebAPI.Repositories;
using FootballLeagueWebAPI.Requests;
using FootballLeagueWebAPI.Services;
using Moq;
using NUnit.Framework;

namespace FootballLeagueWebAPI.Tests.Services
{
    [TestFixture]
    class LeagueInputServiceTest
    {
        private Models.Match _playedMatch;
        private Team _homeTeam, _guestTeam;
        private Mock<TeamRepository> _teamRepository;
        private Mock<PlayerRepositiory> _playerRepository;
        private Mock<MatchRepository> _matchRepository;

        private ILeagueInputService _leagueInputService;
        [SetUp]
        public void SetUp()
        {
            _homeTeam = new Team
            {
                Id = 3,
            };
            _guestTeam = new Team
            {
                Id = 4,
            };

            _teamRepository = new Mock<TeamRepository>(new LeagueContext(new Microsoft.EntityFrameworkCore.DbContextOptions<LeagueContext>()));
            _teamRepository.Setup(x => x.IdExists(1)).Returns(false);
            _teamRepository.Setup(x => x.IdExists(2)).Returns(false);
            _teamRepository.Setup(x => x.IdExists(3)).Returns(true);
            _teamRepository.Setup(x => x.IdExists(4)).Returns(true);
            _teamRepository.Setup(x => x.GetById(3)).Returns(_homeTeam);
            _teamRepository.Setup(x => x.GetById(4)).Returns(_guestTeam);
            _teamRepository.Setup(x => x.AddMatchPlayed(It.IsAny<int>(), It.IsAny<Models.Match>(), It.IsAny<bool?>(), It.IsAny<bool>()));

            _playerRepository = new Mock<PlayerRepositiory>(new LeagueContext(new Microsoft.EntityFrameworkCore.DbContextOptions<LeagueContext>()));
            _playerRepository.Setup(x => x.IdExists(1)).Returns(true);
            _playerRepository.Setup(x => x.IdExists(2)).Returns(false);

            _matchRepository = new Mock<MatchRepository>(new LeagueContext(new Microsoft.EntityFrameworkCore.DbContextOptions<LeagueContext>()));
            _matchRepository.Setup(x => x.Add(It.IsAny<Models.Match>())).Callback((Models.Match m) =>
               {
                   _playedMatch = m;
               });

            _leagueInputService = new LeagueInputService(_teamRepository.Object, _playerRepository.Object, _matchRepository.Object);
        }

        [Test]
        public void RemoveTeamById_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            Assert.Throws<ArgumentException>(() => _leagueInputService.RemoveTeamById(2));
        }

        [Test]
        public void TransferPlayer_ThrowsArgumentException_WhenIdsDoNotExist()
        {
            Assert.Throws<ArgumentException>(() => _leagueInputService.TransferPlayer(1, 2));
            Assert.Throws<ArgumentException>(() => _leagueInputService.TransferPlayer(2, 3));
        }

        [Test]
        public void RemovePlayerById_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            Assert.Throws<ArgumentException>(() => _leagueInputService.RemovePlayerById(2));
        }

        [Test]
        public void PlayMatch_CorretlyAssingsTeamsAndGoals()
        {
            MatchRequest matchRequest = new MatchRequest
            {
                HomeTeamId = 3,
                GuestTeamId = 4,
                HomeTeamGoals = 1,
                GuestTeamGoals = 0
            };

            _leagueInputService.PlayMatch(matchRequest);

            _playedMatch.HomeTeam.Should().Be(_homeTeam);
            _playedMatch.GuestTeam.Should().Be(_guestTeam);
            _playedMatch.HomeTeamGoals.Should().Be(1);
            _playedMatch.GuestTeamGoals.Should().Be(0);
        }

        [Test]
        public void PlayMatch_ThrowsArgumentException_WhenMatchRequestHasWrongProperties()
        {
            MatchRequest matchRequest = new MatchRequest
            {
                HomeTeamId = 3,
                GuestTeamId = 3,
                HomeTeamGoals = 1,
                GuestTeamGoals = 0
            };
            Assert.Throws<ArgumentException>(() => _leagueInputService.PlayMatch(matchRequest));

            matchRequest.GuestTeamId = 1;
            Assert.Throws<ArgumentException>(() => _leagueInputService.PlayMatch(matchRequest));

            matchRequest.GuestTeamId = 4;
            matchRequest.HomeTeamId = 2;
            Assert.Throws<ArgumentException>(() => _leagueInputService.PlayMatch(matchRequest));

            matchRequest.HomeTeamId = 3;
            matchRequest.HomeTeamGoals = 1;
            matchRequest.GuestTeamGoals = -5;
            Assert.Throws<ArgumentException>(() => _leagueInputService.PlayMatch(matchRequest));

            matchRequest.HomeTeamGoals = -1;
            matchRequest.GuestTeamGoals = 2;
            Assert.Throws<ArgumentException>(() => _leagueInputService.PlayMatch(matchRequest));
        }
    }
}
