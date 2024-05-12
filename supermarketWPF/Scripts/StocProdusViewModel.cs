using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using supermarketWPF;
using supermarketWPF.Layers.DataAccesLayer;
namespace supermarketWPF.ViewModels
{
    public class StocProdusViewModel : INotifyPropertyChanged
    {
        private StocProdus stocSelectat;
        private ObservableCollection<StocProdus> stocuri;
        private StocProdusDAL stocProdusDAL;
        private string idProdus;
        private string cantitate;
        private string unitateMasura;
        private DateTime dataAprovizionare;
        private DateTime dataExpirare;
        private string pretAchizitie;
        private string pretVanzare;

        public string IDProdus
        {
            get { return idProdus; }
            set
            {
                idProdus = value;
                OnPropertyChanged("IDProdus");
            }
        }
        public string Cantitate
        {
            get { return cantitate; }
            set
            {
                cantitate = value;
                OnPropertyChanged("Cantitate");
            }
        }
        public string UnitateMasura
        {
            get { return unitateMasura; }
            set
            {
                unitateMasura = value;
                OnPropertyChanged("UnitateMasura");
            }
        }
        public DateTime DataAprovizionare
        {
            get { return dataAprovizionare; }
            set
            {
                dataAprovizionare = value;
                OnPropertyChanged("DataAprovizionare");
            }
        }
        public DateTime DataExpirare
        {
            get { return dataExpirare; }
            set
            {
                dataExpirare = value;
                OnPropertyChanged("DataExpirare");
            }
        }
        public string PretAchizitie
        {
            get { return pretAchizitie; }
            set
            {
                pretAchizitie = value;
                OnPropertyChanged("PretAchizitie");
            }
        }
        public string PretVanzare
        {
            get { return pretVanzare; }
            set
            {
                pretVanzare = value;
                OnPropertyChanged("PretVanzare");
            }
        }


        public StocProdusViewModel(string connectionString)
        {
            stocProdusDAL = new StocProdusDAL(connectionString);

            Stocuri = new ObservableCollection<StocProdus>(stocProdusDAL.GetStocuriProduse());
            OnPropertyChanged("Stocuri");
        }
        public StocProdusViewModel()
        {
            stocProdusDAL = new StocProdusDAL(@"ConnectionString");
            Stocuri = new ObservableCollection<StocProdus>(stocProdusDAL.GetStocuriProduse());
            OnPropertyChanged("Stocuri");
        }

        public StocProdus StocSelectat
        {
            get { return stocSelectat; }
            set
            {
                stocSelectat = value;
                OnPropertyChanged("StocSelectat");
            }
        }

        public ObservableCollection<StocProdus> Stocuri
        {
            get { return stocuri; }
            set
            {
                stocuri = value;
                OnPropertyChanged("Stocuri");
            }
        }

        private ICommand adaugaStocCommand;
        public ICommand AdaugaStocCommand
        {
            get
            {
                if (adaugaStocCommand == null)
                {
                    adaugaStocCommand = new RelayCommand(AdaugaStoc);
                }
                return adaugaStocCommand;
            }
        }

        private ICommand editeazaStocCommand;
        public ICommand EditeazaStocCommand
        {
            get
            {
                if (editeazaStocCommand == null)
                {
                    editeazaStocCommand = new RelayCommand(EditeazaStoc);
                }
                return editeazaStocCommand;
            }
        }

        private ICommand stergeStocCommand;
        public ICommand StergeStocCommand
        {
            get
            {
                if (stergeStocCommand == null)
                {
                    stergeStocCommand = new RelayCommand(StergeStoc);
                }
                return stergeStocCommand;
            }
        }

        private void AdaugaStoc(object obj)
        {
            StocProdus stoc = new StocProdus
            {
                IDProdus = int.Parse(IDProdus),
                Cantitate = Convert.ToInt32(Cantitate),
                UnitateMasura = UnitateMasura,
                DataAprovizionare = DataAprovizionare,
                DataExpirare = DataExpirare,
                PretAchizitie = Convert.ToDecimal(PretAchizitie),
                PretVanzare = ConfigFile.adaosComercial * Convert.ToDecimal(PretAchizitie)/100 + Convert.ToDecimal(PretAchizitie)
            };
            stocProdusDAL.AdaugaStocProdus(stoc);
            Stocuri.Add(StocSelectat);
            stocuri = new ObservableCollection<StocProdus>(stocProdusDAL.GetStocuriProduse());
            OnPropertyChanged("Stocuri");
            
        }

        private void EditeazaStoc(object obj)
        {
            StocProdus stoc = new StocProdus
            {
                IDProdus = int.Parse(IDProdus),
                Cantitate = Convert.ToInt32(Cantitate),
                UnitateMasura = UnitateMasura,
                DataAprovizionare = DataAprovizionare,
                DataExpirare = DataExpirare,
                PretAchizitie = Convert.ToDecimal(PretAchizitie),
                PretVanzare = ConfigFile.adaosComercial * Convert.ToDecimal(PretAchizitie) / 100 + Convert.ToDecimal(PretAchizitie)
            };
            stocProdusDAL.ActualizeazaStocProdus(StocSelectat);
            stocuri = new ObservableCollection<StocProdus>(stocProdusDAL.GetStocuriProduse());
            OnPropertyChanged("Stocuri");

        }

        private void StergeStoc(object obj)
        {
            if(StocSelectat == null)
            {
                return;
            }
            stocProdusDAL.StergeStocProdus(StocSelectat.ID);
            Stocuri.Remove(StocSelectat);
            OnPropertyChanged("Stocuri");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}