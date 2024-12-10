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

    public sealed partial class PageGestionActivite : Page
    {
        ObservableCollection<Activites> activites;
        public PageGestionActivite()
        {
            this.InitializeComponent();
            Singleton.getInstance().getActivites();
            activites = Singleton.getInstance().activiteListe;
            gridActivites.ItemsSource = activites;
        }
        private async void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var numero = int.Parse(btn.Tag.ToString());
            DialogueEditActivite dialogEditActivite = new DialogueEditActivite(numero);
            dialogEditActivite.XamlRoot = this.XamlRoot;
            await dialogEditActivite.ShowAsync();
        }
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var id = btn.Tag.ToString();
            Singleton.getInstance().deleteActivite(int.Parse(id));

        }

        private async void app_bar_icon_add_Click(object sender, RoutedEventArgs e)
        {
            DialogAjoutActivite dialogAjouteActivite = new DialogAjoutActivite();
            dialogAjouteActivite.XamlRoot = this.XamlRoot;
            await dialogAjouteActivite.ShowAsync();
        }

        public Visibility CanDisplay2 { get { return Singleton.getInstance().Admin == null ? Visibility.Collapsed : Visibility.Visible; } }


        private async void Inscription_activite_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int activiteId = int.Parse(button.Tag.ToString());

            DialogueInscriptionActivite dialogueInscriptionActivite = new DialogueInscriptionActivite(activiteId)
            {
                XamlRoot = this.XamlRoot
            };
            await dialogueInscriptionActivite.ShowAsync();
        }
    }
}
