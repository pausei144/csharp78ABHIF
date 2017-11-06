using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSheet_03_Artikel_Lieferant
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("initialized");
            DAL dal = new DAL();
            dal.ErstelleArtTabelle("Artikel");
            dal.ErstelleLieferantenTabelle("Lieferant");
            dal.ErstelleRelTabelle("Relation");
        }
    }
}
