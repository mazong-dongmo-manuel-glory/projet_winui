using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;



namespace proje_final
{

    public sealed partial class PageGestionActivite : Page
    {
        ObservableCollection<Activites> activites;
        public PageGestionActivite()
        {
            var singleton = Singleton.getInstance();

            this.InitializeComponent();
            Singleton.getInstance().getActivites();
            Singleton.getInstance().getSeances();
            singleton.getParticipations(); 
            activites = Singleton.getInstance().activiteListe;
            gridActivites.ItemsSource = activites;

            ChargerActivitesInscrites();



            // Calculer les moyennes des notes
            var moyennesNotes = singleton.GetMoyenneNotesParActivite();
            foreach (var activite in singleton.activiteListe)
            {
                if (moyennesNotes.ContainsKey(activite.Id))
                {
                    activite.MoyenneNote = moyennesNotes[activite.Id];
                }
            }

            activites = singleton.activiteListe;
            gridActivites.ItemsSource = activites;

        }


        private async void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var numero = int.Parse(btn.Tag.ToString());
            DialogueEditActivite dialogEditActivite = new DialogueEditActivite(numero);
            dialogEditActivite.XamlRoot = this.XamlRoot;
            await dialogEditActivite.ShowAsync();
           
        }
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var id = btn.Tag.ToString();
            Singleton.getInstance().deleteActivite(int.Parse(id));
            Singleton.getInstance().getSeances();

        }

        private async void app_bar_icon_add_Click(object sender, RoutedEventArgs e)
        {
            DialogAjoutActivite dialogAjouteActivite = new DialogAjoutActivite();
            dialogAjouteActivite.XamlRoot = this.XamlRoot;
            await dialogAjouteActivite.ShowAsync();
        }

        public Visibility CanDisplay2 { get { return Singleton.getInstance().Admin == null ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility CanDisplayAdherent { get { return Singleton.getInstance().adherent == null ? Visibility.Collapsed : Visibility.Visible; } }


        private async void Inscription_activite_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int activiteId = int.Parse(button.Tag.ToString());

            DialogueInscriptionActivite dialogueInscriptionActivite = new DialogueInscriptionActivite(activiteId)
            {
                XamlRoot = this.XamlRoot
            };
            await dialogueInscriptionActivite.ShowAsync();
        }

        private void ChargerActivitesInscrites()
        {
            var singleton = Singleton.getInstance();
            var adherentConnecte = singleton.adherent;

            if (adherentConnecte != null)
            {
                Debug.WriteLine($"Adhérent connecté : {adherentConnecte.Nom} ({adherentConnecte.Numero})");

                // Récupérer les séances auxquelles l'utilisateur est inscrit
                var seancesInscrites = singleton.GetSeancesParAdherent(adherentConnecte.Numero);

                Debug.WriteLine($"Nombre de séances trouvées : {seancesInscrites.Count}");

                // Définir les données pour la ComboBox
                comboBoxActivitesInscrites.ItemsSource = seancesInscrites;
            }
            else
            {
                Debug.WriteLine("Aucun adhérent connecté.");
                comboBoxActivitesInscrites.ItemsSource = null;
            }
        }

        private async void comboBoxActivitesInscrites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSeance = comboBoxActivitesInscrites.SelectedItem as Seances;
            if (selectedSeance != null)
            {
                var singleton = Singleton.getInstance();
                var participation = singleton.participationsListe.FirstOrDefault(p =>
                    p.IdSeance == selectedSeance.Id &&
                    p.NumeroAdherent == singleton.adherent.Numero);

                if (participation != null)
                {
                    if (participation.Note > 0)
                    {
                        ContentDialog alert = new ContentDialog
                        {
                            Title = "Activité déjà notée",
                            Content = $"Vous avez déjà donné une note de {participation.Note}/20 pour cette activité.",
                            CloseButtonText = "OK",
                            XamlRoot = this.XamlRoot
                        };
                        await alert.ShowAsync();
                        return;
                    }

                    // Ouvrir le dialogue pour noter
                    DialogueNoterActivite dialog = new DialogueNoterActivite(participation.IdParticipation);
                    dialog.XamlRoot = this.XamlRoot;

                    var result = await dialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        Debug.WriteLine($"Note sélectionnée pour la mise à jour : {dialog.Note}");
                        bool succes = singleton.ModifierNoteParticipation(participation.IdParticipation, dialog.Note);
                        if (succes)
                        {
                            Debug.WriteLine($"Note mise à jour avec succès : {dialog.Note}");
                        }
                        else
                        {
                            Debug.WriteLine("Erreur lors de la mise à jour de la note.");
                        }
                    }
                }
            }
        }



    }
}
