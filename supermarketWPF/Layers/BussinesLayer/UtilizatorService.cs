using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class UtilizatorService
{
    private string connectionString;

    public UtilizatorService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unui utilizator nou
    public void AdaugaUtilizator(string numeUtilizator, string parola, string tipUtilizator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Utilizatori (NumeUtilizator, Parola, TipUtilizator) VALUES (@NumeUtilizator, @Parola, @TipUtilizator)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeUtilizator", numeUtilizator);
            command.Parameters.AddWithValue("@Parola", parola);
            command.Parameters.AddWithValue("@TipUtilizator", tipUtilizator);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru actualizarea parolei unui utilizator
    public void ActualizeazaParola(int id, string parola)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Utilizatori SET Parola = @Parola WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Parola", parola);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru ștergerea unui utilizator
    public void StergeUtilizator(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Utilizatori WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru obținerea tuturor utilizatorilor din baza de date
    public List<Utilizator> GetUtilizatori()
    {
        List<Utilizator> utilizatori = new List<Utilizator>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Utilizatori";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Utilizator utilizator = new Utilizator();
                utilizator.ID = Convert.ToInt32(reader["ID"]);
                utilizator.NumeUtilizator = reader["NumeUtilizator"].ToString();
                utilizator.Parola = reader["Parola"].ToString();
                utilizator.TipUtilizator = reader["TipUtilizator"].ToString();

                utilizatori.Add(utilizator);
            }
        }

        return utilizatori;
    }
}

public class Utilizator
{
    public int ID { get; set; }
    public string NumeUtilizator { get; set; }
    public string Parola { get; set; }
    public string TipUtilizator { get; set; }
}
