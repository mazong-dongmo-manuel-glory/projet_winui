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
    public sealed partial class DialogueAjouteAdherent : ContentDialog
    {
        // Propriétés pour stocker les données saisies
        public string Nom { get; private set; }
        public string Prenom { get; private set; }
        public string Numero { get; private set; }
        public DateTime DateNaissance { get; private set; }
        public string Adresse { get; private set; }
        Adherent adherent;
        public DialogueAjouteAdherent()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            txtErreur.Visibility = Visibility.Collapsed;
            adherent = new Adherent("","","","","");

            string dateNaissance = dpDateNaissance.SelectedDate.HasValue
                                   ? dpDateNaissance.SelectedDate.Value.ToString("yyyy-MM-dd")
                                   : string.Empty;

            var errors = Adherent.ValidateAdherent(
                txtNom.Text,
                txtPrenom.Text,
                dateNaissance,
                txtAdresse.Text
            );

            if (errors.Length != 0)
            {
                txtErreur.Text = string.Join("\n", errors);
                txtErreur.Visibility = Visibility.Visible;
                args.Cancel = true;

                return;
            }
            adherent.Nom = txtNom.Text;
            adherent.Prenom = txtPrenom.Text;
            adherent.Numero = Numero;
            adherent.Adresse = txtAdresse.Text;
            adherent.DateNaissance = dpDateNaissance.Date.ToString("yyyy-MM-dd HH:mm:ss");
            Singleton.getInstance().ajouterAdherent(adherent);
            Singleton.getInstance().getAdherents();
          
        }
    }
}
