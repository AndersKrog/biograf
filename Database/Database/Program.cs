using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biograf.Databaselag;
using Biograf.Model;
using Biograf.Ui;

namespace Biograf
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.Onstart();

            do
            {
                Menu.menuinput();
            } while (Menu.Exit == false);

            // Console.ReadKey();
        }
    }
}
