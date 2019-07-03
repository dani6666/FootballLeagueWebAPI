using FootballLeagueWebAPI.EntityFramework;
using FootballLeagueWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballLeagueWebAPI.Repositories
{
    public abstract class LeagueRepository<T> where T : Model
    {
        protected readonly LeagueContext _context;
        public LeagueRepository(LeagueContext context)
        {
            _context = context;
        }

        public int GetStartingId()
        {
            try
            {
                return _context.Set<T>().Select((r) => r.Id).Max() + 1;
            }
            catch(InvalidOperationException)
            {
                return 1;
            }
        }

        public virtual void Add(T record)
        {
            _context.Set<T>().Add(record);

            _context.SaveChanges();
        }

        public void Save(T record)
        {
            _context.Set<T>().Update(record);

            _context.SaveChanges();
        }

        public abstract T GetById(int id);

        public abstract List<T> GetAll();

        public void RemoveById(int id)
        {
            _context.Set<T>()
                .Remove(_context.Set<T>()
                    .Where(m => m.Id == id)
                    .FirstOrDefault());

            _context.SaveChanges();
        }

        public virtual bool IdExists(int id)
        {
            return _context.Set<T>().Any(r => r.Id == id);
        }
    }
}
