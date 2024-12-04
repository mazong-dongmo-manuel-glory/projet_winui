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
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace proje_final
{



    public sealed partial class DialogueCategorie : ContentDialog
    {
        public string Nom;
        public string ImageLinkV;
        public DialogueCategorie()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var errors = new List<string>();
            var categorie = new Categorie(NomCategorie.Text, ImageLink.Text,0);
            var imageLinkPattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*\.(jpg|jpeg|png|webp)(\?[^\s]*)?$";


            if (NomCategorie.Text.Length < 3)
            {
                errors.Add("Le nom de la categorie n'est pas valide");
            }
            if (!Regex.IsMatch(ImageLink.Text, imageLinkPattern,RegexOptions.IgnoreCase))
            {
                errors.Add("L'adresse de l'image n'est pas valide");
            }
            if(errors.Count != 0)
            {
                ErrorMessage.Text = String.Join("\n",errors.ToArray());
                ErrorMessage.Visibility = Visibility;
                args.Cancel = true;
            }
            else
            {
                Singleton.getInstance().ajouterCategorie(categorie);
                Singleton.getInstance().getAllCategories();

            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
