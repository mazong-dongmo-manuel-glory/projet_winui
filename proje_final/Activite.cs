using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje_final
{
    internal class Activite : INotifyPropertyChanged
    {
        public int id;
        public string nom;
        public int  categorie_id;
        public double prix_cout;
        public double prix_vente;

        public int Id { get=>id; set { value = id; this.onPropertyChanged(nameof(Id)); } }
        public string Nom { get=> nom; set { nom = value; this.onPropertyChanged(nameof(Nom)); } }
        public int CategorieId {  get=>categorie_id; set { value = categorie_id; this.onPropertyChanged(nameof(CategorieId)); } }
        public double PrixCout { get=>prix_cout; set { value = prix_cout; this.onPropertyChanged(nameof(PrixCout)); } }
        public double PrixVente { get=> prix_vente; set { value = prix_vente; this.onPropertyChanged(nameof(PrixVente)); }  }

        public Activite(int id, string nom, int categorieId, double prixCout, double prixVente)
        {
            this.id = id;
            this.nom = nom;
            this.categorie_id = categorieId;
            this.prix_cout = prixCout;
            this.prix_vente = prixVente;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
