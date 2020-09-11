using System;
using System.Collections.Generic;
using System.Data;
using Biograf.Databaselag;


namespace Biograf.Model
{
    class Kunde : IComparable<Kunde>
    {
        public string Kundetype { get; set; }
        public int Kundeid { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Adresse { get; set; }
        public int Alder { get; set; }
        public int Telefon { get; set; }

        public Kunde() { } //default constructor

        public Kunde(string nv, string efnv, string kundetp, string adr, int alder, int telefon)
        {
            Fornavn = nv;
            Efternavn = efnv;
            Kundetype = kundetp;
            Adresse = adr;
            Alder = alder;
            Telefon = telefon;
        }

        public static void UpdateInDB(int kundeid, string updatefelt, string updatevalue)
        {
            string sql = $"UPDATE Kunder SET {updatefelt} = '"+updatevalue + "' WHERE kundeid='"+kundeid +"'";
            try
            {
                SQL.Update(sql);
                Console.WriteLine($"Kunden med kundeid {kundeid} er opdateret i tabellen");
            }
            catch (Exception)
            {
                Console.WriteLine("Der opstod en fejl, kunden er IKKE opdateret");
            }
        }
        // overload med int
        public static void UpdateInDB(int kundeid, string updatefelt, int updatevalue)
        {
            string sql = $"UPDATE Kunder SET {updatefelt} = '" + updatevalue + "' WHERE kundeid='" + kundeid + "'";
            try
            {
                SQL.Update(sql);
                Console.WriteLine($"Kunden med kundeid {kundeid} er opdateret i tabellen");
            }
            catch (Exception)
            {
                Console.WriteLine("Der opstod en fejl, kunden er IKKE opdateret");
            }
        }

        public static void DeleteInDB(int kundeid)
        {
            string sql = "DELETE FROM Kunder WHERE kundeid='" + kundeid + "'";
            try
            {
                SQL.DeleteData(sql);
                Console.WriteLine($"Kunden med kundeid {kundeid} er slettet fra tabellen");

                // slet tilhørende ordrer
                sql = "DELETE FROM ordre WHERE kundeid='" + kundeid + "'";
                SQL.DeleteData(sql);
            }
            catch (Exception)
            {   // virker ikke
                Console.WriteLine("Der opstod en fejl, kunden er IKKE slettet");
            }
        }
        public void InsertIntoDB()
        {
            string sql = "insert into Kunder values ('" + Fornavn + "','" + Efternavn + "','" + Kundetype + "','" + Adresse + "'," + Alder + ", " + Telefon + ")";
            try
            {
                SQL.insert(sql);
                Console.WriteLine($"Kunden {Fornavn} {Efternavn} oprettet på tabellen");
            }
            catch (Exception)
            {
                Console.WriteLine("Der opstod en fejl i oprettelsen, kunden IKKE oprettet");
            }
        }

        public static List<Kunde> DanKundeListe()
        {
            string sql = "Select * from Kunder";
            DataTable kundeDataTable = SQL.ReadTable(sql);

            List<Kunde> listKunder = new List<Kunde>();
            foreach (DataRow kundeData in kundeDataTable.Rows)
            {
                listKunder.Add(new Kunde()
                {
                    Kundeid = Convert.ToInt32(kundeData["kundeid"]),
                    Fornavn = kundeData["fornavn"].ToString(),
                    Efternavn = kundeData["efternavn"].ToString(),
                    Kundetype = kundeData["kundetype"].ToString(),
                    Adresse = kundeData["adresse"].ToString(),
                    Alder = Convert.ToInt32(kundeData["alder"]),
                    Telefon = Convert.ToInt32(kundeData["telefon"])
                });
            }
            /*
            //En specifik rækker, her den første ellers kan [0] udskiftes med tal eller tæller
            string denførsterække = kundeDataTable.Rows[0]["navn"].ToString();
            Console.WriteLine("Den første række " + denførsterække + kundeDataTable.Rows.Count);
            */
            return listKunder;
        }

        public static List<Ordre> DanOrdreListe(int kundeid)
        {
            string sql = "SELECT * FROM kunder,ordre WHERE kunder.kundeid=Ordre.kundeid and kunder.kundeid='" + kundeid + "'";
            DataTable ordreDataTable = SQL.ReadTable(sql);

            List<Ordre> listOrdre = new List<Ordre>();
            foreach (DataRow OrdreData in ordreDataTable.Rows)
            {
                    listOrdre.Add(new Ordre()
                    {
                        Ordreid = Convert.ToInt32(OrdreData["kundeid"]),
                        SpilleTidspunkt = Convert.ToDateTime(OrdreData["spilletidspunkt"]),
                        Pris = Convert.ToInt32(OrdreData["pris"]),
                        Kundeid = Convert.ToInt32(OrdreData["kundeid"]),
                        Filmid = Convert.ToInt32(OrdreData["filmid"]),
                        Billetantal = Convert.ToInt32(OrdreData["billetantal"]),
                        Betalt = (Convert.ToString(OrdreData["betalt"]) == "ja" ? true : false)
                    }) ;
            }
            return listOrdre;
        }
            public int CompareTo(Kunde that)
            {
            int tal = string.Compare(this.Efternavn.ToUpper(), that.Efternavn.ToUpper());
                return tal;
            }
    }
}
