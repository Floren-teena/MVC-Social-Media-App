using dotnetcoremorningclass.Models;

namespace dotnetcoremorningclass.Interfaces
{
    public interface IDashBoardRepository
    {

        Task<List<Race>> GetAllUserRacesAsync();
        Task<List<Club>> GetAllUserClubsAsync();

    }
}
