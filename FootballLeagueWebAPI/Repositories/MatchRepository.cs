using FootballLeagueWebAPI.EntityFramework;
using FootballLeagueWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FootballLeagueWebAPI.Repositories
{
    public class MatchRepository : LeagueRepository<Match>
    {
        public MatchRepository(LeagueContext context)
            : base(context)
        {
        }

        public override Match GetById(int id)
        {
            return _context.Matches
                .Where(m => m.Id == id)
                .Include(m => m.HomeTeam)
                .Include(m => m.GuestTeam)
                .FirstOrDefault();
        }

        public override List<Match> GetAll()
        {
            return _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.GuestTeam)
                .ToList();
        }

        public List<Match> GetAllMacthesPlayedByTeamId(int teamId)
        {
            return _context.Matches
                .Where(m => (m.HomeTeam.Id == teamId || m.GuestTeam.Id == teamId))
                .Include(m => m.HomeTeam)
                .Include(m => m.GuestTeam)
                .ToList();
        }
    }
}
