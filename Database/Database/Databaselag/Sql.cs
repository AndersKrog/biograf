using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace H1CaseSQLTableOrDataReader.Databaselag
{
    /// <summary>
    /// ConnectionString indeholder navn på SQL Server, Databasenavn samt sikkerhedsoplysninger, f.eks. hvordan logges på m.m.
    /// Her er der refereret til lokalhost i stedet for til en navngiven server. 
    /// private static string ConnectionString = "Data Source=TEC-5350-LA0116;Initial Catalog=sqleksempler; Integrated...
    /// </summary>
    static class SQL
    {
        private static string ConnectionString = "Data Source=localhost;Initial Catalog=biograf; Integrated Security=SSPI;Connect Timeout=5;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static bool SqlConnectionOK()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        // Create, Read, Update og Delete (CRUD)

        //1) Create, Data der skal creates i en tabel (det hedder insert på sql'sk)
        public static void insert(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }

        //2) Read, Data der skal læses fra en tabel (det hedder select på sql'sk)

        //2a) DataAdapter og DataTable, returnere DataTable
        public static DataTable ReadTable(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                DataTable records = new DataTable();

                //Create new DataAdapter
                using (SqlDataAdapter a = new SqlDataAdapter(sql, con))
                {
                    //Use DataAdapter to fill DataTable records
                    con.Open();
                    a.Fill(records);
                }

                return records;
            }
        }

        //2b) Read med Datareader, udskriver med Console.WriteLine()
        public static void DataReader()
        {
            Console.WriteLine("DataReader");
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Kunder", con);

                SqlDataReader reader = cmd.ExecuteReader();
                //Er der rækker?
                Console.WriteLine(reader.HasRows);

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string navn = reader.GetString(1);
                    string adr = reader.GetString(3);
                    int alder = reader.GetInt32(4);

                    Console.WriteLine($"Id: {id} navn: {navn} adresse: {adr} - alder: {alder}");
                }

            }
        }
        public static void DeleteData(string sqldel)
        {
            // der skal være en form for tjek, om det ønskede opslag findes

            Console.WriteLine("DeleteData");
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqldel, con);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
