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

namespace FootballLeagueWebAPI.Tests.Mappings
{
    [TestFixture]
    class MatchMapperTest
    {
        private Models.Match _match;
        [SetUp]
        public void SetUp()
        {
            _match = new Models.Match
            {
                HomeTeam = new Team
                {
                    Name = "name1",
                    City = "city1"
                },
                GuestTeam = new Team
                {
                    Name = "name2",
                    City = "city2"
                },
                HomeTeamGoals = 2
            };
        }

        [Test]
        public void Map_ShoudlAssingPropertiesCorrectly()
        {
            MatchDTO mappedMatch = _match.Map();

            mappedMatch.DateOfMatch.Should().Be(_match.DateOfMatch);
            mappedMatch.HomeTeamCity.Should().Be(_match.HomeTeam.City);
            mappedMatch.HomeTeamName.Should().Be(_match.HomeTeam.Name);
            mappedMatch.HomeTeamGoals.Should().Be(_match.HomeTeamGoals);
            mappedMatch.GuestTeamCity.Should().Be(_match.GuestTeam.City);
            mappedMatch.GuestTeamName.Should().Be(_match.GuestTeam.Name);
            mappedMatch.GuestTeamGoals.Should().Be(_match.GuestTeamGoals);
            mappedMatch.Id.Should().Be(_match.Id);
        }

        [Test]
        public void Map_ReturndNull_WhenMatchIsIncomplete()
        {
            Models.Match match = new Models.Match
            {
                HomeTeam = new Team(),
                GuestTeamGoals = 3
            };

            match.Map().Should().BeNull();
        }
    }
}
