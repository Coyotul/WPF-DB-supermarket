using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

public class ProdusService
{
    private string connectionString;

    public ProdusService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unui produs nou
    public void AdaugaProdus(string numeProdus, string codDeBare, string categorie, string producator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Produse (NumeProdus, CodDeBare, Categorie, Producator, DataIntrarii) VALUES (@NumeProdus, @CodDeBare, @Categorie, @Producator, GETDATE())";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeProdus", numeProdus);
            command.Parameters.AddWithValue("@CodDeBare", codDeBare);
            command.Parameters.AddWithValue("@Categorie", categorie);
            command.Parameters.AddWithValue("@Producator", producator);
            command.Parameters.AddWithValue("@DataIntrarii", DateTime.Now);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru actualizarea unui produs existent
    public void ActualizeazaProdus(int id, string numeProdus, string codDeBare, string categorie, string producator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Produse SET NumeProdus = @NumeProdus, CodDeBare = @CodDeBare, Categorie = @Categorie, Producator = @Producator WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@NumeProdus", numeProdus);
            command.Parameters.AddWithValue("@CodDeBare", codDeBare);
            command.Parameters.AddWithValue("@Categorie", categorie);
            command.Parameters.AddWithValue("@Producator", producator);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru ștergerea unui produs existent
    public void StergeProdus(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Produse WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru obținerea tuturor produselor din baza de date
    public List<Produs> GetProduse()
    {
        List<Produs> produse = new List<Produs>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT p.*, sp.PretAchizitie, sp.PretVanzare FROM Produse p JOIN StocuriProduse sp ON p.ID = sp.IDProdus";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Produs produs = new Produs();
                produs.ID = Convert.ToInt32(reader["ID"]);
                produs.NumeProdus = reader["NumeProdus"].ToString();
                produs.CodDeBare = reader["CodDeBare"].ToString();
                produs.Categorie = reader["Categorie"].ToString();
                produs.Producator = reader["Producator"].ToString();
                produs.DataIntrarii = Convert.ToDateTime(reader["DataIntrarii"]);
                produs.Pret = (float)Convert.ToDecimal(reader["PretVanzare"]);
                produse.Add(produs);
            }
        }

        return produse;
    }
}


public class Produs
{
    public int ID { get; set; }
    public string NumeProdus { get; set; }
    public string CodDeBare { get; set; }
    public string Categorie { get; set; }
    public string Producator { get; set; }
    public DateTime DataIntrarii { get; set; }
    public float Pret { get; set; }
    public Produs(int iD, string numeProdus, string codDeBare, string categorie, string producator, DateTime dataIntrarii, float pret)
    {
        ID = iD;
        NumeProdus = numeProdus;
        CodDeBare = codDeBare;
        Categorie = categorie;
        Producator = producator;
        DataIntrarii = dataIntrarii;
        Pret = pret;
    }
    public Produs()
    {
    }
}

