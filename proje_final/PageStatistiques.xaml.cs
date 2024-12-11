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
            Singleton.getInstance().getParticipations();
            Singleton.getInstance().getSeances();
            Singleton.getInstance().getActivites();
            comboBoxActivites.ItemsSource = Singleton.getInstance().activiteListe;
            comboBoxActivites.DisplayMemberPath = "Nom";
            comboBoxActivites.SelectedValuePath = "Id";
            comboBoxAdherentsParActivite.ItemsSource = Singleton.getInstance().activiteListe;
            comboBoxAdherentsParActivite.DisplayMemberPath = "Nom";
            comboBoxAdherentsParActivite.SelectedValuePath = "Id";
            AfficherActiviteMieuxNotee();
            AfficherJourAvecPlusDeSeances();
            AfficherAdherentPlusAge();
            AfficherAdherentPlusJeune();

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

        private void comboBoxActivites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

         
            var bd = Singleton.getInstance();

        
            if (comboBoxActivites.SelectedItem is Activites activiteSelectionnee)
            {
             
                var seancesDeActivite = bd.seancesListe.Where(s => s.IdActivite == activiteSelectionnee.Id);

       
                var participations = bd.participationsListe.Where(p => seancesDeActivite.Any(s => s.Id == p.IdSeance));

             
                if (participations.Any())
                {
                    double moyenne = participations.Average(p => p.Note);
                    nbMoyenneNoteChaqueActivites.Text = $"Moyenne : {moyenne:F2}/20";
                }
                else
                {
                    nbMoyenneNoteChaqueActivites.Text = "Aucune note disponible pour cette activité.";
                }
            }

        }


        private void AfficherActiviteMieuxNotee()
        {
           
            var bd = Singleton.getInstance();

           
            var activitesAvecMoyennes = bd.activiteListe
                .Select(activite =>
                {
                  
                    var seancesDeActivite = bd.seancesListe.Where(s => s.IdActivite == activite.Id);

                   
                    var participations = bd.participationsListe.Where(p => seancesDeActivite.Any(s => s.Id == p.IdSeance));

                 
                    double moyenne = participations.Any() ? participations.Average(p => p.Note) : 0;

                    return new { Activite = activite, Moyenne = moyenne };
                });

           
            var activiteMieuxNotee = activitesAvecMoyennes
                .OrderByDescending(a => a.Moyenne)
                .FirstOrDefault();

        
            if (activiteMieuxNotee != null && activiteMieuxNotee.Moyenne > 0)
            {
                txtActiviteMieuxNotee.Text = $"{activiteMieuxNotee.Activite.Nom} ({activiteMieuxNotee.Moyenne:F2}/20)";
            }
            else
            {
                txtActiviteMieuxNotee.Text = "Aucune activité notée.";
            }
        }

        private void AfficherJourAvecPlusDeSeances()
        {
        
            var bd = Singleton.getInstance();

        
            var seancesParJour = bd.seancesListe
                .GroupBy(s => DateTime.Parse(s.DateOrganisation).DayOfWeek)
                .Select(g => new { Jour = g.Key, NombreSeances = g.Count() })
                .OrderByDescending(j => j.NombreSeances)
                .FirstOrDefault();

         
            if (seancesParJour != null)
            {
                txtJourAvecPlusSeances.Text = $"{seancesParJour.Jour} ({seancesParJour.NombreSeances} séances)";
            }
            else
            {
                txtJourAvecPlusSeances.Text = "Aucune séance organisée.";
            }
        }

        private void comboBoxAdherentsParActivite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var bd = Singleton.getInstance();

        
            if (comboBoxAdherentsParActivite.SelectedItem is Activites activiteSelectionnee)
            {
             
                var seancesDeActivite = bd.seancesListe.Where(s => s.IdActivite == activiteSelectionnee.Id);

               
                var adherentsInscrits = bd.participationsListe
                    .Where(p => seancesDeActivite.Any(s => s.Id == p.IdSeance))
                    .Select(p => p.NumeroAdherent)
                    .Distinct();

                int nombreAdherents = adherentsInscrits.Count();

         
                txtNbAdherentsParActivite.Text = $"Nombre d'adhérents : {nombreAdherents}";
            }
            else
            {
                txtNbAdherentsParActivite.Text = "Veuillez sélectionner une activité.";
            }

        }

        private void AfficherAdherentPlusAge()
        {
            var bd = Singleton.getInstance();

           
            var adherentPlusAge = bd.adherentListe
                .OrderBy(a => DateTime.Parse(a.DateNaissance))
                .FirstOrDefault();

           
            if (adherentPlusAge != null)
            {
                var dateNaissance = DateTime.Parse(adherentPlusAge.DateNaissance);
                int age = DateTime.Now.Year - dateNaissance.Year;
                if (DateTime.Now < dateNaissance.AddYears(age)) age--; 

                txtAdherentPlusAge.Text = $"{adherentPlusAge.Nom} {adherentPlusAge.Prenom} ({age} ans, né le {dateNaissance:yyyy-MM-dd})";
            }
            else
            {
                txtAdherentPlusAge.Text = "Aucun adhérent disponible.";
            }
        }

        private void AfficherAdherentPlusJeune()
        {
            var bd = Singleton.getInstance();

           
            var adherentPlusJeune = bd.adherentListe
                .OrderByDescending(a => DateTime.Parse(a.DateNaissance))
                .FirstOrDefault();

           
            if (adherentPlusJeune != null)
            {
                var dateNaissance = DateTime.Parse(adherentPlusJeune.DateNaissance);
                int age = DateTime.Now.Year - dateNaissance.Year;
                if (DateTime.Now < dateNaissance.AddYears(age)) age--; 

                txtAdherentPlusJeune.Text = $"{adherentPlusJeune.Nom} {adherentPlusJeune.Prenom} ({age} ans, né le {dateNaissance:yyyy-MM-dd})";
            }
            else
            {
                txtAdherentPlusJeune.Text = "Aucun adhérent disponible.";
            }
        }



    }
}
