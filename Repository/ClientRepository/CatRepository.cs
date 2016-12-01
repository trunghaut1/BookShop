using Repository.Model;

namespace Repository.ClientRepository
{
    public class CatRepository : GenericRepository<Cat>
    {
        public CatRepository()
        {
            url = "cat";
        }
    }
}
