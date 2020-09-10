using System;
using System.Collections.Generic;
using Biograf.Databaselag;
using Biograf.Model;


namespace Biograf.Ui
{
    static class Menu
    {
        public static void Onstart()
        {
            // ting der køres i starten. tests osv.

            if (SQL.SqlConnectionOK())
            {
                Console.WriteLine("Der er forbindelse til databasen");
            }
            else
            {
                Console.WriteLine("Der er ikke forbindelse til databasen");
            }
        }

        public static int menuinput()
        {
            ConsoleKeyInfo input;

            input = default(ConsoleKeyInfo);

            string[] menuemner = new string[] {
                "Opret ny kunde                             ",
                "opdater kunde                              ",
                "slet kunde                                 ",
                "List alle kunder                           ",
                "List alle kunder (sorter efter efternavn)  ",
                
                "Opret ny ordre                             ",
                "opdater ordre                              ",
                "List kundes ordre                          " };


        int valg = 0;

            do
            {
                for(int i = 0; i < menuemner.Length;i++)
                {
                    Console.ForegroundColor = i == valg ? ConsoleColor.Red : ConsoleColor.Gray;
                    Console.BackgroundColor = i == valg ? ConsoleColor.Gray : ConsoleColor.Red;

                    Console.SetCursorPosition(5, 4 + i);
                    Console.Write(menuemner[i]);
                }
                do
                {
                } while (!Console.KeyAvailable);

                input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        valg = valg == 0 ? menuemner.Length -1 : valg - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        valg = valg == menuemner.Length - 1 ? 0 : valg + 1;
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

            } while (input.Key != ConsoleKey.Enter);
            return valg;
        }

        public static void printKundeListe()
        {
            //Læser alle kunder i tabellen Kunder og danner liste vha. datatabel
            List<Kunde> alleKunder = Kunde.DanKundeListe();

            //som udskrives her
            foreach (var item in alleKunder)
            {
                Console.WriteLine(item.Fornavn + " " + item.Adresse + " Alder: " + item.Alder + " ID: " + item.Kundeid);
            }
        }

        public static void printKundeListeSort()
        {
            //Læser alle kunder i tabellen Kunder og danner liste vha. datatabel
            List<Kunde> alleKunder = Kunde.DanKundeListe();
            // sorterer listen efter kundernes efternavn
            alleKunder.Sort();

            //som udskrives her
            foreach (var item in alleKunder)
            {
                Console.WriteLine(item.Fornavn + " " + item.Adresse + " Alder: " + item.Alder + " ID: " + item.Kundeid);
            }
        }


    }

    //Opretter ny kunde i main og kalder metode, så kunden bliver oprettes i databasen

    // Kunde k1 = new Kunde("TEST", "Zperson", "normal", "Nattestien 47", 30, 25555564);
    // k1.InsertIntoDB();

    // opdaterer en kunde med angivet id i angivet felt med angivet værdi:
    // Kunde.UpdateInDB(1, "fornavn", "Kent");

    // sletter en kunde og kundens ordre i databasen
    //Kunde.DeleteInDB(50);

    //opretter liste over en kundes ordre, parameter er kundens ordrenummer
    /*
    List<Ordre> ordreliste = Kunde.DanOrdreListe(1);
    foreach (var item in ordreliste)
    {
        Console.WriteLine(item.Ordreid + " " + item.Kundeid + " Spilletidspunkt: " + item.SpilleTidspunkt);
    }
    */
    //SQL.DataReader();



}

