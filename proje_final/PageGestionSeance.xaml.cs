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



namespace proje_final
{
  
    public sealed partial class PageGestionSeance : Page
    {
        ObservableCollection<Seances> seances;
        public PageGestionSeance()
        {
            this.InitializeComponent();
           Singleton.getInstance().getSeances();
           seances = Singleton.getInstance().seancesListe;
           gridSeances.ItemsSource = seances;
        }
        private async void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag.ToString();
            var cat = Singleton.getInstance().getCategorie(tag);
            DialogEditCategorie dialogEditCategorie = new DialogEditCategorie(cat.Id, cat.Nom, cat.ImageLink);
            dialogEditCategorie.XamlRoot = this.XamlRoot;
            await dialogEditCategorie.ShowAsync();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tag = btn.Tag.ToString();
            Singleton.getInstance().deleteCategorie(tag);
            Singleton.getInstance().getAllCategories();
            Singleton.getInstance().getActivites();

        }

        private async void app_bar_icon_add_Click(object sender, RoutedEventArgs e)
        { 
            DialogueAjouteSeance dialogAjouteSeance = new DialogueAjouteSeance();

            dialogAjouteSeance.XamlRoot = this.XamlRoot;
            await dialogAjouteSeance.ShowAsync();
        }
        public Visibility CanDisplay2 { get { return Singleton.getInstance().Admin == null ? Visibility.Collapsed : Visibility.Visible; } }
    }
}
