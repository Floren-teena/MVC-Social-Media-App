using dotnetcoremorningclass.Data;
using dotnetcoremorningclass.Interfaces;
using dotnetcoremorningclass.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoremorningclass.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly AppDbContext _context;
        //dependency injection and repo pattern(following clean architechture)//
        //becus you are not meant to put your business logic and database communication in the same file or the controller file
        
        //pending,succesful or fulfilled,fail(all under the promise when working with asynchronous)

        //this is also done to make testing easy

       
        public ClubRepository(AppDbContext context)
        {
            _context = context;
        }

        //whenever u r querying data from database make user ur methods are asynchronous
        public async Task<IEnumerable<Club>> GetAll()
        {
           // throw new NotImplementedException();
           return await _context.clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.clubs.Include(a => a.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Update(Club club)
        {
            //throw new NotImplementedException();
            _context.Update(club);
            return Save();
        }

        public bool Add(Club club)
        {
            //throw new NotImplementedException();
            _context.Add(club);
            return Save();
        }

        public bool Save()
        {
          
          var save = _context.SaveChanges();
          return save > 0;

        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }
    }
}
