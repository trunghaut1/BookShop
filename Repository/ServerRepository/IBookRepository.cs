using Repository.Model;
using Repository.ViewModel;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IEnumerable<Book> GetByName(string name);
        ListPaging<Book> SearchPage(string name, int pageSize, int page);
        ListPaging<Book> GetPage(int pageSize, int page);
        IEnumerable<Book> tGetByNumber(int number);
        Book tGetByID(int id);
        IEnumerable<Book> tGetRelated(int id);
        ListPaging<Book> tGetByCatPage(int id, int pageSize, int page);
        ListPaging<Book> tGetBySubCatPage(int id, int pageSize, int page);
        ListPaging<Book> tGetPage(int pageSize, int page);
    }
}
