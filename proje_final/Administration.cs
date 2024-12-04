using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje_final
{
    internal class Administration : INotifyPropertyChanged
    {
        public int Id { get=>id; set { id = value; this.onPropertyChanged(nameof(Id)); } }
        public string Nom {  get=> nom; set { nom = value; this.onPropertyChanged(nameof(Nom)); } }
        public string Mdp { get=>mdp; set { mdp = value; this.onPropertyChanged(Mdp); } }

        public int id;
        public string nom;
        public string mdp;
        public Administration(int id, string nom, string mdp) {
            this.id = id;
            this.nom = nom;
            this.mdp = mdp;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
