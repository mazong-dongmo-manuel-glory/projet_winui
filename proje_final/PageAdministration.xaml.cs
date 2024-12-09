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


namespace proje_final
{

    public sealed partial class PageAdministration : Page
    {
        public PageAdministration()
        {
            this.InitializeComponent();
        }
        private void VoirAdherents_Click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionAdherent));
        }
        private void VoirCategories_Click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionCategorie));
        } 
        private void VoirActivites_click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionActivite));
        }
        private void VoirSeances_click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionSeance));
        }
        private void btn_statistique_Click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageStatistiques));
        }
        
    }
}
