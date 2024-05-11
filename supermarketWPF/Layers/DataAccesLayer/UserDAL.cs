using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class UserDAL
{
    private string connectionString;

    public UserDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void AdaugaUtilizator(Utilizator utilizator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Utilizatori (NumeUtilizator, Parola, TipUtilizator) VALUES (@NumeUtilizator, @Parola, @TipUtilizator)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeUtilizator", utilizator.NumeUtilizator);
            command.Parameters.AddWithValue("@Parola", utilizator.Parola);
            command.Parameters.AddWithValue("@TipUtilizator", utilizator.TipUtilizator);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

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

    public void ActualizeazaUtilizator(Utilizator utilizator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Utilizatori SET NumeUtilizator = @NumeUtilizator, Parola = @Parola, TipUtilizator = @TipUtilizator WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeUtilizator", utilizator.NumeUtilizator);
            command.Parameters.AddWithValue("@Parola", utilizator.Parola);
            command.Parameters.AddWithValue("@TipUtilizator", utilizator.TipUtilizator);
            command.Parameters.AddWithValue("@ID", utilizator.ID);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void StergeUtilizator(int idUtilizator)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Utilizatori WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", idUtilizator);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
