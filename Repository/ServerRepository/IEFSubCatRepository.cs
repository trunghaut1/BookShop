using Repository.Model;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public interface IEFSubCatRepository : IEFGenericRepository<SubCat>
    {
        IEnumerable<SubCat> GetByCat(int id);
    }
}
