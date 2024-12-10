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

            // R�cup�re les s�ances disponibles pour l'activit�
            var seancesDisponibles = Singleton.getInstance().GetSeancesDisponiblesPourActivite(idActivite);

            // Associe les s�ances � la liste d�roulante ou ListView
            SeancesListView.ItemsSource = seancesDisponibles;

            // Si aucune s�ance n'est disponible, affiche un message
            if (seancesDisponibles.Count == 0)
            {
                txtErreur.Text = "Aucune s�ance disponible pour cette activit�.";
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
                txtErreur.Text = "Veuillez s�lectionner une s�ance.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

            // R�duire le nombre de places restantes
            if (selectedSeance.NombrePlaceRestante > 0)
            {
                selectedSeance.NombrePlaceRestante--;
                Singleton.getInstance().UpdateSeance(selectedSeance); // Mettez � jour dans la base de donn�es
            }
            else
            {
                txtErreur.Text = "Cette s�ance n'a plus de places disponibles.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

            // G�n�rer un num�ro de confirmation al�atoire
            string numeroConfirmation = $"CONF-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            
            this.Hide();

            // Message de confirmation
            await new ContentDialog
            {
                Title = "S�ance ajout�e",
                Content = $"Votre inscription pour la s�ance du {selectedSeance.DateOrganisationAffichage} � {selectedSeance.HeureOrganisation} a �t� confirm�e avec succ�s !\n\nNum�ro de confirmation : {numeroConfirmation}",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            }.ShowAsync();
        }

    }
}
