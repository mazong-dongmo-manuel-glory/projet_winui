using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

         
            var seancesDisponibles = Singleton.getInstance().GetSeancesDisponiblesPourActivite(idActivite);

          
            SeancesListView.ItemsSource = seancesDisponibles;

       
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
            var bd = Singleton.getInstance();

            if (selectedSeance == null)
            {
                txtErreur.Text = "Veuillez s�lectionner une s�ance.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

      
            if (bd.adherent == null)
            {
                txtErreur.Text = "Erreur : Aucun adh�rent connect�.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

          
            if (bd.EstDejaInscrit(selectedSeance.Id, bd.adherent.Numero))
            {
                txtErreur.Text = "Vous �tes d�j� inscrit � cette s�ance.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

            
            var nouvelleParticipation = new Participation
            {
                IdSeance = selectedSeance.Id,
                NumeroAdherent = Singleton.getInstance().adherent.Numero, 
                Note = 0
            };

         
            Debug.WriteLine($"Nouvelle participation : Seance={nouvelleParticipation.IdSeance}, Adherent={nouvelleParticipation.NumeroAdherent}");


            if (string.IsNullOrEmpty(Singleton.getInstance().adherent.Numero))
            {
                txtErreur.Text = "Erreur : Impossible de trouver le num�ro de l'adh�rent connect�.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }


            Debug.WriteLine($"Insertion dans la BD : IdSeance={nouvelleParticipation.IdSeance}, NumeroAdherent={nouvelleParticipation.NumeroAdherent}, Note={nouvelleParticipation.Note}");



            if (!bd.AjouterParticipation(nouvelleParticipation))
            {
                txtErreur.Text = "Une erreur s'est produite lors de l'inscription. Veuillez r�essayer.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

     
            if (selectedSeance.NombrePlaceRestante > 0)
            {
                selectedSeance.NombrePlaceRestante--;
                bd.UpdateSeance(selectedSeance);
            }
            else
            {
                txtErreur.Text = "Cette s�ance n'a plus de places disponibles.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

         
            string numeroConfirmation = $"CONF-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            this.Hide();

           
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
