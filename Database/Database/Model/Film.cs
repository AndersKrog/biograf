using System;
using System.Collections.Generic;
using System.Data;
using Biograf.Databaselag;

namespace Biograf.Model
{
    class Film
    {
        public int Filmid { get; set; }
        public string Filmtitel { get; set; }
        public int Varighed { get; set; }
        public int Pris { get; set; }


        public static List<Film> DanFilmListe()
        {
            string sql = "Select * from film";
            DataTable filmDataTable = SQL.ReadTable(sql);

            List<Film> listFilm = new List<Film>();
            foreach (DataRow FilmData in filmDataTable.Rows)
            {
                listFilm.Add(new Film()
                {
                    Filmid = Convert.ToInt32(FilmData["filmid"]),
                    Filmtitel = FilmData["titel"].ToString(),
                    Varighed = Convert.ToInt32(FilmData["varighed"]),
                    Pris = Convert.ToInt32(FilmData["pris"])

                });
            }
            return listFilm;
        }
    }
}