using FootballLeagueWebAPI.EntityFramework;
using FootballLeagueWebAPI.Repositories;
using FootballLeagueWebAPI.Requests;
using FootballLeagueWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueWebAPI
{
    public static class DataInitializer
    {
        public static void Seed(LeagueContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.ExecuteSqlCommand("SET IDENTITY INSERT dbo.Teams ON");
            //context.Database.ExecuteSqlCommand("SET IDENTITY INSERT dbo.Players ON");
            //context.Database.ExecuteSqlCommand("SET IDENTITY INSERT dbo.Matches ON");
            context.SaveChanges();

            var teams = new List<TeamRequest>
            {
                new TeamRequest
                {
                    
                    Name = "Legia",
                    City = "Warszawa"
                },
                new TeamRequest
                {
                    Name = "Wisla",
                    City = "Krakow"
                },
                new TeamRequest
                {
                    Name = "Slask",
                    City = "Wroclaw"
                }
            };

            var players = new List<PlayerRequest>
            {
                new PlayerRequest
                {
                    FirstName="Jack",
                    SurName="Reacher",
                    Age=30,
                    TeamId=1
                },
                new PlayerRequest
                {
                    FirstName="John",
                    SurName="Carter",
                    Age=35,
                    TeamId=1
                },
                new PlayerRequest
                {
                    FirstName="Robert",
                    SurName="DeNirro",
                    Age=30,
                    TeamId=2
                },
                new PlayerRequest
                {
                    FirstName="Jack",
                    SurName="Strong",
                    Age=38,
                    TeamId=3
                }
            };

            var matches = new List<MatchRequest>
            {
                new MatchRequest
                {
                    HomeTeamId = 1,
                    GuestTeamId = 2,
                    HomeTeamGoals = 1,
                    GuestTeamGoals = 2
                },
                new MatchRequest
                {
                    HomeTeamId = 3,
                    GuestTeamId = 1,
                    HomeTeamGoals = 1,
                    GuestTeamGoals = 2
                },
                new MatchRequest
                {
                    HomeTeamId = 2,
                    GuestTeamId = 3,
                    HomeTeamGoals = 1,
                    GuestTeamGoals = 1
                },
            };

            TeamRepository teamRepositiory = new TeamRepository(context);
            PlayerRepositiory playerRepositiory = new PlayerRepositiory(context);
            MatchRepository matchRepository = new MatchRepository(context);

            LeagueInputService leagueInputService = new LeagueInputService(teamRepositiory, playerRepositiory, matchRepository);

            foreach(TeamRequest teamRequest in teams)
            {
                leagueInputService.CreateTeam(teamRequest);
            }

            foreach (PlayerRequest playerRequest in players)
            {
                leagueInputService.AddPlayer(playerRequest);
            }

            foreach (MatchRequest matchRequest in matches)
            {
                leagueInputService.PlayMatch(matchRequest);
            }

            //context.Database.ExecuteSqlCommand("SET IDENTITY INSERT dbo.Events OFF");
            //context.Database.ExecuteSqlCommand("SET IDENTITY INSERT dbo.Persons OFF");
            //context.Database.ExecuteSqlCommand("SET IDENTITY INSERT dbo.PersonEvents OFF");
            context.SaveChanges();
        }
    }
}
