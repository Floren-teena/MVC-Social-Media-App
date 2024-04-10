using dotnetcoremorningclass.Data;
using dotnetcoremorningclass.Interfaces;
using dotnetcoremorningclass.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoremorningclass.Repository
{

    public class RaceRepository : IRaceRepository
    {
        //dependency injection and repo pattern(following clean architechture)//
        //becus you are not meant to put your business logic and database communication in the same file or the controller file

        //pending,succesful or fulfilled,fail(all under the promise when working with asynchronous)

        //this is also done to make testing easy 

        private readonly AppDbContext _context;
        public RaceRepository(AppDbContext context)
        {
            _context = context;
        }

        //whenever u r querying data from database make usre ur methods are asynchronous
        public async Task<IEnumerable<Race>> GetAll()
        {
            // throw new NotImplementedException();
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            //throw new NotImplementedException();
            return await _context.Races.Include(a => a.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Update(Race race)
        {
            //throw new NotImplementedException();
            _context.Update(race);
            return Save();
        }

        public bool Add(Race race)
        {
            //throw new NotImplementedException();
            _context.Add(race);
            return Save();
        }

        public bool Save()
        {

            var save = _context.SaveChanges();
            return save > 0;

        }
    }
}
