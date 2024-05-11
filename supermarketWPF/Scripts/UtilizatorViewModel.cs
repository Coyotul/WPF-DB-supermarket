using supermarketWPF;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

public class UtilizatorViewModel : INotifyPropertyChanged
{
    private Utilizator utilizatorSelectat;
    private ObservableCollection<Utilizator> utilizatori;
    private UserDAL userDAL;

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

    public ICommand AdaugaUtilizatorCommand { get; set; }
    public ICommand EditeazaUtilizatorCommand { get; set; }
    public ICommand StergeUtilizatorCommand { get; set; }

    public UtilizatorViewModel(string connectionString)
    {
        userDAL = new UserDAL(connectionString);

        Utilizatori = new ObservableCollection<Utilizator>(userDAL.GetUtilizatori());

        AdaugaUtilizatorCommand = new RelayCommand(AdaugaUtilizator);
        EditeazaUtilizatorCommand = new RelayCommand(EditeazaUtilizator);
        StergeUtilizatorCommand = new RelayCommand(StergeUtilizator);
    }

    private void AdaugaUtilizator(object obj)
    {
        userDAL.AdaugaUtilizator(UtilizatorSelectat);
        Utilizatori.Add(UtilizatorSelectat);
    }

    private void EditeazaUtilizator(object obj)
    {
        userDAL.ActualizeazaUtilizator(UtilizatorSelectat);
    }

    private void StergeUtilizator(object obj)
    {
        userDAL.StergeUtilizator(UtilizatorSelectat.ID);
        Utilizatori.Remove(UtilizatorSelectat);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
