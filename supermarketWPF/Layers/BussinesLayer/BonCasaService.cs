using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class BonCasaService
{
    private string connectionString;

    public BonCasaService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Metodă pentru adăugarea unui bon de casă nou
    public void AdaugaBonCasa(DateTime dataEliberare, string casier, List<ProdusVandut> produseVandute, decimal sumaIncasata)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Adăugăm bonul de casă
            string queryBon = "INSERT INTO BonuriCasa (DataEliberare, Casier, SumaIncasata) VALUES (@DataEliberare, @Casier, @SumaIncasata)";
            SqlCommand commandBon = new SqlCommand(queryBon, connection);
            commandBon.Parameters.AddWithValue("@DataEliberare", dataEliberare);
            commandBon.Parameters.AddWithValue("@Casier", casier);
            commandBon.Parameters.AddWithValue("@SumaIncasata", sumaIncasata);

            connection.Open();
            commandBon.ExecuteNonQuery();

            // Obținem ID-ul bonului de casă nou adăugat
            string queryGetBonID = "SELECT SCOPE_IDENTITY()";
            SqlCommand commandGetBonID = new SqlCommand(queryGetBonID, connection);
            int bonID = Convert.ToInt32(commandGetBonID.ExecuteScalar());

            // Adăugăm produsele vândute pe acest bon
            foreach (var produsVandut in produseVandute)
            {
                string queryProdus = "INSERT INTO ProduseVandute (IDBon, NumeProdus, Cantitate, PretUnitar, Subtotal) VALUES (@IDBon, @NumeProdus, @Cantitate, @PretUnitar, @Subtotal)";
                SqlCommand commandProdus = new SqlCommand(queryProdus, connection);
                commandProdus.Parameters.AddWithValue("@IDBon", bonID);
                commandProdus.Parameters.AddWithValue("@NumeProdus", produsVandut.NumeProdus);
                commandProdus.Parameters.AddWithValue("@Cantitate", produsVandut.Cantitate);
                commandProdus.Parameters.AddWithValue("@PretUnitar", produsVandut.PretUnitar);
                commandProdus.Parameters.AddWithValue("@Subtotal", produsVandut.Subtotal);

                commandProdus.ExecuteNonQuery();
            }
        }
    }

    // Metodă pentru obținerea tuturor bonurilor de casă din baza de date
    public List<BonCasa> GetBonuriCasa()
    {
        List<BonCasa> bonuriCasa = new List<BonCasa>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string queryBonuri = "SELECT * FROM BonuriCasa";
            SqlCommand commandBonuri = new SqlCommand(queryBonuri, connection);

            connection.Open();
            SqlDataReader readerBonuri = commandBonuri.ExecuteReader();

            while (readerBonuri.Read())
            {
                BonCasa bonCasa = new BonCasa();
                bonCasa.IDBon = Convert.ToInt32(readerBonuri["IDBon"]);
                bonCasa.DataEliberare = Convert.ToDateTime(readerBonuri["DataEliberare"]);
                bonCasa.Casier = readerBonuri["Casier"].ToString();
                bonCasa.SumaIncasata = Convert.ToDecimal(readerBonuri["SumaIncasata"]);

                // Obținem produsele vândute pe acest bon
                string queryProduse = "SELECT * FROM ProduseVandute WHERE IDBon = @IDBon";
                SqlCommand commandProduse = new SqlCommand(queryProduse, connection);
                commandProduse.Parameters.AddWithValue("@IDBon", bonCasa.IDBon);
                SqlDataReader readerProduse = commandProduse.ExecuteReader();

                bonCasa.ProduseVandute = new List<ProdusVandut>();

                while (readerProduse.Read())
                {
                    ProdusVandut produsVandut = new ProdusVandut();
                    produsVandut.NumeProdus = readerProduse["NumeProdus"].ToString();
                    produsVandut.Cantitate = Convert.ToInt32(readerProduse["Cantitate"]);
                    produsVandut.PretUnitar = Convert.ToDecimal(readerProduse["PretUnitar"]);
                    produsVandut.Subtotal = Convert.ToDecimal(readerProduse["Subtotal"]);

                    bonCasa.ProduseVandute.Add(produsVandut);
                }

                bonuriCasa.Add(bonCasa);
            }
        }

        return bonuriCasa;
    }
}

public class BonCasa
{
    public int IDBon { get; set; }
    public DateTime DataEliberare { get; set; }
    public string Casier { get; set; }
    public decimal SumaIncasata { get; set; }
    public List<ProdusVandut> ProduseVandute { get; set; }
}

public class ProdusVandut
{
    public string NumeProdus { get; set; }
    public int Cantitate { get; set; }
    public decimal PretUnitar { get; set; }
    public decimal Subtotal { get; set; }
}
