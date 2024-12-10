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


namespace proje_final
{

    public sealed partial class DialogueInscriptionActivite : ContentDialog
    {
        public DialogueInscriptionActivite(int idActivite)
        {
            this.InitializeComponent();
            Singleton.getInstance().getSeances();

            // Récupère les séances disponibles pour l'activité
            var seancesDisponibles = Singleton.getInstance().GetSeancesDisponiblesPourActivite(idActivite);

            // Associe les séances à la liste déroulante ou ListView
            SeancesListView.ItemsSource = seancesDisponibles;

            // Si aucune séance n'est disponible, affiche un message
            if (seancesDisponibles.Count == 0)
            {
                txtErreur.Text = "Aucune séance disponible pour cette activité.";
                txtErreur.Visibility = Visibility.Visible;
            }
            else
            {
                txtErreur.Visibility = Visibility.Collapsed;
            }
        }



        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selectedSeance = SeancesListView.SelectedItem as Seances;

            if (selectedSeance == null)
            {
                txtErreur.Text = "Veuillez sélectionner une séance.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

            // Réduire le nombre de places restantes
            if (selectedSeance.NombrePlaceRestante > 0)
            {
                selectedSeance.NombrePlaceRestante--;
                Singleton.getInstance().UpdateSeance(selectedSeance); // Mettez à jour dans la base de données
            }
            else
            {
                txtErreur.Text = "Cette séance n'a plus de places disponibles.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

            // Générer un numéro de confirmation aléatoire
            string numeroConfirmation = $"CONF-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            
            this.Hide();

            // Message de confirmation
            await new ContentDialog
            {
                Title = "Séance ajoutée",
                Content = $"Votre inscription pour la séance du {selectedSeance.DateOrganisationAffichage} à {selectedSeance.HeureOrganisation} a été confirmée avec succès !\n\nNuméro de confirmation : {numeroConfirmation}",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            }.ShowAsync();
        }

    }
}
