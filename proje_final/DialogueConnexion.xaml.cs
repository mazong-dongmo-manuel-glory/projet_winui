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
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace proje_final
{
    public sealed partial class DialogueConnexion : ContentDialog
    {
        public DialogueConnexion()
        {
            this.InitializeComponent();
        }

        // Gérer le changement de sélection dans le ComboBox
        private void ComboBoxTypeUtilisateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selection = (ComboBoxTypeUtilisateur.SelectedItem as ComboBoxItem).Content.ToString();

            if (selection == "Administrateur")
            {
                PanelAdmin.Visibility = Visibility.Visible;
                PanelAdherent.Visibility = Visibility.Collapsed;
            }
            else if (selection == "Adhérent")
            {
                PanelAdmin.Visibility = Visibility.Collapsed;
                PanelAdherent.Visibility = Visibility.Visible;
            } else if ( selection == "Visiteur")
            {
                
                Singleton.mainFrame.Navigate(typeof(PageBlanche));
            }
        }

        // Gérer la validation de connexion lors du clic sur le bouton principal
        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if(ComboBoxTypeUtilisateur.SelectedIndex == -1)
            {
                this.Hide();
                return;
            }
            ContentDialog alert;
            
            
            if ((ComboBoxTypeUtilisateur.SelectedItem as ComboBoxItem).Content.ToString() == "Administrateur")
            {
                string nom = TextBoxAdminNom.Text;
                string mdp = PasswordBoxAdmin.Password;
                this.Hide();

                if (Singleton.getInstance().connexionAdministrateur(nom, mdp))
                {
                   alert = new ContentDialog
                    {
                        Title = "Connexion réussie",
                        Content = "Vous êtes maintenant connecté. "+Singleton.getInstance().administration.Nom,
                        CloseButtonText = "OK"
                    };
                    alert.XamlRoot = this.XamlRoot;
                    await alert.ShowAsync();


                }
                else
                {
                    alert = new ContentDialog
                    {
                        Title = "Connexion echoue",
                        Content = "Nom d'administrateur ou mot de passe incorrecte !",
                         CloseButtonText = "OK"
                    };
                    alert.XamlRoot = this.XamlRoot;
                    await alert.ShowAsync();

                }
            }
            else if ((ComboBoxTypeUtilisateur.SelectedItem as ComboBoxItem).Content.ToString() == "Adhérent")
            {
                this.Hide();

                string numero = TextBoxAdherentNumero.Text;
                if (Singleton.getInstance().connexionAdherent(numero))
                {
                    alert = new ContentDialog
                    {
                        Title = "Connexion réussie",
                        Content = "Vous êtes maintenant connecté. "+Singleton.getInstance().adherent.Nom,
                        CloseButtonText = "OK"
                    };

                    
                    alert.XamlRoot = this.XamlRoot;

                    await alert.ShowAsync();
                }
                else
                {
                    alert = new ContentDialog
                    {
                        Title = "Connexion echoue",
                        Content = "Numero identification incorrect !",
                        CloseButtonText = "OK"
                    };
                    alert.XamlRoot = this.XamlRoot;
                    await alert.ShowAsync();

                }
            }
        }
    }
}
