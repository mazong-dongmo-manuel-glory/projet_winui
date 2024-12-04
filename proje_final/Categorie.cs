using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje_final
{
    internal class Categorie : INotifyPropertyChanged
    {
        public string nom; public string imageLink;  public  int id;
        public string Nom { get=>nom; set { value = nom; this.OnPropertyChanged(nameof(Nom)); } }
        
        public string ImageLink { get => imageLink; set { value = imageLink; this.OnPropertyChanged(nameof(ImageLink)); } }
        public int Id { get => id; set { value = id; this.OnPropertyChanged(nameof(Id)); } }

        public Categorie(string nom, string imageLink, int id)
        {
            this.nom = nom;
            this.imageLink = imageLink;
            this.id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
        }
    }
}
