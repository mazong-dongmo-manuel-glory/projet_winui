using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace proje_final
{
    public sealed partial class DialogueAjouteSeance : ContentDialog
    {
        public DialogueAjouteSeance()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            txtErreur.Visibility = Visibility.Collapsed; // Cache le message d'erreur au début
           
            string nomSeance = txtNom.Text.Trim();
            string coutOrganisation = txtCoutOrganisation.Text.Trim();
            string prixParticipant = txtPrixParticipant.Text.Trim();
            DateTime? dateSeance = datePickerSeance.Date.Date;
            TimeSpan? heureSeance = timePickerSeance.Time;

            // Validation des champs
            if (string.IsNullOrEmpty(nomSeance))
            {
                txtErreur.Text = "Le nom de la séance est requis.";
                txtErreur.Visibility = Visibility.Visible;
                return;
            }

            if (!decimal.TryParse(coutOrganisation, out _))
            {
                txtErreur.Text = "Le coût d'organisation doit être un nombre valide.";
                txtErreur.Visibility = Visibility.Visible;
                return;
            }

            if (!decimal.TryParse(prixParticipant, out _))
            {
                txtErreur.Text = "Le prix de participation doit être un nombre valide.";
                txtErreur.Visibility = Visibility.Visible;
                return;
            }

            if (dateSeance == null)
            {
                txtErreur.Text = "La date de la séance est requise.";
                txtErreur.Visibility = Visibility.Visible;
                return;
            }

            if (heureSeance == null)
            {
                txtErreur.Text = "L'heure de la séance est requise.";
                txtErreur.Visibility = Visibility.Visible;
                return;
            }
            

            // Si tout est valide, traiter l'ajout de la séance
            // Exemple : Appeler une méthode pour sauvegarder les données
            AjouterSeance(nomSeance, coutOrganisation, prixParticipant, dateSeance.Value, heureSeance.Value);
        }

        private void AjouterSeance(string nom, string cout, string prix, DateTime date, TimeSpan heure)
        {
            // Exemple de logique pour ajouter la séance
            // Implémentez votre code de sauvegarde ici

            // Afficher un message de confirmation (à adapter)
           
        }
    }

}

