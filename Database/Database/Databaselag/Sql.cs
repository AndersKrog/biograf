using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Biograf.Databaselag
{
    /// <summary>
    /// ConnectionString indeholder navn på SQL Server, Databasenavn samt sikkerhedsoplysninger, f.eks. hvordan logges på m.m.
    /// Her er der refereret til lokalhost i stedet for til en navngiven server. 
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
        // 1 Create, 2 Read, 3 Update og 4 Delete (CRUD)

        //1) Create, Data der skal creates i en tabel (insert)
        public static void insert(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }

        //2) Read, Data der skal læses fra en tabel (select)

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
        
        //3) Update
        public static void Update(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }

        //4) Delete
        public static void DeleteData(string sqldel)
        {
            // der skal være en form for tjek, om det ønskede opslag findes

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqldel, con);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
