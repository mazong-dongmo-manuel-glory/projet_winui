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
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace proje_final
{

    public sealed partial class PageAdministration : Page
    {
        public PageAdministration()
        {
            this.InitializeComponent();
        }
        private void VoirAdherents_Click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionAdherent));
        }
        private void VoirCategories_Click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionCategorie));
        } 
        private void VoirActivites_click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionActivite));
        }
        private void VoirSeances_click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionSeance));
        }
        private void btn_statistique_Click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageStatistiques));
        }

        private async void btn_export_adherent_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Singleton.mainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "adherents";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();
            if (monFichier == null)
            {
                return; 
            }

    

            var csvLines = new List<string> { "Numero;Nom;Prenom;DateNaissance;Adresse" };
            Singleton.getInstance().getAdherents();
            foreach (var adherent in Singleton.getInstance().adherentListe) 
            {
                csvLines.Add($"{adherent.Numero};\"{adherent.Nom}\";\"{adherent.Prenom}\";\"{adherent.DateNaissance}\";\"{adherent.Adresse}\"");
            }

            try
            {
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, csvLines, Windows.Storage.Streams.UnicodeEncoding.Utf8);

            }
            catch (Exception)
            {

            }
        }

        private async void btn_export_activite_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Singleton.mainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "Activites";
            picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();
            if (monFichier == null)
            {
                return;
            }
            var csvLines = new List<string> { "Id;Nom;CategorieId;PrixCout;PrixVente;ImageLink;CategorieNom;MoyenneNote" };
            Singleton.getInstance().getActivites();
            foreach (var activite in Singleton.getInstance().activiteListe)
            {
                csvLines.Add($"{activite.Id};\"{activite.Nom}\";{activite.CategorieId};{activite.PrixCout};{activite.PrixVente};\"{activite.ImageLink}\";\"{activite.CategorieNom}\";{activite.MoyenneNote:F2}");
                Debug.WriteLine(activite.Nom);
            }
            try
            {
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, csvLines, Windows.Storage.Streams.UnicodeEncoding.Utf8);

            }catch(Exception)
            {

            }

        }
    }
}
