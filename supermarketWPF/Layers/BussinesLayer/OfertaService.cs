using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class OfertaService
{
    private string connectionString;

    public OfertaService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unei noi oferte
    public void AdaugaOferta(string motivOferta, int idProdus, decimal procentReducere, DateTime dataInceput, DateTime dataSfarsit)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Oferte (MotivOferta, IDProdus, ProcentReducere, DataInceput, DataSfarsit) VALUES (@MotivOferta, @IDProdus, @ProcentReducere, @DataInceput, @DataSfarsit)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MotivOferta", motivOferta);
            command.Parameters.AddWithValue("@IDProdus", idProdus);
            command.Parameters.AddWithValue("@ProcentReducere", procentReducere);
            command.Parameters.AddWithValue("@DataInceput", dataInceput);
            command.Parameters.AddWithValue("@DataSfarsit", dataSfarsit);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    // Metodă pentru obținerea tuturor ofertelor din baza de date
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

public class Oferta
{
    public int ID { get; set; }
    public string MotivOferta { get; set; }
    public int IDProdus { get; set; }
    public decimal ProcentReducere { get; set; }
    public DateTime DataInceput { get; set; }
    public DateTime DataSfarsit { get; set; }
}
