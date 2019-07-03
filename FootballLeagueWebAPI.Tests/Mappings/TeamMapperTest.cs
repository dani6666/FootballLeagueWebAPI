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
    class TeamMapperTest
    {
        private Team _team;
        [SetUp]
        public void SetUp()
        {
            _team = new Team
            {
                Id = 1,
                Name = "name",
                City = "city",
                Points = 10,
                Wins = 3,
                Draws = 1,
                Loses = 2
            };
        }

        [Test]
        public void Map_ShoudlAssingPropertiesCorrectly()
        {
            TeamDTO mappedTeam = _team.Map();

            mappedTeam.Name.Should().Be(_team.Name);
            mappedTeam.City.Should().Be(_team.City);
            mappedTeam.Points.Should().Be(_team.Points);
            mappedTeam.Wins.Should().Be(_team.Wins);
            mappedTeam.Draws.Should().Be(_team.Draws);
            mappedTeam.Loses.Should().Be(_team.Loses);
            mappedTeam.Id.Should().Be(_team.Id);
        }

        [Test]
        public void Map_ReturndNull_WhenTeamIsNull()
        {
            Team team = null;

            team.Map().Should().BeNull();
        }
    }
}
