using System;
using System.Collections.Generic;
using Biograf.Databaselag;
using Biograf.Model;

namespace Biograf.Ui
{
    static class Menu
    {
        public static bool Exit { get; set; }

        private static string[] kundeFelter = new string[] {"kundeid","fornavn","efternavn","kundetype","adresse","alder","telefon" };
        private static string[] ordreFelter = new string[] {"ordreid", "spilletidspunkt","pris","kundeid","filmid","billetantal","betalt"};

        public static void Onstart()
        {
            // ting der køres i starten. tests osv
            Exit = false;

            if (SQL.SqlConnectionOK())
            {
                Console.WriteLine("Der er forbindelse til databasen");
            }
            else
            {
                Console.WriteLine("Der er ikke forbindelse til databasen");
            }
        }

        public static void menuinput()
        {
            Console.CursorVisible = false;
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
                "Slet ordre                                 ",
                "List kundes ordre                          " };

            Action[] menuhandlinger = new Action[] {
            Menu.opretKunde,
            Menu.opretKunde,
            Menu.sletKunde,
            Menu.printKundeListe,
            Menu.printKundeListeSort,
            Menu.opretOrdre,
            Menu.opdaterOrdre,
            Menu.sletKunde,
            Menu.opretKundesOrdreListe,
            };

        int valg = 0;

            Console.Clear();

            Console.SetCursorPosition(5, 3);
            Console.WriteLine("valg: piletaster + enter,  escape = afslut    ");

            do
            {

                for (int i = 0; i < menuemner.Length;i++)
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
                    case ConsoleKey.Escape:
                        Exit = true;
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

            } while (input.Key != ConsoleKey.Enter && input.Key != ConsoleKey.Escape);

            Console.Clear();

            if (Exit != true)
            {
                Console.CursorVisible = true;
                menuhandlinger[valg]();
                Console.WriteLine("tryk en tast for at vende tilbage til menuen");
                Console.ReadKey(true);

            }
        }
        public static void opretKunde()
        {
            // "TEST", "Zperson", "normal", "Nattestien 47", 30, 25555564)
            string[] input = new string[7];

            for (int i = 0; i < ordreFelter.Length -1; i++)
            {
                Console.WriteLine("Indtast :" + kundeFelter[i+1]);

                input[i] = Console.ReadLine();
            }

            Kunde k1 = new Kunde(input[0], input[1], input[2], input[3], Convert.ToInt32(input[4]), Convert.ToInt32(input[5]));

            k1.InsertIntoDB();

        }
        public static void opdaterKunde()
        {
            // opdaterer en kunde med angivet id i angivet felt med angivet værdi:
            // Kunde.UpdateInDB(1, "fornavn", "Kent");

            Console.WriteLine("Indtast kundeid på ordren, som du ønsker at opdatere");
            int kundeid = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < kundeFelter.Length; i++)
            {
                Console.WriteLine("feltnummer" + " " + i + " " + kundeFelter[i]);
            }
            Console.WriteLine("Indtast feltnummer");
            int feltnr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indtast den værdi du ønsker at indsætte");
            string stringvalue = Console.ReadLine();
            int intvalue;

            if (kundeid == 0 || kundeid == 5 || kundeid == 6)
            {
                intvalue = Convert.ToInt32(stringvalue);
                Kunde.UpdateInDB(kundeid, kundeFelter[feltnr], intvalue);
            }
            else
            {
                Kunde.UpdateInDB(kundeid, ordreFelter[feltnr],stringvalue);
            }
        }
        public static void sletKunde()
        {
            // mangler inputtjek og tjek om handlingen sker

            Console.WriteLine("Indtast kundeid på den kunde som du ønsker at slette");
            int kundeid = Convert.ToInt32(Console.ReadLine());
            // sletter en kunde og kundens ordre i databasen
            Kunde.DeleteInDB(kundeid);
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
                Console.WriteLine(item.Fornavn + " " + item.Efternavn + " " + item.Adresse + " Alder: " + item.Alder + " ID: " + item.Kundeid);
            }
        }
        public static void opretOrdre()
        {
            // mangler inputtjek og tjek om handlingen sker
            // det burde kun være muligt at vælge filmtitler der allerede er oprettet osv.

            string[] input = new string[7];

            List<Film> filmliste = Film.DanFilmListe();

            foreach (var item in filmliste)
            {
                Console.WriteLine($"{item.Filmid} {item.Filmtitel} {item.Varighed} {item.Pris}");
            }
            Console.WriteLine();

            for (int i = 0; i < ordreFelter.Length - 1; i++)
            {
                Console.WriteLine("Indtast :" + ordreFelter[i + 1]);

                input[i] = Console.ReadLine();
            }

            Ordre O1 = new Ordre(Convert.ToDateTime(input[0]), Convert.ToInt32(input[1]), Convert.ToInt32(input[2]), Convert.ToInt32(input[3]), Convert.ToInt32(input[4]), input[5]);

            O1.InsertIntoDB();

        }
        public static void opdaterOrdre()
        {
            Console.WriteLine("Indtast ordreid på ordren, som du ønsker at opdatere");
            int ordreid = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < ordreFelter.Length; i++)
            {
                Console.WriteLine("feltnummer" +" " +i +" "+ ordreFelter[i]);
            }
            Console.WriteLine("Indtast feltnummer");
            int feltnr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indtast den værdi du ønsker at indsætte");
            int value = Convert.ToInt32(Console.ReadLine());

            // value skal så konverteres afhængigt af om det er int eller eller datetime
            // der skal muligvis foretages en konvertering af datetime så formattet passer.

            // opdaterer en ordren med angivet id i angivet felt med angivet værdi:
            if (ordreid != 1)
                Ordre.UpdateInDB(ordreid, ordreFelter[feltnr], value);
            else
                Ordre.UpdateInDB(ordreid, ordreFelter[feltnr], Convert.ToDateTime(value));
        }
        public static void sletOrdre()
        {
            // mangler inputtjek og tjek om handlingen sker

            Console.WriteLine("Indtast ordreid på ordren, som du ønsker at slette");
            int ordreid = Convert.ToInt32(Console.ReadLine());
            // sletter en kunde og kundens ordre i databasen
            Ordre.DeleteInDB(ordreid);
        }
        public static void opretKundesOrdreListe()
        {
            // mangler inputtjek og tjek om handlingen sker

            // opretter liste over en kundes ordre, parameter er kundens ordrenummer

            Console.WriteLine("Indtast kundeid på den kunde, hvis orde du ønsker listet");
            int kundeid = Convert.ToInt32(Console.ReadLine());

            List<Ordre> ordreliste = Kunde.DanOrdreListe(kundeid);
            foreach (var item in ordreliste)
            {
                Console.WriteLine(item.Ordreid + " " + item.Kundeid + " Spilletidspunkt: " + item.SpilleTidspunkt);
            }
        }
    }
}