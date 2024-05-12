using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using SupermarketApp.Converters;
using supermarketWPF.Layers.DataAccesLayer;

namespace supermarketWPF.Layers.DataAccesLayer
{
    public class ProdusDAL
    {
        private string connectionString;

        public ProdusDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AdaugaProdus(Produs produs)
        {
            try
            {
                using (SqlConnection connection = DALHelper.Connection)
                {
                    string query = "INSERT INTO Produse (NumeProdus, CodDeBare, Categorie, Producator, DataIntrarii) VALUES (@NumeProdus, @CodDeBare, @Categorie, @Producator, GETDATE())";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NumeProdus", produs.NumeProdus);
                    command.Parameters.AddWithValue("@CodDeBare", produs.CodDeBare);
                    command.Parameters.AddWithValue("@Categorie", produs.Categorie);
                    command.Parameters.AddWithValue("@Producator", produs.Producator);
                    command.Parameters.AddWithValue("@DataIntrarii", DateTime.Now);

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

        public void EditeazaProdus(Produs produs, string numeProdusVechi, string codDeBareVechi)
        {
            try
            {
                using (SqlConnection connection = DALHelper.Connection)
                {
                    string query = "UPDATE Produse SET NumeProdus = @NumeProdus, CodDeBare = @CodDeBare, Categorie = @Categorie, Producator = @Producator WHERE NumeProdus = @NumeProdusVechi AND CodDeBare = @CodDeBareVechi";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NumeProdus", produs.NumeProdus);
                    command.Parameters.AddWithValue("@CodDeBare", produs.CodDeBare);
                    command.Parameters.AddWithValue("@Categorie", produs.Categorie);
                    command.Parameters.AddWithValue("@Producator", produs.Producator);
                    command.Parameters.AddWithValue("@NumeProdusVechi", numeProdusVechi);
                    command.Parameters.AddWithValue("@CodDeBareVechi", codDeBareVechi);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Produsul nu a putut fi editat în baza de date!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void StergeProdus(string numeprodus)
        {
            try
            {
                using (SqlConnection connection = DALHelper.Connection)
                {
                    string query = "DELETE FROM Produse WHERE NumeProdus = @NumeProdus";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NumeProdus", numeprodus);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Produsul nu a putut fi sters", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Produs> GetProduse()
        {
            List<Produs> produse = new List<Produs>();

            try
            {
                using (SqlConnection connection = DALHelper.Connection)
                {
                    string query = "SELECT p.*, sp.PretAchizitie, sp.PretVanzare FROM Produse p JOIN StocuriProduse sp ON p.ID = sp.IDProdus";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Produs produs = new Produs
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            NumeProdus = reader["NumeProdus"].ToString(),
                            CodDeBare = reader["CodDeBare"].ToString(),
                            Categorie = reader["Categorie"].ToString(),
                            Producator = reader["Producator"].ToString(),
                            DataIntrarii = Convert.ToDateTime(reader["DataIntrarii"]),
                            Pret = (float)Convert.ToDecimal(reader["PretVanzare"])
                    };
                        produse.Add(produs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Produsul nu a putut fi adăugat în baza de date!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return produse;
        }
    }
}
