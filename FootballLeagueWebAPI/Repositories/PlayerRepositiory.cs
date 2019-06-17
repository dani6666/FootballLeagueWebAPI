using FootballLeagueWebAPI.EntityFramework;
using FootballLeagueWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FootballLeagueWebAPI.Repositories
{
    public class PlayerRepositiory : LeagueRepository<Player>
    {
        public PlayerRepositiory(LeagueContext context)
            : base(context)
        {
        }

        public override Player GetById(int id)
        {
            return _context.Players
                .Where(p => p.Id == id)
                .Include(p => p.Team)
                .FirstOrDefault();
        }

        public override List<Player> GetAll()
        {
            return _context.Players
                .Include(p => p.Team)
                .ToList();
        }
    }
}
