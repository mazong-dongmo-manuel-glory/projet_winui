using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace proje_final
{
    public sealed partial class DialogueAjouteSeance : ContentDialog
    {
        public DialogueAjouteSeance()
        {
            this.InitializeComponent();

            Singleton.getInstance().getActivites();
            var activitesList = Singleton.getInstance().activiteListe;
            comboBoxActivites.ItemsSource = activitesList;
            comboBoxActivites.DisplayMemberPath = "Nom"; 
            comboBoxActivites.SelectedValuePath = "Id"; 
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            string nombrePlace = txtNombrePlace.Text.Trim();
            DateTime? dateSeance = datePickerSeance.Date.Date;
            TimeSpan? heureSeance = timePickerSeance.Time;

            if (comboBoxActivites.SelectedItem == null)
            {
                txtErreur.Text = "Veuillez sélectionner une activité.";
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }
            var activiteSelectionnee = (Activites)comboBoxActivites.SelectedItem;
            int idActivite = activiteSelectionnee.Id;

            // Liste d'erreurs
            var errors = new List<string>();

           

            if (!int.TryParse(nombrePlace, out int nombrePlaces))
            {
                errors.Add("Le nombre de places doit être un nombre valide.");
            }

            if (dateSeance == null)
            {
                errors.Add("La date de la séance est requise.");
            }
            else if (dateSeance <= DateTime.Now.Date) 
            {
                errors.Add("La date de la séance doit être supérieure à la date actuelle.");
            }

            if (heureSeance == null)
            {
                errors.Add("L'heure de la séance est requise.");
            }

            if (errors.Count > 0)
            {
                txtErreur.Text = string.Join("\n", errors);
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }

            var nouvelleSeance = new Seances();
            nouvelleSeance.IdActivite = idActivite;
            nouvelleSeance.NombrePlace = nombrePlaces;
            nouvelleSeance.NombrePlaceRestante = nombrePlaces;
            nouvelleSeance.HeureOrganisation = heureSeance.ToString();
            nouvelleSeance.DateOrganisation = dateSeance.ToString();


            Singleton.getInstance().AjouterSeance(nouvelleSeance);

            this.Hide();
        }
    }
}
