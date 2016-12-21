using Repository.Model;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public interface ISubCatRepository : IGenericRepository<SubCat>
    {
        IEnumerable<SubCat> GetByBook(int id);
    }
}
