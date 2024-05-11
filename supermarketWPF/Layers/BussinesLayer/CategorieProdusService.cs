using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class CategorieProdusService
{
    private string connectionString;

    public CategorieProdusService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unei categorii de produse noi
    public void AdaugaCategorieProdus(string numeCategorie)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO CategoriiProduse (NumeCategorie) VALUES (@NumeCategorie)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeCategorie", numeCategorie);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru ștergerea unei categorii de produse
    public void StergeCategorieProdus(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM CategoriiProduse WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

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
}

public class CategorieProdus
{
    public int ID { get; set; }
    public string NumeCategorie { get; set; }
}
