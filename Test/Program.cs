using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BookEntities db = new BookEntities();
            Console.WriteLine(db.Cat.Find(1).Name);
            Console.ReadLine();
        }
    }
}
