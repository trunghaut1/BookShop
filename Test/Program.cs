using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Model;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AdminRepository aa = new AdminRepository();
            Admin a = aa.SelectByID(1);
            Admin aaa = new Admin() { ID = 1, Name = "" };
            //a = aaa;
            int i = aa.Update(a);
            Console.ReadLine();
        }
    }
}
