using supermarketWPF;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace supermarketWPF.ViewModels
{
    public class UtilizatorViewModel : INotifyPropertyChanged
    {
        private Utilizator utilizatorSelectat;
        private ObservableCollection<Utilizator> utilizatori;
        private UserDAL userDAL;
        private UtilizatorService userBL;
        public UtilizatorViewModel()
        {
            utilizatori = new ObservableCollection<Utilizator>();
            userDAL = new UserDAL(@"Data Source=DESKTOP-3QK6J8I\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
            utilizatori = new ObservableCollection<Utilizator>(userDAL.GetUtilizatori());
            OnPropertyChanged("Utilizatori");
        }

        public Utilizator UtilizatorSelectat
        {
            get { return utilizatorSelectat; }
            set
            {
                utilizatorSelectat = value;
                OnPropertyChanged("UtilizatorSelectat");
            }
        }

        public ObservableCollection<Utilizator> Utilizatori
        {
            get { return utilizatori; }
            set
            {
                utilizatori = value;
                OnPropertyChanged("Utilizatori");
            }
        }

        private string parola;


        public string Parola
        {
            get { return parola; }
            set
            {
                parola = value;
                OnPropertyChanged("Parola");
            }
        }

        private string username;


        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }


        private RelayCommand adaugaUtilizatorCommand;

        public ICommand AdaugaUtilizatorCommand
        {
            get
            {
                if (adaugaUtilizatorCommand == null)
                {
                    adaugaUtilizatorCommand = new RelayCommand(AdaugaUtilizator);
                }

                return adaugaUtilizatorCommand;
            }
        }

        private RelayCommand editeazaUtilizatorCommand;

        public ICommand EditeazaUtilizatorCommand
        {
            get
            {
                if (editeazaUtilizatorCommand == null)
                {
                    editeazaUtilizatorCommand = new RelayCommand(EditeazaUtilizator);
                }

                return editeazaUtilizatorCommand;
            }
        }

        private RelayCommand stergeUtilizatorCommand;

        public ICommand StergeUtilizatorCommand
        {
            get
            {
                if (stergeUtilizatorCommand == null)
                {
                    stergeUtilizatorCommand = new RelayCommand(StergeUtilizator);
                }

                return stergeUtilizatorCommand;
            }
        }
        public UtilizatorViewModel(string connectionString)
        {
            userDAL = new UserDAL(connectionString);

            Utilizatori = new ObservableCollection<Utilizator>(userDAL.GetUtilizatori());

            
        }

        private void AdaugaUtilizator(object obj)
        {
            UtilizatorSelectat = new Utilizator();
            UtilizatorSelectat.NumeUtilizator = Username;
            UtilizatorSelectat.ID = Utilizatori.Count + 1;
            UtilizatorSelectat.Parola = Parola;
            UtilizatorSelectat.TipUtilizator = "casier";
            userDAL.AdaugaUtilizator(UtilizatorSelectat);
            Utilizatori.Add(UtilizatorSelectat);
            utilizatori = new ObservableCollection<Utilizator>(userDAL.GetUtilizatori());
            OnPropertyChanged("Utilizatori"); // Notifică ObservableCollection că s-au adăugat elemente noi
            MessageBox.Show("Utilizatorul a fost adăugat.");
        }

        private void EditeazaUtilizator(object obj)
        {
            userDAL.ActualizeazaUtilizator(username, parola, UtilizatorSelectat);
            utilizatori = new ObservableCollection<Utilizator>(userDAL.GetUtilizatori());

            OnPropertyChanged("Utilizatori"); // Notifică ObservableCollection că s-au adăugat elemente noi
            MessageBox.Show("Utilizatorul a fost actualizat.");
        }

        private void StergeUtilizator(object obj)
        {
            userDAL.StergeUtilizator(UtilizatorSelectat.ID);
            utilizatori = new ObservableCollection<Utilizator>(userDAL.GetUtilizatori());

            OnPropertyChanged("Utilizatori"); // Notifică ObservableCollection că s-au adăugat elemente noi
            MessageBox.Show("Utilizatorul a fost șters.");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private RelayCommand loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommand(Login);
                }

                return loginCommand;
            }
        }

        private void Login(object commandParameter)
        {
            if (!string.IsNullOrEmpty(Parola))
            {
                // Verificăm în baza de date dacă există un utilizator cu numele introdus și parola corespunzătoare
                var utilizatorGasit = userDAL.GetUtilizatorByParola(Parola);

                if (utilizatorGasit != null)
                {
                    if(utilizatorGasit.TipUtilizator == "cashier")
                    {
                        Window window = new CashierWindow();
                        window.Show();
                    }
                    else
                    {
                        Window window = new AdminWindow();
                        window.Show();
                    }
                   
                }
                else
                {
                    MessageBox.Show("Utilizatorul nu există sau parola este incorectă.");
                }
            }
            else
            {
                MessageBox.Show("Introduceți numele de utilizator și parola.");
            }
        }
    }
}