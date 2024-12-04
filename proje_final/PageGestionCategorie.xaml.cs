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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageGestionCategorie : Page
    {
        ObservableCollection<Categorie> categories;
        public PageGestionCategorie()
        {
            this.InitializeComponent();
            categories = Singleton.getInstance().categorieListe;
            gridCategories.ItemsSource = categories;

        }

        private async void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag.ToString();
            var cat = Singleton.getInstance().getCategorie(tag);
            DialogEditCategorie  dialogEditCategorie = new DialogEditCategorie(cat.Id, cat.Nom, cat.ImageLink);
            dialogEditCategorie.XamlRoot = this.XamlRoot;
            await dialogEditCategorie.ShowAsync();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag.ToString();
            Singleton.getInstance().deleteCategorie(tag);

        }

        private async void app_bar_icon_add_Click(object sender, RoutedEventArgs e)
        {
            DialogueCategorie categorie = new DialogueCategorie();
            categorie.XamlRoot = this.XamlRoot;
            await categorie.ShowAsync();
        }
    }
}
