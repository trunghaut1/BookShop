using Repository.Model;

namespace Repository.ServerRepository
{
    public class EFCatRepository: EFGenericRepository<Cat>, IEFCatRepository
    {
        public EFCatRepository(BookEntities db) : base(db) { }
    }
}
