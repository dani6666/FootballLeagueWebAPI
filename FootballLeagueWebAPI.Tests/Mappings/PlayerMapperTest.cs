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
    class PlayerMapperTest
    {
        private Player _player;
        [SetUp]
        public void SetUp()
        {
            _player = new Player
            {
                FirstName = "fname",
                SurName = "sname",
                Age = 23,
                Team = new Team
                {
                    Name = "name1",
                    City = "city1"
                }
            };
        }

        [Test]
        public void Map_ShoudlAssingPropertiesCorrectly()
        {
            PlayerDTO mappedPlayer = _player.Map();

            mappedPlayer.FirstName.Should().Be(_player.FirstName);
            mappedPlayer.SurName.Should().Be(_player.SurName);
            mappedPlayer.Age.Should().Be(_player.Age);
            mappedPlayer.TeamName.Should().Be(_player.Team.Name);
            mappedPlayer.TeamCity.Should().Be(_player.Team.City);
        }

        [Test]
        public void Map_ReturndNull_WhenPlayerIsIncomplete()
        {
            Player player = new Player
            {
                FirstName = "name"
            };

            player.Map().Should().BeNull();
        }
    }
}
