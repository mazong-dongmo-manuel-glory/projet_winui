using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

namespace proje_final
{
    public sealed partial class DialogueEditAdherent : ContentDialog
    {
        // Propriétés pour stocker les données saisies
        public string Nom { get; private set; }
        public string Prenom { get; private set; }
        public string Numero { get; private set; }
        public DateTime DateNaissance { get; private set; }
        public string Adresse { get; private set; }
        Adherent adherent;

        public DialogueEditAdherent(string numero)
        {
            this.InitializeComponent();
            this.Numero = numero;
          
            adherent = Singleton.getInstance().getAdherent(numero);
            txtNom.Text = adherent.Nom;
            txtPrenom.Text = adherent.Prenom;
            DateTime d = DateTime.Parse(adherent.DateNaissance);
            dpDateNaissance.Date = d;
            txtAdresse.Text = adherent.Adresse;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            txtErreur.Visibility = Visibility.Collapsed;

            string dateNaissance = dpDateNaissance.SelectedDate.HasValue
                                   ? dpDateNaissance.SelectedDate.Value.ToString("yyyy-MM-dd")
                                   : string.Empty;

            var errors = Adherent.ValidateAdherent(
                txtNom.Text,
                txtPrenom.Text,
                dateNaissance,
                txtAdresse.Text
            );

            if (errors.Length != 0)
            {
                txtErreur.Text = string.Join("\n", errors); 
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;

                return;
            }
            adherent.Nom = txtNom.Text;
            adherent.Prenom = txtPrenom.Text;
            adherent.Numero = Numero;
            adherent.Adresse = txtAdresse.Text;
            adherent.DateNaissance = dpDateNaissance.Date.ToString();
            Singleton.getInstance().updateAdherents(adherent);
        }

      
    }
}
