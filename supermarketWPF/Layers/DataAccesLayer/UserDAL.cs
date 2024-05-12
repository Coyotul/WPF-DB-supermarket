using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using supermarketWPF.Layers.DataAccesLayer;
public class UserDAL
{
    private string connectionString;
    List<Utilizator> utilizatori;

    public UserDAL(string connectionString)
    {
        this.connectionString = connectionString;
        utilizatori = new List<Utilizator>();
    }

    public void AdaugaUtilizator(Utilizator utilizator)
    {
        try
        {
            using (SqlConnection connection = DALHelper.Connection)
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show("Produsul nu a putut fi adăugat în baza de date!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public List<Utilizator> GetUtilizatori()
    {
        if (utilizatori.Count == 0)
        {
            using (SqlConnection connection = DALHelper.Connection)
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
        }

        return utilizatori;
    }

    public void ActualizeazaUtilizator(string username, string parola, Utilizator utilizator)
    {
        try
        {
            using (SqlConnection connection = DALHelper.Connection)
            {
                string query = "UPDATE Utilizatori SET NumeUtilizator = @NumeUtilizator, Parola = @Parola, TipUtilizator = @TipUtilizator WHERE NumeUtilizator = @OldNumeUtilizator AND Parola = @OldParola";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeUtilizator", username);
                command.Parameters.AddWithValue("@Parola", parola);
                command.Parameters.AddWithValue("@TipUtilizator", utilizator.TipUtilizator);
                command.Parameters.AddWithValue("@OldNumeUtilizator", utilizator.NumeUtilizator);
                command.Parameters.AddWithValue("@OldParola", utilizator.Parola);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show("LoginError", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    public void StergeUtilizator(int idUtilizator)
    {
        for(int i = 0; i < utilizatori.Count; i++)
        {
            if (utilizatori[i].ID == idUtilizator)
            {
                utilizatori.RemoveAt(i);
                break;
            }
        }
        /*try
        {
            using (SqlConnection connection = DALHelper.Connection)
            {
                string query = "DELETE FROM Utilizatori WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", idUtilizator);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        catch
        (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show("LoginError", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
        }*/
    }

    public Utilizator GetUtilizatorByParola(string parola)
    {
        Utilizator utilizatorGasit = null;

        try
        {
            using (SqlConnection connection = DALHelper.Connection)
            {
                string query = "SELECT * FROM Utilizatori WHERE Parola = @Parola";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Parola", parola);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    utilizatorGasit = new Utilizator
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        NumeUtilizator = reader["NumeUtilizator"].ToString(),
                        Parola = reader["Parola"].ToString(),
                        TipUtilizator = reader["TipUtilizator"].ToString()
                    };
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show("LoginError", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        return utilizatorGasit;
    }

    public Utilizator GetUtilizatorByUsername(string username)
    {
        Utilizator utilizatorGasit = null;


        using (SqlConnection connection = DALHelper.Connection)
        {
            string query = "SELECT * FROM Utilizatori WHERE NumeUtilizator = @NumeUtilizator";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumeUtilizator", username);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                utilizatorGasit = new Utilizator
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    NumeUtilizator = reader["NumeUtilizator"].ToString(),
                    Parola = reader["Parola"].ToString(),
                    TipUtilizator = reader["TipUtilizator"].ToString()
                };
            }
        }

        return utilizatorGasit;
    }

}
