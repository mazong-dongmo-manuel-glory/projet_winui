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
    public sealed partial class DialogueEditSeance : ContentDialog
    {
        Seances seance;
        public DialogueEditSeance(int id)
        {
            this.InitializeComponent();
            seance = Singleton.getInstance().getSeance(id);

            txtNombrePlace.Text = seance.NombrePlace.ToString();  // Remplir le champ Nombre de places
            datePickerSeance.Date = DateTime.Parse(seance.DateOrganisation);    // Remplir le champ Date de la séance
            timePickerSeance.Time = TimeSpan.Parse(seance.HeureOrganisation);

        }
    
    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            string nombrePlace = txtNombrePlace.Text.Trim();
            DateTime? dateSeance = datePickerSeance.Date.Date;
            TimeSpan? heureSeance = timePickerSeance.Time;

           

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

            seance.NombrePlace = nombrePlaces;
            seance.NombrePlaceRestante = nombrePlaces;
            seance.HeureOrganisation = heureSeance.ToString();
            seance.DateOrganisation = dateSeance.ToString();
            Singleton.getInstance().UpdateSeance(seance);
            this.Hide();
        }
    }
}
