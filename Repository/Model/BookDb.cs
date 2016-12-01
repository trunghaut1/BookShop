using System.Data.Entity;

namespace Repository.Model
{
    public partial class BookEntities : DbContext
    {
        public BookEntities(string connectString)
            : base(connectString)
        {
        }
    }
}
