using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje_final
{
    internal class Seances : INotifyPropertyChanged
    {
        public int id;
        public int idActivite;
        public string nomActivite;
        public string dateOrganisation;
        public string heureOrganisation;
        public int nombrePlace;
        public int nombrePlaceRestante;

        public int Id { get => id; set { id = value; } }
        public int IdActivite { get=> idActivite; set { idActivite = value; OnPropertyChanged(nameof(IdActivite)); } }
        public string DateOrganisation { get => dateOrganisation; set { dateOrganisation = value; OnPropertyChanged(nameof(DateOrganisation)); } }
        public string HeureOrganisation { get => heureOrganisation; set { heureOrganisation = value; OnPropertyChanged(nameof(HeureOrganisation)); } }
        public int NombrePlace { get => nombrePlace; set { nombrePlace = value; OnPropertyChanged(nameof(NombrePlace)); } }
        public int NombrePlaceRestante { get => nombrePlaceRestante; set { nombrePlaceRestante = value; OnPropertyChanged(nameof(NombrePlaceRestante)); } }
        public string NomActivite { get => nomActivite; set { nomActivite = value; OnPropertyChanged(nameof(NomActivite)); } }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
