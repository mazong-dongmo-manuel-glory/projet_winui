using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace proje_final
{
    
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            Singleton.mainWindow = this;
            Singleton.mainFrame = mainFrame;
            initTestAdmin();
            Singleton.getInstance().getAdherents();
            Singleton.getInstance().getAllCategories();

           
        }
        private void initTestAdmin()
        {
            Singleton.getInstance().connexionAdministrateur("Administrateur1", "mdp1234");
            iConnexion.Content = "Se déconnecté";
           
            nv.SelectedItem = iAccueil;
            headerTextBlock.Text = $"Bienvenue, {Singleton.getInstance().administration.Nom}";
            mainFrame.Navigate(typeof(PageAdministration));
        }

        private void  NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem  nav = args.SelectedItem as NavigationViewItem;

            switch (nav.Name)
            {
                case "iAccueil":
                   
                    var administration = Singleton.getInstance().administration;
                    var adherent = Singleton.getInstance().adherent;

                    if (administration != null)
                    {
                        app_bar_icon.IsEnabled = true;
                        mainFrame.Navigate(typeof(PageAdministration));
                    }
                    else if (adherent != null)
                    {
                        headerTextBlock.Text = $"Bienvenue, {Singleton.getInstance().adherent.Nom}";
                        app_bar_icon.IsEnabled = true;
                        mainFrame.Navigate(typeof(PageAdherent));
                    } else
                    {
                        
                        headerTextBlock.Text = "Espace visiteur";
                        mainFrame.Navigate(typeof(PageBlanche));
                    }

                    break;

                case "iConnexion":

                    if (Singleton.getInstance().adherent != null || Singleton.getInstance().administration != null)
                    {
                        iConnexion.Content = "Se connecté";
                        Singleton.getInstance().deconnexion();
                        mainFrame.Content = null;
                        mainFrame.Navigate(typeof(PageBlanche));
                        UpdateHeader();
                        showConnexionDialog();
                        break;
                    }
                    else
                    {
                        iConnexion.Content = "Se déconnecté";
                        showConnexionDialog();
                    }
                    break;
               
            }

        }
 
    

    private async void showConnexionDialog()
        {
            DialogueConnexion dialog = new DialogueConnexion();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.Title = "Connexion";
            dialog.PrimaryButtonText = "Se connecter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            ContentDialogResult resultat = await dialog.ShowAsync();
            UpdateHeader();
    }
    /* Mise a jour du header et navigation a la page correspondante */
    private void UpdateHeader()
        {
            var adherent = Singleton.getInstance().adherent;
            var administration = Singleton.getInstance().administration;
            if (adherent != null)
            {
                app_bar_icon.IsEnabled = true;
                headerTextBlock.Text = $"Bienvenue, {Singleton.getInstance().adherent.Nom}";
                mainFrame.Navigate(typeof(PageAdherent));
                iConnexion.Content = "Déconnexion";
            }
            else if (administration != null)
            {
                app_bar_icon.IsEnabled = true;
                headerTextBlock.Text = $"Bienvenue, {administration.Nom}";
                mainFrame.Navigate(typeof(PageAdministration));
                iConnexion.Content = "Déconnexion";

            }
            else
            {
                headerTextBlock.Text = "Non connecté";
            }
            nv.SelectedItem = iAccueil;

        }

        private void app_bar_icon_Click(object sender, RoutedEventArgs e)
        {
            try {
                var administration = Singleton.getInstance().administration;
                var adherent = Singleton.getInstance().adherent;

                if (administration != null)
                {
                    app_bar_icon.IsEnabled = true;
                    mainFrame.Navigate(typeof(PageAdministration));
                } else if (adherent != null)
                {
                    app_bar_icon.IsEnabled = true;
                    mainFrame.Navigate(typeof(PageAdherent));
                } else
                {
                    mainFrame.Navigate(typeof(PageBlanche));
                }

            }
            catch (Exception ex) {

            }
        }
    }


}
