using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;
using System.Net;
using Repository.ServerRepository;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BookEntities _db = new BookEntities();
            EFBookRepository db = new EFBookRepository(_db);
            var n = db.tGetByNumber(4);
        }
    }
}
