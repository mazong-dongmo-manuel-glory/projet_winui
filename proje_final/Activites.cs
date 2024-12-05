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
        public string imageLink;
        public string categorie_nom;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Nom { get=>nom; set { 
               nom = value;

            } }
        public string ImageLink { get => imageLink; set { imageLink = value; OnPropertyChanged(nameof(ImageLink)); } }
        public int CategorieId { get => categorie_id; set { categorie_id = value; OnPropertyChanged(nameof(CategorieId)); } }
        public int PrixCout { get => prixCout; set { prixCout = value; OnPropertyChanged(nameof(PrixCout)); } }
        public int PrixVente { get => prixVente; set { prixVente = value; OnPropertyChanged(nameof(PrixVente)); } }
        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string CategorieNom { get => categorie_nom; set { categorie_nom = value; OnPropertyChanged(nameof(CategorieNom)); } }
        public Activites(int id, string  nom, int categorie_id, int prixCout, int prixVente, string imageLink, string categorie_nom)
        {
            this.nom = nom;
            this.categorie_id = categorie_id;
            this.prixCout = prixCout;
            this.prixVente = prixVente;
            this.id = id;
            this.imageLink = imageLink;
            this.categorie_nom = categorie_nom;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
