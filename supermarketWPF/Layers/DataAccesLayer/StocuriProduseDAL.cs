using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class StocProdusDAL
{
    private string connectionString;

    public StocProdusDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unui stoc de produs în baza de date
    public void AdaugaStocProdus(StocProdus stocProdus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO StocuriProduse (IDProdus, Cantitate, UnitateMasura, DataAprovizionare, DataExpirare, PretAchizitie, PretVanzare) VALUES (@IDProdus, @Cantitate, @UnitateMasura, @DataAprovizionare, @DataExpirare, @PretAchizitie, @PretVanzare)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IDProdus", stocProdus.IDProdus);
            command.Parameters.AddWithValue("@Cantitate", stocProdus.Cantitate);
            command.Parameters.AddWithValue("@UnitateMasura", stocProdus.UnitateMasura);
            command.Parameters.AddWithValue("@DataAprovizionare", stocProdus.DataAprovizionare);
            command.Parameters.AddWithValue("@DataExpirare", stocProdus.DataExpirare);
            command.Parameters.AddWithValue("@PretAchizitie", stocProdus.PretAchizitie);
            command.Parameters.AddWithValue("@PretVanzare", stocProdus.PretVanzare);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru obținerea tuturor stocurilor de produse din baza de date
    public List<StocProdus> GetStocuriProduse()
    {
        List<StocProdus> stocuriProduse = new List<StocProdus>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM StocuriProduse";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                StocProdus stocProdus = new StocProdus();
                stocProdus.ID = Convert.ToInt32(reader["ID"]);
                stocProdus.IDProdus = Convert.ToInt32(reader["IDProdus"]);
                stocProdus.Cantitate = Convert.ToInt32(reader["Cantitate"]);
                stocProdus.UnitateMasura = reader["UnitateMasura"].ToString();
                stocProdus.DataAprovizionare = Convert.ToDateTime(reader["DataAprovizionare"]);
                stocProdus.DataExpirare = Convert.ToDateTime(reader["DataExpirare"]);
                stocProdus.PretAchizitie = Convert.ToDecimal(reader["PretAchizitie"]);
                stocProdus.PretVanzare = Convert.ToDecimal(reader["PretVanzare"]);

                stocuriProduse.Add(stocProdus);
            }
        }

        return stocuriProduse;
    }

    // Metodă pentru actualizarea unui stoc de produs în baza de date
    public void ActualizeazaStocProdus(StocProdus stocProdus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE StocuriProduse SET Cantitate = @Cantitate, UnitateMasura = @UnitateMasura, DataAprovizionare = @DataAprovizionare, DataExpirare = @DataExpirare, PretAchizitie = @PretAchizitie, PretVanzare = @PretVanzare WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Cantitate", stocProdus.Cantitate);
            command.Parameters.AddWithValue("@UnitateMasura", stocProdus.UnitateMasura);
            command.Parameters.AddWithValue("@DataAprovizionare", stocProdus.DataAprovizionare);
            command.Parameters.AddWithValue("@DataExpirare", stocProdus.DataExpirare);
            command.Parameters.AddWithValue("@PretAchizitie", stocProdus.PretAchizitie);
            command.Parameters.AddWithValue("@PretVanzare", stocProdus.PretVanzare);
            command.Parameters.AddWithValue("@ID", stocProdus.ID);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru ștergerea unui stoc de produs din baza de date
    public void StergeStocProdus(int idStocProdus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM StocuriProduse WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", idStocProdus);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
