using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje_final
{
    internal class Participation : INotifyPropertyChanged
    {

        private int idParticipation;
        private int idSeance;
        private string numeroAdherent;
        private int note;

        public int IdParticipation
        {
            get => idParticipation;
            set { idParticipation = value; OnPropertyChanged(nameof(IdParticipation)); }
        }

        public int IdSeance
        {
            get => idSeance;
            set { idSeance = value; OnPropertyChanged(nameof(IdSeance)); }
        }

        public string NumeroAdherent
        {
            get => numeroAdherent;
            set { numeroAdherent = value; OnPropertyChanged(nameof(NumeroAdherent)); }
        }

        public int Note
        {
            get => note;
            set { note = value; OnPropertyChanged(nameof(Note)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      




    }
}
