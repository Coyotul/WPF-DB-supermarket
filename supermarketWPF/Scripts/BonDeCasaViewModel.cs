using supermarketWPF.Layers.DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;

namespace supermarketWPF.ViewModels
{
    public class BonDeCasaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BonComplet> bonuri;
        private BonCasaDAL bonCasaDAL;

        private string idBon;
        private string dataEliberare;
        private string casier;
        private string sumaIncasata;
        private string idProdus;
        private string subtotal;

        public string IDBon
        {
            get { return idBon; }
            set
            {
                idBon = value;
                OnPropertyChanged("IDBon");
            }
        }
        public string DataEliberare
        {
            get { return dataEliberare; }
            set
            {
                dataEliberare = value;
                OnPropertyChanged("DataEliberare");
            }
        }
        public string Casier
        {
            get { return casier; }
            set
            {
                casier = value;
                OnPropertyChanged("Casier");
            }
        }
        public string SumaIncasata
        {
            get { return sumaIncasata; }
            set
            {
                sumaIncasata = value;
                OnPropertyChanged("SumaIncasata");
            }
        }
        public string IDProdus
        {
            get { return idProdus; }
            set
            {
                idProdus = value;
                OnPropertyChanged("IDProdus");
            }
        }
        public string Subtotal
        {
            get { return subtotal; }
            set
            {
                subtotal = value;
                OnPropertyChanged("Subtotal");
            }
        }


        public ObservableCollection<BonComplet> Bonuri
        {
            get { return bonuri; }
            set
            {
                bonuri = value;
                OnPropertyChanged("Bonuri");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BonDeCasaViewModel()
        {
            bonuri = new ObservableCollection<BonComplet>();
            bonCasaDAL = new BonCasaDAL(@"Data Source=DESKTOP-3QK6J8I\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=false");
            Bonuri = new ObservableCollection<BonComplet>(bonCasaDAL.GetBonComplet());
            OnPropertyChanged("Bonuri");
        }
    }
}