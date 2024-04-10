using dotnetcoremorningclass.Models;

namespace dotnetcoremorningclass.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();

        Task<Race> GetByIdAsync(int id);

        bool Update(Race race);

        bool Add(Race race);

        bool Save();
    }

}
