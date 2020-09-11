using System;
using System.Collections.Generic;
using System.Data;
using Biograf.Databaselag;


namespace Biograf.Model
{
    class Ordre
    {
        public int Ordreid { get; set; }
        public DateTime SpilleTidspunkt { get; set; }
        public int Pris { get; set; }
        public int Kundeid { get; set; }
        public int Filmid { get; set; }
        public int Billetantal { get; set; }
        public bool Betalt { get; set; }

        // default contructor
        public Ordre()
        { }

        public Ordre(DateTime spiltidsp, int pris, int kundeid, int fid, int biantl, string betalt)
        {
            SpilleTidspunkt = spiltidsp;
            Pris = pris;
            Kundeid = kundeid;
            Filmid = fid;
            Billetantal = biantl;

            if (betalt == "ja")
                Betalt = true;
            else
                Betalt = false;
        }

        // det samme som med kundeopdatering
        // skal overloades til at tage både int og datetime
        public static void UpdateInDB(int ordrid, string updatefelt, int updatevalue)
        {
            string sql = $"UPDATE ordre SET {updatefelt} = '" + updatevalue + "' WHERE ordreid='" + ordrid + "'";
            try
            {
                SQL.Update(sql);
                Console.WriteLine($"ordren med ordreid {ordrid} er opdateret i tabellen");
            }
            catch (Exception)
            {
                Console.WriteLine("Der opstod en fejl, ordren er IKKE opdateret");
            }
        }
        // overload med Datetime
        public static void UpdateInDB(int ordrid, string updatefelt, DateTime updatevalue)
        {
            string sql = $"UPDATE ordre SET {updatefelt} = '" + updatevalue + "' WHERE ordreid='" + ordrid + "'";
            try
            {
                SQL.Update(sql);
                Console.WriteLine($"ordren med ordreid {ordrid} er opdateret i tabellen");
            }
            catch (Exception)
            {
                Console.WriteLine("Der opstod en fejl, ordren er IKKE opdateret");
            }
        }
        // ligesom brugeren, man laver et ordreobjekt som overføres til databasen
        public void InsertIntoDB()
        {
            string sql = "insert into ordre values ('" + SpilleTidspunkt + "','" + Pris + "','" + Kundeid + "','" + Filmid + "'," + Billetantal + ", " + Betalt + ")";
            try
            {
                SQL.insert(sql);
                Console.WriteLine($"Ordren med {Ordreid} oprettet på tabellen");
            }
            catch (Exception)
            {
                Console.WriteLine("Der opstod en fejl i oprettelsen, kunden IKKE oprettet");
            }
        }
        public static void DeleteInDB(int ordreid)
        {
            string sql = "DELETE FROM ordre WHERE ordreid='" + ordreid + "'";
            try
            {
                SQL.DeleteData(sql);
                Console.WriteLine($"ordren med kundeid {ordreid} er slettet fra tabellen");
            }
            catch (Exception)
            {   // virker ikke
                Console.WriteLine("Der opstod en fejl, ordren er IKKE slettet");
            }
        }

    }
}
