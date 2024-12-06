using Microsoft.UI.Xaml;
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
                OnPropertyChanged(nameof(Nom));
            } }
        public string ImageLink { get => imageLink; set { imageLink = value; OnPropertyChanged(nameof(ImageLink)); } }
        public int CategorieId { get => categorie_id; set { categorie_id = value; OnPropertyChanged(nameof(CategorieId)); } }
        public int PrixCout { get => prixCout; set { prixCout = value; OnPropertyChanged(nameof(PrixCout)); } }
        public string PrixCoutAvecSigne { get => "Prix du coût : " + prixCout.ToString() + " $";}

        public int PrixVente { get => prixVente; set { prixVente = value; OnPropertyChanged(nameof(PrixVente)); } }
        public string PrixVenteAvecSigne { get => "Prix de vente : " + prixVente.ToString() + " $"; }

        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string CategorieNom { get => categorie_nom; set { categorie_nom = value; OnPropertyChanged(nameof(CategorieNom)); } }
        public string CategorieNomAvecTexte { get => "Catégorie : " + categorie_nom; }

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

        public Visibility CanDisplay3 { get { return Singleton.getInstance().Admin != null ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility CanDisplay { get { return Singleton.getInstance().Admin == null ? Visibility.Collapsed : Visibility.Visible; } }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
