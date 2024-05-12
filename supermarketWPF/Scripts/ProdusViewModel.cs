using supermarketWPF.Layers.DataAccesLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace supermarketWPF.ViewModels
{
    public class ProdusViewModel : INotifyPropertyChanged
    {
        private Produs produsSelectat;
        private ObservableCollection<Produs> produse;
        private ProdusDAL produsDAL;

        private string numeProdus;
        private string codDeBare;
        private string categorie;
        private string producator;
        private string iD;
        private string pretVanzare;

        private string producatorCautare;

        public string ProducatorCautare
        {
            get { return producatorCautare; }
            set
            {
                producatorCautare = value;
                OnPropertyChanged("ProducatorCautare");
            }
        }

        public string Numeprodus
        {
            get { return numeProdus; }
            set
            {
                numeProdus = value;
                OnPropertyChanged("NumeProdus");
            }
        }
        public string CodDeBare
        {
            get { return codDeBare; }
            set
            {
                codDeBare = value;
                OnPropertyChanged("CodDeBare");
            }
        }
        public string Categorie
        {
            get { return categorie; }
            set
            {
                categorie = value;
                OnPropertyChanged("Categorie");
            }
        }
        public string Producator
        {
            get { return producator; }
            set
            {
                producator = value;
                OnPropertyChanged("Producator");
            }
        }
        public string ID
        {
            get { return iD; }
            set
            {
                iD = value;
                OnPropertyChanged("ID");
            }
        }

        public string PretVanzare
        {
            get { return pretVanzare; }
            set
            {
                pretVanzare = value;
                OnPropertyChanged("Pret");
            }
        }

        

        public Produs ProdusSelectat
        {
            get { return produsSelectat; }
            set
            {
                produsSelectat = value;
                OnPropertyChanged("ProdusSelectat");
            }
        }

        public ObservableCollection<Produs> Produse
        {
            get { return produse; }
            set
            {
                produse = value;
                OnPropertyChanged("Produse");
            }
        }

        // Alte proprietăți și membri

        private RelayCommand adaugaProdusCommand;

        public ICommand AdaugaProdusCommand
        {
            get
            {
                if (adaugaProdusCommand == null)
                {
                    adaugaProdusCommand = new RelayCommand(AdaugaProdus);
                }
                return adaugaProdusCommand;
            }
        }

        private RelayCommand editeazaProdusCommand;

        public ICommand EditeazaProdusCommand
        {
            get
            {
                if (editeazaProdusCommand == null)
                {
                    editeazaProdusCommand = new RelayCommand(EditeazaProdus);
                }
                return editeazaProdusCommand;
            }
        }

        private RelayCommand stergeProdusCommand;

        public ICommand StergeProdusCommand
        {
            get
            {
                if (stergeProdusCommand == null)
                {
                    stergeProdusCommand = new RelayCommand(StergeProdus);
                }
                return stergeProdusCommand;
            }
        }

        public ProdusViewModel()
        {
            // Inițializare produse și DAL
            produse = new ObservableCollection<Produs>();
            produsDAL = new ProdusDAL(@"ConnectionString");
            Produse = new ObservableCollection<Produs>(produsDAL.GetProduse());
        }

        // Metoda pentru adăugarea unui produs
        private void AdaugaProdus(object obj)
        {
            Produs prod= new Produs();
            prod.NumeProdus = Numeprodus;
            prod.Producator = Producator;
            prod.Categorie = Categorie;
            prod.CodDeBare = CodDeBare;
            prod.ID = produse.Count + 1;
            prod.Pret = 10;
            StocProdus stoc = new StocProdus();
            stoc.IDProdus = prod.ID;
            stoc.Cantitate = 0;
            stoc.UnitateMasura = "buc";
            stoc.DataAprovizionare = DateTime.Now;
            stoc.DataExpirare = DateTime.Now.AddMonths(1);
            stoc.PretAchizitie = 0;
            stoc.PretVanzare = 0;
            StocProdusDAL stocDAL = new StocProdusDAL(@"ConnectionString");
            stocDAL.AdaugaStocProdus(stoc);
            produsDAL.AdaugaProdus(prod);
            MessageBox.Show("Produs adăugat cu succes!");
            Produse = new ObservableCollection<Produs>(produsDAL.GetProduse());
            OnPropertyChanged("Produse");
        }

        // Metoda pentru editarea unui produs
        private void EditeazaProdus(object obj)
        {
            Produs produs = new Produs();
            produs.NumeProdus = Numeprodus;
            produs.CodDeBare = CodDeBare;
            produs.ID = produse.
                Count + 1;
            produs.DataIntrarii = DateTime.Now;
            produs.Categorie = Categorie;
            produs.Producator = Producator;
            produsDAL.EditeazaProdus(produs, produsSelectat.NumeProdus, produsSelectat.CodDeBare);
            Produse = new ObservableCollection<Produs>(produsDAL.GetProduse());
            MessageBox.Show("Produs editat cu succes!");
            OnPropertyChanged("Produse");
        }

        // Metoda pentru ștergerea unui produs
        private void StergeProdus(object obj)
        {
            if (produsSelectat != null)
            {
                StocProdusDAL stocProdusDAL = new StocProdusDAL(@"ConnectionString");
                stocProdusDAL.StergeStocProdus(produsSelectat.ID);
                produsDAL.StergeProdus(produsSelectat.NumeProdus);
                Produse = new ObservableCollection<Produs>(produsDAL.GetProduse());
                MessageBox.Show("Produs șters cu succes!");
                OnPropertyChanged("Produse");
            }
            else
                MessageBox.Show("Selectați un produs pentru a-l șterge!");

        }

        // Alte metode și evenimente

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
