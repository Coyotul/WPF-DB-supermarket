using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

public class ProducatorService
{
    private string connectionString;

    public ProducatorService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unui producător nou
    public void AdaugaProducator(string numeProducator, string taraOrigine)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Producatori (NumeProducator, TaraOrigine) VALUES (@NumeProducator, @TaraOrigine)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeProducator", numeProducator);
            command.Parameters.AddWithValue("@TaraOrigine", taraOrigine);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru actualizarea unui producător existent
    public void ActualizeazaProducator(int id, string numeProducator, string taraOrigine)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Producatori SET NumeProducator = @NumeProducator, TaraOrigine = @TaraOrigine WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@NumeProducator", numeProducator);
            command.Parameters.AddWithValue("@TaraOrigine", taraOrigine);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru ștergerea unui producător existent
    public void StergeProducator(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Producatori WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

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
}

public class Producator
{
    public int ID { get; set; }
    public string NumeProducator { get; set; }
    public string TaraOrigine { get; set; }
}
