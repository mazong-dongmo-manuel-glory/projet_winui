using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace proje_final
{
    internal class Adherent : INotifyPropertyChanged
    {
        public string Nom { get => nom; set { nom = value; this.onPropertyChanged(nameof(Nom)); } }
        public string Numero { get => numero; set { numero = value; this.onPropertyChanged(nameof(Numero)); } }
        public string Prenom { get => prenom; set { prenom = value; this.onPropertyChanged(nameof(Prenom)); } }
        public string DateNaissance { get => dateNaissance; set { dateNaissance = value; this.onPropertyChanged(nameof(DateNaissance)); } }
        public string Adresse { get => adresse; set { adresse = value; this.onPropertyChanged(nameof(Adresse)); } }

        public string nom;
        public string prenom;
        public string numero;
        public string dateNaissance;
        public string adresse;

        public Adherent(string numero, string nom, string prenom, string dateNaissance, string adresse)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
            this.numero = numero;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static string[] ValidateAdherent(string nom, string prenom, string dateNaissance, string adresse)
        {
            List<string> errors = new List<string>();


            if (string.IsNullOrWhiteSpace(nom) || !Regex.IsMatch(nom, @"^[A-Za-zÀ-ÿ\s]+$"))
            {
                errors.Add("Le nom ne peut pas être vide et doit contenir uniquement des lettres.");
            }

            if (string.IsNullOrWhiteSpace(prenom) || !Regex.IsMatch(prenom, @"^[A-Za-zÀ-ÿ\s]+$"))
            {
                errors.Add("Le prénom ne peut pas être vide et doit contenir uniquement des lettres.");
            }

            if (!DateTime.TryParse(dateNaissance, out DateTime parsedDate))
            {
                errors.Add("La date de naissance n'est pas valide.");
            }
            else if (parsedDate > DateTime.Now)
            {
                errors.Add("La date de naissance ne peut pas être dans le futur.");
            }

            if (string.IsNullOrWhiteSpace(adresse))
            {
                errors.Add("L'adresse ne peut pas être vide.");
            }

            return errors.ToArray();
        }
    }
}
