using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class CategorieProdusDAL
{
    private string connectionString;

    public CategorieProdusDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unei categorii de produse în baza de date
    public void AdaugaCategorieProdus(CategorieProdus categorieProdus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO CategoriiProduse (NumeCategorie) VALUES (@NumeCategorie)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeCategorie", categorieProdus.NumeCategorie);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru obținerea tuturor categoriilor de produse din baza de date
    public List<CategorieProdus> GetCategoriiProduse()
    {
        List<CategorieProdus> categoriiProduse = new List<CategorieProdus>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM CategoriiProduse";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CategorieProdus categorieProdus = new CategorieProdus();
                categorieProdus.ID = Convert.ToInt32(reader["ID"]);
                categorieProdus.NumeCategorie = reader["NumeCategorie"].ToString();

                categoriiProduse.Add(categorieProdus);
            }
        }

        return categoriiProduse;
    }

    // Metodă pentru actualizarea unei categorii de produse în baza de date
    public void ActualizeazaCategorieProdus(CategorieProdus categorieProdus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE CategoriiProduse SET NumeCategorie = @NumeCategorie WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeCategorie", categorieProdus.NumeCategorie);
            command.Parameters.AddWithValue("@ID", categorieProdus.ID);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru ștergerea unei categorii de produse din baza de date
    public void StergeCategorieProdus(int idCategorieProdus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM CategoriiProduse WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", idCategorieProdus);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
