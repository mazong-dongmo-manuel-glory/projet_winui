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

    public sealed partial class PageStatistiques : Page
    {
        public PageStatistiques()
        {
            this.InitializeComponent();
            Afficherstatistiques();
        }

        private void Afficherstatistiques()
        {
            var bd = Singleton.getInstance();


            int totalAdherents = bd.GetTotalAdherent();
            nb_total_adherent.Text =  $"Total : {totalAdherents}";

            int totalActivite = bd.GetTotalActivite();
            nbActivite.Text = $"Total : {totalActivite}";

            int totalCategories = bd.GetTotalcategorie();
            nbCategorie.Text = $"Total : {totalCategories}";

            int totalSeances = bd.GetTotalSeance();
            nb_total_seances.Text = $"Total : {totalSeances}";


            var adherentsParActivite = bd.GetNombreAdherentsParActivite();
            string adherentsDetails = string.Join("\n", adherentsParActivite.Select(kvp => $"{kvp.Key} : {kvp.Value} adhérents"));
           // adherents_par_activite.Text = adherentsDetails;


        }
    }
}
