using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class BonCasaDAL
{
    private string connectionString;

    public BonCasaDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void AdaugaBonDeCasa(BonCasa BonCasa)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO BonuriDeCasa (DataEliberare, Casier, SumaIncasata) VALUES (@DataEliberare, @Casier, @SumaIncasata)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DataEliberare", BonCasa.DataEliberare);
            command.Parameters.AddWithValue("@Casier", BonCasa.Casier);
            command.Parameters.AddWithValue("@SumaIncasata", BonCasa.SumaIncasata);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public List<BonCasa> GetBonuriDeCasa()
    {
        List<BonCasa> bonuriDeCasa = new List<BonCasa>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM BonuriDeCasa";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                BonCasa BonCasa = new BonCasa();
                BonCasa.IDBon = Convert.ToInt32(reader["ID"]);
                BonCasa.DataEliberare = Convert.ToDateTime(reader["DataEliberare"]);
                BonCasa.Casier = reader["Casier"].ToString();
                BonCasa.SumaIncasata = Convert.ToDecimal(reader["SumaIncasata"]);

                bonuriDeCasa.Add(BonCasa);
            }
        }

        return bonuriDeCasa;
    }

    public void ActualizeazaBonDeCasa(BonCasa BonCasa)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE BonuriDeCasa SET DataEliberare = @DataEliberare, Casier = @Casier, SumaIncasata = @SumaIncasata WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DataEliberare", BonCasa.DataEliberare);
            command.Parameters.AddWithValue("@Casier", BonCasa.Casier);
            command.Parameters.AddWithValue("@SumaIncasata", BonCasa.SumaIncasata);
            command.Parameters.AddWithValue("@ID", BonCasa.IDBon);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void StergeBonDeCasa(int idBonDeCasa)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM BonuriDeCasa WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", idBonDeCasa);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
