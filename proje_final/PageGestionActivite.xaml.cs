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
            var numero = btn.Tag.ToString();
            DialogueEditAdherent dialogueEditAdherent = new DialogueEditAdherent(numero);
            dialogueEditAdherent.XamlRoot = this.XamlRoot;
            await dialogueEditAdherent.ShowAsync();
        }
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            Singleton.getInstance().deleteAdherent(btn.Tag.ToString());

        }

        private async void app_bar_icon_add_Click(object sender, RoutedEventArgs e)
        {
            DialogueAjouteAdherent ajouteAdherent = new DialogueAjouteAdherent();
            ajouteAdherent.XamlRoot = this.XamlRoot;
            await ajouteAdherent.ShowAsync();
        }
    }
}
