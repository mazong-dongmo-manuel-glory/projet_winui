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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace proje_final
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                    break;
                case "iConnexion":
                    if (Singleton.getInstance().adherent != null || Singleton.getInstance().administration != null)
                    {
                        nv.SelectedItem = iAccueil;
                    }
                    else
                    {
                        showConnexionDialog();

                    }
                    break;
                case "iDeconnexion":
                    Singleton.getInstance().deconnexion();
                    mainFrame.Content = null;
                    nv.SelectedItem = iAccueil;
                    UpdateHeader();
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
            dialog.DefaultButton = ContentDialogButton.Close;
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
                headerTextBlock.Text = $"Bienvenue, {adherent.Nom}";
                mainFrame.Navigate(typeof(PageAdherent));

            }
            else
            {
                headerTextBlock.Text = "Non connecte";

            }

            if (administration != null)
            {
                headerTextBlock.Text = $"Bienvenue, {administration.Nom}";
                mainFrame.Navigate(typeof(PageAdministration));
            }

            else
            {
                headerTextBlock.Text = "Non connecte";

            }
            nv.SelectedItem = iAccueil;



        }

        private void app_bar_icon_Click(object sender, RoutedEventArgs e)
        {
            try { 
                mainFrame.GoBack();
            }
            catch (Exception ex) {
            }
        }
    }


}
