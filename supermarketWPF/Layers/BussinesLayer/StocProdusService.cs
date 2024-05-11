using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class StocProdusService
{
    private string connectionString;

    public StocProdusService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unui nou stoc de produs
    public void AdaugaStocProdus(int idProdus, int cantitate, string unitateMasura, DateTime dataAprovizionare, DateTime dataExpirare, decimal pretAchizitie, decimal pretVanzare)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO StocuriProduse (IDProdus, Cantitate, UnitateMasura, DataAprovizionare, DataExpirare, PretAchizitie, PretVanzare) VALUES (@IDProdus, @Cantitate, @UnitateMasura, @DataAprovizionare, @DataExpirare, @PretAchizitie, @PretVanzare)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IDProdus", idProdus);
            command.Parameters.AddWithValue("@Cantitate", cantitate);
            command.Parameters.AddWithValue("@UnitateMasura", unitateMasura);
            command.Parameters.AddWithValue("@DataAprovizionare", dataAprovizionare);
            command.Parameters.AddWithValue("@DataExpirare", dataExpirare);
            command.Parameters.AddWithValue("@PretAchizitie", pretAchizitie);
            command.Parameters.AddWithValue("@PretVanzare", pretVanzare);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru actualizarea unui stoc de produs existent
    public void ActualizeazaStocProdus(int id, int cantitate)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE StocuriProduse SET Cantitate = @Cantitate WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Cantitate", cantitate);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru ștergerea unui stoc de produs
    public void StergeStocProdus(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM StocuriProduse WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

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
}

public class StocProdus
{
    public int ID { get; set; }
    public int IDProdus { get; set; }
    public int Cantitate { get; set; }
    public string UnitateMasura { get; set; }
    public DateTime DataAprovizionare { get; set; }
    public DateTime DataExpirare { get; set; }
    public decimal PretAchizitie { get; set; }
    public decimal PretVanzare { get; set; }
}
