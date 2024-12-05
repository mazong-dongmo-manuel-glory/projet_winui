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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace proje_final
{
    public sealed partial class DialogueEditActivite : ContentDialog
    {
        Activites activite;
        ObservableCollection<Categorie> categories;
        public DialogueEditActivite(int id)
        {
            this.InitializeComponent();
            categories = Singleton.getInstance().categorieListe;
            activite = Singleton.getInstance().getActivite(id);
            txtNom.Text = activite.Nom;
            txtCoutOrganisation.Text = activite.PrixCout.ToString();
            txtPrixParticipant.Text = activite.PrixVente.ToString();
         
           
            
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int prixCout;
            int prixVente;
            List<string> errors = new List<string>();
            try
            {
                prixVente = int.Parse(txtPrixParticipant.Text);
                if (prixVente < 1) {
                    errors.Add("Le prix de vente n'est pas valide");
                }
            }
            catch (Exception)
            {
                errors.Add("Le prix de vente doit etre un nombre");
            }
            try
            {
                prixCout = int.Parse(txtCoutOrganisation.Text);
                if (prixCout < 1)
                {
                    errors.Add("Le prix d'organisation n'est pas valide");
                }
            }
            catch (Exception)
            {
                errors.Add("Le prix d'organisation doit etre un nombre");
            }
            if (txtNom.Text.Length < 2)
            {
                errors.Add("Le nom de l'activite n'est pas valide");
                foreach(var act in Singleton.getInstance().activiteListe)
                {
                    if(act.Nom == txtNom.Text)
                    {
                        errors.Add("Une activite avec ce nom existe deja");
                    }
                }
            }
            if(cmbCategories.SelectedIndex < 0)
            {
                errors.Add("Selectionner une categorie");
            }
            if(errors.Count != 0)
            {
                txtErreur.Visibility = Visibility;
                txtErreur.Text = String.Join("\n", errors.ToArray());
                args.Cancel = true;
            }
            else
            {
                
            }
        }
    }
}
