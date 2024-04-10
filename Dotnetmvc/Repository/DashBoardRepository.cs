using dotnetcoremorningclass.Data;
using dotnetcoremorningclass.Interfaces;
using dotnetcoremorningclass.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoremorningclass.Repository
{

    public class DashBoardRepository: IDashBoardRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        //what httpcntextaccessor does is that 
        public DashBoardRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Race>> GetAllUserRacesAsync()
        {
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var userRaces = _context.Races.Where(r => r.AppUser .Id == curUser);

            return await userRaces.ToListAsync();
        }

        public async Task<List<Club>> GetAllUserClubsAsync()
        {
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var userClubs = _context.clubs.Where(r => r.AppUser.Id == curUser);

            return await userClubs.ToListAsync();
            //throw new NotImplementedException();
        }
    }
}
