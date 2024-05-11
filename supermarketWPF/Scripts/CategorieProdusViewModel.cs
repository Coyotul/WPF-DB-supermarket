using supermarketWPF;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

public class CategorieProdusViewModel : INotifyPropertyChanged
{
    private CategorieProdus categorieSelectata;
    private ObservableCollection<CategorieProdus> categorii;
    private CategorieProdusDAL categorieProdusDAL;

    public CategorieProdus CategorieSelectata
    {
        get { return categorieSelectata; }
        set
        {
            categorieSelectata = value;
            OnPropertyChanged("CategorieSelectata");
        }
    }

    public ObservableCollection<CategorieProdus> Categorii
    {
        get { return categorii; }
        set
        {
            categorii = value;
            OnPropertyChanged("Categorii");
        }
    }

    public ICommand AdaugaCategorieCommand { get; set; }
    public ICommand EditeazaCategorieCommand { get; set; }
    public ICommand StergeCategorieCommand { get; set; }

    public CategorieProdusViewModel(string connectionString)
    {
        categorieProdusDAL = new CategorieProdusDAL(connectionString);

        Categorii = new ObservableCollection<CategorieProdus>(categorieProdusDAL.GetCategoriiProduse());

        AdaugaCategorieCommand = new RelayCommand(AdaugaCategorie);
        EditeazaCategorieCommand = new RelayCommand(EditeazaCategorie);
        StergeCategorieCommand = new RelayCommand(StergeCategorie);
    }

    private void AdaugaCategorie(object obj)
    {
        categorieProdusDAL.AdaugaCategorieProdus(CategorieSelectata);
        Categorii.Add(CategorieSelectata);
    }

    private void EditeazaCategorie(object obj)
    {
        categorieProdusDAL.ActualizeazaCategorieProdus(CategorieSelectata);
    }

    private void StergeCategorie(object obj)
    {
        categorieProdusDAL.StergeCategorieProdus(CategorieSelectata.ID);
        Categorii.Remove(CategorieSelectata);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
