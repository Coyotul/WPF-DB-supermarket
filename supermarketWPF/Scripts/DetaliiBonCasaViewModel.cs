using System;
using System.ComponentModel;
using System.Collections.Generic;

public class DetaliiBonCasaViewModel : INotifyPropertyChanged
{
    private DetaliiBonCasa detaliiBonCasa;
    private DetaliiBonCasaDAL detaliiBonCasaDAL;

    public DetaliiBonCasa DetaliiBonCasa
    {
        get { return detaliiBonCasa; }
        set
        {
            detaliiBonCasa = value;
            OnPropertyChanged("DetaliiBonCasa");
        }
    }

    public DetaliiBonCasaViewModel(string connectionString)
    {
        detaliiBonCasaDAL = new DetaliiBonCasaDAL(connectionString);
    }

    public void IncarcaDetaliiBonCasa(int idBon)
    {
        DetaliiBonCasa = detaliiBonCasaDAL.GetDetaliiBonCasa(idBon);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
