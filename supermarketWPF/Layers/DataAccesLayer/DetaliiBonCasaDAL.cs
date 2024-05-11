using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class DetaliiBonCasaDAL
{
    private string connectionString;

    public DetaliiBonCasaDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public DetaliiBonCasa GetDetaliiBonCasa(int idBon)
    {
        DetaliiBonCasa detaliiBonCasa = new DetaliiBonCasa();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string queryBon = "SELECT * FROM BonuriCasa WHERE IDBon = @IDBon";
            SqlCommand commandBon = new SqlCommand(queryBon, connection);
            commandBon.Parameters.AddWithValue("@IDBon", idBon);

            connection.Open();
            SqlDataReader readerBon = commandBon.ExecuteReader();

            if (readerBon.Read())
            {
                detaliiBonCasa.IDBon = Convert.ToInt32(readerBon["IDBon"]);
                detaliiBonCasa.DataEliberare = Convert.ToDateTime(readerBon["DataEliberare"]);
                detaliiBonCasa.Casier = readerBon["Casier"].ToString();
                detaliiBonCasa.SumaIncasata = Convert.ToDecimal(readerBon["SumaIncasata"]);
            }

            string queryProduse = "SELECT * FROM ProduseVandute WHERE IDBon = @IDBon";
            SqlCommand commandProduse = new SqlCommand(queryProduse, connection);
            commandProduse.Parameters.AddWithValue("@IDBon", idBon);

            SqlDataReader readerProduse = commandProduse.ExecuteReader();

            detaliiBonCasa.ProduseVandute = new List<ProdusVandut>();

            while (readerProduse.Read())
            {
                ProdusVandut produsVandut = new ProdusVandut();
                produsVandut.NumeProdus = readerProduse["NumeProdus"].ToString();
                produsVandut.Cantitate = Convert.ToInt32(readerProduse["Cantitate"]);
                produsVandut.PretUnitar = Convert.ToDecimal(readerProduse["PretUnitar"]);
                produsVandut.Subtotal = Convert.ToDecimal(readerProduse["Subtotal"]);

                detaliiBonCasa.ProduseVandute.Add(produsVandut);
            }
        }

        return detaliiBonCasa;
    }
}
