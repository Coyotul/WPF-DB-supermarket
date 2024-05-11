using supermarketWPF;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

public class ProducatorViewModel : INotifyPropertyChanged
{
    private Producator producatorSelectat;
    private ObservableCollection<Producator> producatori;
    private ProducatorDAL producatorDAL;

    public Producator ProducatorSelectat
    {
        get { return producatorSelectat; }
        set
        {
            producatorSelectat = value;
            OnPropertyChanged("ProducatorSelectat");
        }
    }

    public ObservableCollection<Producator> Producatori
    {
        get { return producatori; }
        set
        {
            producatori = value;
            OnPropertyChanged("Producatori");
        }
    }

    public ICommand AdaugaProducatorCommand { get; set; }
    public ICommand EditeazaProducatorCommand { get; set; }
    public ICommand StergeProducatorCommand { get; set; }

    public ProducatorViewModel(string connectionString)
    {
        producatorDAL = new ProducatorDAL(connectionString);

        Producatori = new ObservableCollection<Producator>(producatorDAL.GetProducatori());

        AdaugaProducatorCommand = new RelayCommand(AdaugaProducator);
        EditeazaProducatorCommand = new RelayCommand(EditeazaProducator);
        StergeProducatorCommand = new RelayCommand(StergeProducator);
    }

    private void AdaugaProducator(object obj)
    {
        producatorDAL.AdaugaProducator(ProducatorSelectat);
        Producatori.Add(ProducatorSelectat);
    }

    private void EditeazaProducator(object obj)
    {
        producatorDAL.ActualizeazaProducator(ProducatorSelectat);
    }

    private void StergeProducator(object obj)
    {
        producatorDAL.StergeProducator(ProducatorSelectat.ID);
        Producatori.Remove(ProducatorSelectat);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
