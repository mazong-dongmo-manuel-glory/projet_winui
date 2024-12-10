using Microsoft.UI.Xaml;
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
        public string IdAdherent { get; set; }
        public string nomActivite;
        public string dateOrganisation;
        public string heureOrganisation;
        public int nombrePlace;
        public int nombrePlaceRestante;
        public string imageLink;


        public int Id { get => id; set { id = value; } }
        public int IdActivite { get=> idActivite; set { idActivite = value; OnPropertyChanged(nameof(IdActivite)); } }
        public string DateOrganisation { get => dateOrganisation; set { dateOrganisation = value; OnPropertyChanged(nameof(DateOrganisation)); OnPropertyChanged(DateOrganisationAffichage); } }
        public string HeureOrganisation { get => heureOrganisation; set { heureOrganisation = value; OnPropertyChanged(nameof(HeureOrganisation)); OnPropertyChanged(nameof(HeureOrganisationAffichage)); } }
        public int NombrePlace { get => nombrePlace; set { nombrePlace = value; OnPropertyChanged(nameof(NombrePlace)); OnPropertyChanged(nameof(NombrePlaceAffichage)); } }
        public int NombrePlaceRestante { get => nombrePlaceRestante; set { nombrePlaceRestante = value; OnPropertyChanged(nameof(NombrePlaceRestante)); OnPropertyChanged(nameof(NombrePlaceRestanteAffichage)); } }
        public string ImageLink { get => imageLink; set { imageLink = value; OnPropertyChanged(ImageLink); } }
        public string NomActivite { get => nomActivite; set { nomActivite = value; OnPropertyChanged(nameof(NomActivite)); OnPropertyChanged(NomSeance); } }
        public string NomSeance { get => NomActivite + ": " + Id; }
        public string DateOrganisationAffichage
        {
            get
            {
                
                if (DateTime.TryParse(dateOrganisation, out DateTime parsedDate))
                {
                    return "Date : " + parsedDate.ToString("yyyy-MM-dd");
                }
                return "Date invalide";
            }
        }
        public string NombrePlaceAffichage { get => "Le nombre de place maximum est : " + NombrePlace; }
        public string NombrePlaceRestanteAffichage { get => "Le nombre de place restante est : " + NombrePlaceRestante; }
        public string HeureOrganisationAffichage { get => "Heure  : " + HeureOrganisation; }


        public Visibility CanDisplay { get { return Singleton.getInstance().Admin == null ? Visibility.Collapsed : Visibility.Visible; } }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
