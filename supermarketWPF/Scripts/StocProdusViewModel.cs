using supermarketWPF;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

public class StocProdusViewModel : INotifyPropertyChanged
{
    private StocProdus stocSelectat;
    private ObservableCollection<StocProdus> stocuri;
    private StocProdusDAL stocProdusDAL;

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

    public ICommand AdaugaStocCommand { get; set; }
    public ICommand EditeazaStocCommand { get; set; }
    public ICommand StergeStocCommand { get; set; }

    public StocProdusViewModel(string connectionString)
    {
        stocProdusDAL = new StocProdusDAL(connectionString);

        Stocuri = new ObservableCollection<StocProdus>(stocProdusDAL.GetStocuriProduse());

        AdaugaStocCommand = new RelayCommand(AdaugaStoc);
        EditeazaStocCommand = new RelayCommand(EditeazaStoc);
        StergeStocCommand = new RelayCommand(StergeStoc);
    }

    private void AdaugaStoc(object obj)
    {
        stocProdusDAL.AdaugaStocProdus(StocSelectat);
        Stocuri.Add(StocSelectat);
    }

    private void EditeazaStoc(object obj)
    {
        stocProdusDAL.ActualizeazaStocProdus(StocSelectat);
    }

    private void StergeStoc(object obj)
    {
        stocProdusDAL.StergeStocProdus(StocSelectat.ID);
        Stocuri.Remove(StocSelectat);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
