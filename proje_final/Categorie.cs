﻿using Microsoft.UI.Xaml;
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
        public string Nom { get=>nom; set { nom=value; this.OnPropertyChanged(nameof(Nom)); } }
        
        public string ImageLink { get => imageLink; set { imageLink = value; this.OnPropertyChanged(nameof(ImageLink)); } }
        public int Id { get => id; set { id = value; this.OnPropertyChanged(nameof(Id)); } }

        public Categorie(string nom, string imageLink, int id)
        {
            this.nom = nom;
            this.imageLink = imageLink;
            this.id = id;
        }

        public Visibility CanDisplay { get { return Singleton.getInstance().Admin == null ? Visibility.Collapsed : Visibility.Visible; } }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
