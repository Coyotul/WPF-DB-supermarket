using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

public class OfertaViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Oferta> oferte;
    private OfertaDAL ofertaDAL;

    public ObservableCollection<Oferta> Oferte
    {
        get { return oferte; }
        set
        {
            oferte = value;
            OnPropertyChanged("Oferte");
        }
    }

    public OfertaViewModel(string connectionString)
    {
        ofertaDAL = new OfertaDAL(connectionString);
    }

    public void IncarcaOferte()
    {
        Oferte = new ObservableCollection<Oferta>(ofertaDAL.GetOferte());
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
