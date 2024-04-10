using dotnetcoremorningclass.Models;

namespace dotnetcoremorningclass.Interfaces
{
    public interface IClubRepository
    {
        //bcus the method is going to be async,we are going to be using the task keyword
        //ASYNC operationns suppports concurency//or lets say u are performing a tsk asynchronously or concurently
        Task<IEnumerable<Club>> GetAll();

        Task<Club> GetByIdAsync(int id);

        bool Update(Club club);

        bool Add(Club club);

        bool Save();

        bool Delete(Club club);


    }
}
