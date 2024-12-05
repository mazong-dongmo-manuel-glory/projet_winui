using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje_final
{
 
    internal class Activites : INotifyPropertyChanged
    {
        public string nom;
        public int categorie_id;
        public int prixCout;
        public int prixVente;
        public int id;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Nom { get=>nom; set { 
               nom = value;

            } }
        public int Categorie_id { get => categorie_id; set { categorie_id = value; OnPropertyChanged(nameof(Nom)); } }
        public int PrixCout { get => prixCout; set { prixCout = value; OnPropertyChanged(nameof(PrixCout)); } }
        public int PrixVente { get => prixVente; set { prixVente = value; OnPropertyChanged(nameof(PrixVente)); } }
        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }

        public Activites(int id, string  nom, int categorie_id, int prixCout, int prixVente)
        {
            this.nom = nom;
            this.categorie_id = categorie_id;
            this.prixCout = prixCout;
            this.prixVente = prixVente;
            this.id = id;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
