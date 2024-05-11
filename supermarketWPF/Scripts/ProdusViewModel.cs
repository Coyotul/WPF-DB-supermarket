using supermarketWPF;
using System;
using System.ComponentModel;
using System.Windows.Input;

public class ProdusViewModel : INotifyPropertyChanged
{
    private Produs produs;

    public Produs Produs
    {
        get { return produs; }
        set
        {
            produs = value;
            OnPropertyChanged("Produs");
        }
    }

    public ICommand AdaugaProdusCommand { get; set; }
    public ICommand EditeazaProdusCommand { get; set; }
    public ICommand StergeProdusCommand { get; set; }

    public ProdusViewModel()
    {
        Produs = new Produs(); // Inițializare produs

        // Inițializare comenzi
        AdaugaProdusCommand = new RelayCommand(AdaugaProdus);
        EditeazaProdusCommand = new RelayCommand(EditeazaProdus);
        StergeProdusCommand = new RelayCommand(StergeProdus);
    }

    // Metoda pentru adăugarea unui produs
    private void AdaugaProdus(object obj)
    {
        // Implementare logica pentru adăugarea produsului în baza de date
    }

    // Metoda pentru editarea unui produs
    private void EditeazaProdus(object obj)
    {
        // Implementare logica pentru editarea produsului în baza de date
    }

    // Metoda pentru ștergerea unui produs
    private void StergeProdus(object obj)
    {
        // Implementare logica pentru ștergerea produsului din baza de date
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
