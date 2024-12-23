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
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace proje_final
{
    public sealed partial class DialogAjoutActivite : ContentDialog
    {
        ObservableCollection<Categorie> categories;
        Activites activite;
        public DialogAjoutActivite()
        {
            this.InitializeComponent();
            categories = Singleton.getInstance().categorieListe;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int prixCout = 0;
            int prixVente = 0;
            List<string> errors = new List<string>();
            try
            {
                prixVente = int.Parse(txtPrixParticipant.Text);
                if (prixVente < 1)
                {
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
                foreach (var act in Singleton.getInstance().activiteListe)
                {
                    if (act.Nom == txtNom.Text)
                    {
                        errors.Add("Une activite avec ce nom existe deja");
                    }
                }
            }
            if (cmbCategories.SelectedIndex < 0)
            {
                errors.Add("Selectionner une categorie");
            }
            if (errors.Count != 0)
            {
                txtErreur.Visibility = Visibility;
                txtErreur.Text = String.Join("\n", errors.ToArray());
                args.Cancel = true;
            }
            else
            {
                var activite = new Activites(0,"",0,0,0,"","");
                
                var cat = cmbCategories.SelectedItem as Categorie;
                activite.Nom = txtNom.Text;
                activite.PrixVente = prixVente;
                activite.prixCout = prixCout;
                activite.CategorieId = cat.Id;
                activite.CategorieNom = cat.Nom;
                activite.ImageLink = cat.ImageLink;
                Singleton.getInstance().ajoutActivite(activite);
            }
        }
    
    }
}
