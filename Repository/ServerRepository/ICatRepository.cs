using Repository.Model;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public interface ICatRepository : IGenericRepository<Cat>
    {
        IEnumerable<Cat> GetByBook(int id);
    }
}
