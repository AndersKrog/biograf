using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1CaseSQLTableOrDataReader.Databaselag;
using H1CaseSQLTableOrDataReader.Model;

namespace H1CaseSQLTableOrDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (SQL.SqlConnectionOK())
            {
                Console.WriteLine("Der er forbindelse til databasen");
            }
            else
            {
                Console.WriteLine("Der er ikke forbindelse til databasen");
            }

            //Opretter ny kunde i main og kalder metode, så kunden bliver oprettes i databasen

            // Kunde k1 = new Kunde("TEST", "Zperson", "normal", "Nattestien 47", 30, 25555564);
            // k1.InsertIntoDB();

            // opdaterer en kunde med angivet id i angivet felt med angivet værdi:
            // Kunde.UpdateInDB(1, "fornavn", "Kent");


            //Læser alle kunder i tabellen Kunder og danner liste vha. datatabel
            List<Kunde> alleKunder = Kunde.DanKundeListe();

            //som udskrives her
            foreach (var item in alleKunder)
            {
                Console.WriteLine(item.Fornavn + " " + item.Adresse + " Alder: " + item.Alder + " ID: " + item.Kundeid);
            }

            // sorterer listen efter kundernes efternavn
            // alleKunder.Sort();

            // sletter en kunde og kundens ordre i databasen
            //Kunde.DeleteInDB(50);

            //opretter liste over en kundes ordre, parameter er kundens ordrenummer
            
            List<Ordre> ordreliste = Kunde.DanOrdreListe(1);
            foreach (var item in ordreliste)
            {
                Console.WriteLine(item.Ordreid + " " + item.Kundeid + " Spilletidspunkt: " + item.SpilleTidspunkt);
            }
            
            //SQL.DataReader();

            Console.ReadKey();
        }
    }
}
