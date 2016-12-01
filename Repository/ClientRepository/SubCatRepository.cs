using Repository.Model;

namespace Repository.ClientRepository
{
    public class SubCatRepository : GenericRepository<SubCat>
    {
        public SubCatRepository()
        {
            url = "subcat";
        }
    }
}
