using FootballLeagueWebAPI.EntityFramework;
using FootballLeagueWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballLeagueWebAPI.Repositories
{
    public class TeamRepository : LeagueRepository<Team>
    {
        public TeamRepository(LeagueContext context)
            : base(context)
        {
        }

        public override Team GetById(int id)
        {
            return _context.Teams
                .Where(t => t.Id == id)
                .Include(t => t.Players)
                .Include(t => t.HomeMatchesPlayed)
                .Include(t => t.GuestMatchesPlayed)
                .FirstOrDefault();
        }

        public override List<Team> GetAll()
        {
            return _context.Teams
                .Include(t => t.Players)
                .Include(t => t.HomeMatchesPlayed)
                .Include(t => t.GuestMatchesPlayed)
                .ToList();
        }

        public virtual void AddMatchPlayed(int teamId, Match match, bool? wasWon, bool wasAtHome)
        {
            Team team = GetById(teamId);
            if(wasWon == true)
            {
                team.Wins++;
                team.Points += 3;
            }
            else if(wasWon == null)
            {
                team.Draws++;
                team.Points++;
            }
            else
            {
                team.Loses++;
            }

            if(wasAtHome)
            {
                team.HomeMatchesPlayed.Add(match);
            }
            else
            {
                team.GuestMatchesPlayed.Add(match);
            }
            Save(team);
        }

        public void AddPlayer(int teamId, Player player)
        {
            GetById(teamId).Players.Add(player);

            Save(GetById(teamId));
        }

       

        public void RemovePlayer(int playerId)
        {
            foreach (var team in _context.Teams.ToList())
            {
                if(team.Players.Select(p => p.Id).Contains(playerId))
                {
                    team.Players.Remove(team.Players.Where(p => p.Id == playerId).FirstOrDefault());

                    _context.SaveChanges();

                    return;
                }
            }
        }
    }
}
