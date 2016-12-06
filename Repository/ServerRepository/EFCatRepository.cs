using Repository.Model;

namespace Repository.ServerRepository
{
    public class EFCatRepository: EFGenericRepository<Cat>, ICatRepository
    {
        public EFCatRepository(BookEntities db) : base(db) { }
    }
}
