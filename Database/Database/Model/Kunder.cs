using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1CaseSQLTableOrDataReader.Databaselag;

namespace H1CaseSQLTableOrDataReader.Model
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
        // det ville give mening at implementere betalt som en boolsk værdi
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
        public static void UpdateInDB(int ordrid, string updatefelt, string updatevalue)
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
        // ligesom brugeren man laver et ordreobjekt som overføres til databasen
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
    }
        class Film
    { 
        public int Filmid { get; set; }
        public string Filmtitel { get; set; }
        public int Varighed { get; set; }
        public int Pris { get; set; }

    }
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
                       // Betalt = (Convert.ToString(OrdreData["betalt"]) == "ja" ? true : false)
                    }) ;
            }
            return listOrdre;
        }
            public int CompareTo(Kunde that)
            {
                // troede at det var nemmere

                if ((string.Compare(this.Efternavn, that.Efternavn)) > (string.Compare(that.Efternavn, this.Efternavn)))
                {
                    return -1;
                }
                else if ((string.Compare(this.Efternavn, that.Efternavn)) < (string.Compare(that.Efternavn, this.Efternavn)))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
    }
}
