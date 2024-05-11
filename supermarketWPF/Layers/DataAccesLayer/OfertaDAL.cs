using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class OfertaDAL
{
    private string connectionString;

    public OfertaDAL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void AdaugaOferta(Oferta oferta)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Oferte (MotivOferta, IDProdus, ProcentReducere, DataInceput, DataSfarsit) VALUES (@MotivOferta, @IDProdus, @ProcentReducere, @DataInceput, @DataSfarsit)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MotivOferta", oferta.MotivOferta);
            command.Parameters.AddWithValue("@IDProdus", oferta.IDProdus);
            command.Parameters.AddWithValue("@ProcentReducere", oferta.ProcentReducere);
            command.Parameters.AddWithValue("@DataInceput", oferta.DataInceput);
            command.Parameters.AddWithValue("@DataSfarsit", oferta.DataSfarsit);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public List<Oferta> GetOferte()
    {
        List<Oferta> oferte = new List<Oferta>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Oferte";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Oferta oferta = new Oferta();
                oferta.ID = Convert.ToInt32(reader["ID"]);
                oferta.MotivOferta = reader["MotivOferta"].ToString();
                oferta.IDProdus = Convert.ToInt32(reader["IDProdus"]);
                oferta.ProcentReducere = Convert.ToDecimal(reader["ProcentReducere"]);
                oferta.DataInceput = Convert.ToDateTime(reader["DataInceput"]);
                oferta.DataSfarsit = Convert.ToDateTime(reader["DataSfarsit"]);

                oferte.Add(oferta);
            }
        }

        return oferte;
    }
}
