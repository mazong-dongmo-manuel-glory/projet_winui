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

    public sealed partial class PageAdherent : Page
    {
        public PageAdherent()
        {
            this.InitializeComponent();
        }


        private void btn_seances_Click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionSeance));
        }

        private void btn_activites_Click(object sender, RoutedEventArgs e)
        {
            Singleton.mainFrame.Navigate(typeof(PageGestionActivite));
        }

    }
}
