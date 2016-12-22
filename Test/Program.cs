using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;
using System.Net;
using Repository.ClientRepository;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            t();
        }
        private static async void t()
        {
            BookRepository db = new BookRepository();
            bool r = await db.CheckConnect();
        }
    }
}
