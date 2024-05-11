using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class ProducatorDAL
{
    private string connectionString;

    public ProducatorDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unui producător în baza de date
    public void AdaugaProducator(Producator producator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Producatori (NumeProducator, TaraOrigine) VALUES (@NumeProducator, @TaraOrigine)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeProducator", producator.NumeProducator);
            command.Parameters.AddWithValue("@TaraOrigine", producator.TaraOrigine);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru obținerea tuturor producătorilor din baza de date
    public List<Producator> GetProducatori()
    {
        List<Producator> producatori = new List<Producator>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Producatori";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Producator producator = new Producator();
                producator.ID = Convert.ToInt32(reader["ID"]);
                producator.NumeProducator = reader["NumeProducator"].ToString();
                producator.TaraOrigine = reader["TaraOrigine"].ToString();

                producatori.Add(producator);
            }
        }

        return producatori;
    }

    // Metodă pentru actualizarea unui producător în baza de date
    public void ActualizeazaProducator(Producator producator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Producatori SET NumeProducator = @NumeProducator, TaraOrigine = @TaraOrigine WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeProducator", producator.NumeProducator);
            command.Parameters.AddWithValue("@TaraOrigine", producator.TaraOrigine);
            command.Parameters.AddWithValue("@ID", producator.ID);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru ștergerea unui producător din baza de date
    public void StergeProducator(int idProducator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Producatori WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", idProducator);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
