using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace proje_final
{
    internal class Singleton
    {
        
        public static  Singleton instance;
        public Adherent adherent;
        public Administration administration;
        public ObservableCollection<Adherent> adherentListe = new ObservableCollection<Adherent>();
        public ObservableCollection<Categorie> categorieListe = new ObservableCollection<Categorie>();
        public ObservableCollection<Activites> activiteListe = new ObservableCollection<Activites>();
        public ObservableCollection<Seances> seancesListe = new ObservableCollection<Seances>();
        public ObservableCollection<Participation> participationsListe = new ObservableCollection<Participation>();

        public static MainWindow mainWindow;
        public static Frame mainFrame;
        MySqlConnection con;

        public Singleton()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq14;Uid=2304249;Pwd=2304249;");
        }
    

        public Administration Admin { get { return administration; } }

        public static Singleton  getInstance()
        {
            if(instance == null)
            { 
                instance = new Singleton();

            }
            return instance;
        }
        public void openCon()
        {
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
        }
        /* Recuperation des tables */




        public void getParticipations()
        {
            participationsListe.Clear(); // Vider la liste pour éviter les doublons

            openCon();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM participations";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var participation = new Participation
                    {
                        IdParticipation = reader.GetInt32("idParticipation"),
                        IdSeance = reader.GetInt32("idSeance"),
                        NumeroAdherent = reader.GetString("numeroAdherent"),
                        Note = reader.GetInt32("note")
                    };

                    Debug.WriteLine($"Participation : idParticipation={participation.IdParticipation}, idSeance={participation.IdSeance}, note={participation.Note}");

                    participationsListe.Add(participation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la récupération des participations : {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }


        public bool AjouterParticipation(Participation participation)
        {
            openCon();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO participations (idSeance, numeroAdherent, note) VALUES (@idSeance, @numeroAdherent, @note)";
                cmd.Parameters.AddWithValue("@idSeance", participation.IdSeance);
                cmd.Parameters.AddWithValue("@numeroAdherent", participation.NumeroAdherent);
                cmd.Parameters.AddWithValue("@note", participation.Note);
                cmd.ExecuteNonQuery();
                con.Close();

                participationsListe.Add(participation);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de l'ajout de la participation : {ex.Message}");
                return false;
            }
        }


        public bool ModifierNoteParticipation(int idParticipation, int nouvelleNote)
        {
            openCon();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE participations SET note = @note WHERE idParticipation = @idParticipation";
                cmd.Parameters.AddWithValue("@note", nouvelleNote);
                cmd.Parameters.AddWithValue("@idParticipation", idParticipation);
                cmd.ExecuteNonQuery();
                con.Close();

                var participation = participationsListe.FirstOrDefault(p => p.IdParticipation == idParticipation);
                if (participation != null)
                {
                    participation.Note = nouvelleNote;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la mise à jour de la note : {ex.Message}");
                return false;
            }
        }



        public void getSeances()
        {
            seancesListe.Clear(); // Vider la liste pour éviter les doublons

            openCon();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT seances.*, activites.Nom as nomActivite, categories.imageLink as imageLink FROM seances INNER JOIN activites ON activites.id = seances.idActivite INNER JOIN categories ON categories.id = activites.categorie_id";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var seance = new Seances();
                    seance.Id = reader.GetInt32("id");
                    seance.NomActivite = reader.GetString("nomActivite");
                    seance.NombrePlace = reader.GetInt32("nombrePlace");
                    seance.NombrePlaceRestante = reader.GetInt32("nombrePlaceRestante");
                    seance.IdActivite = reader.GetInt32("idActivite");
                    seance.DateOrganisation = reader.GetDateTime("dateOrganisation").ToString();
                    seance.HeureOrganisation = reader.GetTimeSpan("heureOrganisation").ToString();
                    seance.ImageLink = reader.GetString("imageLink");

                    seancesListe.Add(seance);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Erreur lors de la récupération des séances : {e.Message}");
            }
            finally
            {
                con.Close();
            }
        }
        public bool deteleSeance(int id)
        {
            foreach(var seance in seancesListe)
            {
                if(seance.Id == id)
                {
                    seancesListe.Remove(seance);
                    return true;
                }
            }
            return false;
        }

        public void UpdateSeance(Seances seance)
        {
            openCon();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE seances SET nombrePlaceRestante = @nombrePlaceRestante WHERE id = @id";
                cmd.Parameters.AddWithValue("@nombrePlaceRestante", seance.NombrePlaceRestante);
                cmd.Parameters.AddWithValue("@id", seance.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la mise à jour de la séance : {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }



        public bool AjouterSeance(Seances seance)
        {
            try
            {
                openCon();
                var cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO seances (idActivite, dateOrganisation, heureOrganisation, nombrePlace, nombrePlaceRestante) VALUES (@idActivite, @dateOrganisation, @heureOrganisation, @nombrePlace, @nombrePlaceRestante)";
                cmd.Parameters.AddWithValue("@idActivite", seance.IdActivite);
                cmd.Parameters.AddWithValue("@dateOrganisation", DateTime.Parse(seance.DateOrganisation).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@heureOrganisation", seance.HeureOrganisation);
                cmd.Parameters.AddWithValue("@nombrePlace", seance.NombrePlace);
                cmd.Parameters.AddWithValue("@nombrePlaceRestante", seance.NombrePlaceRestante);
                cmd.ExecuteNonQuery();
                con.Close();

                seancesListe.Add(seance);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erreur lors de l'ajout de la séance : " + e.Message);
                return false;
            }
        }






        public Activites getActivite(int id)
        {
            Activites act = new Activites(0,"",0,0,0,"","");
            foreach(var activite in activiteListe)
            {
                if(activite.id == id)
                {
                    act = activite;
                }
            }
            return act;
        }
        public void getActivites()
        {
            activiteListe.Clear();
            openCon();
            try
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM ActivitesCategories";
                var reader = cmd.ExecuteReader();
                activiteListe = new ObservableCollection<Activites>();
                while (reader.Read())
                {

                    var newActivite = new Activites(
                        reader.GetInt32("id"),
                        reader.GetString("nom"),
                        reader.GetInt32("categorie_id"),
                        reader.GetInt32("prixCout"),
                        reader.GetInt32("prixVente"),
                        reader.GetString("imageLink"),
                        reader.GetString("categorieNom")
                        );
                    activiteListe.Add(newActivite);
                }
                reader.Close();

               
            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            con.Close();
        }

        public bool updateActivite(Activites activite)
        {
            openCon();
            try
            {
                Debug.WriteLine(activite.Nom);
                var cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE activites SET nom=@nom, categorie_id=@categorie_id, prixCout=@prixCout, prixVente=@prixVente  WHERE id=@id";
                cmd.Parameters.AddWithValue("nom", activite.Nom);
                cmd.Parameters.AddWithValue("categorie_id",activite.CategorieId);
                cmd.Parameters.AddWithValue("prixCout",activite.PrixCout);
                cmd.Parameters.AddWithValue("prixVente",activite.PrixVente);
                cmd.Parameters.AddWithValue("id", activite.Id);
                
                activiteListe.Add(activite);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        public bool ajoutActivite(Activites activite)
        {
            openCon();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO activites(nom,prixCout,prixVente,categorie_id) VALUES(@nom, @prixCout, @prixVente, @categorieId)";
                cmd.Parameters.AddWithValue("nom", activite.Nom);
                cmd.Parameters.AddWithValue("prixCout", activite.PrixCout);
                cmd.Parameters.AddWithValue("prixVente", activite.PrixVente);
                cmd.Parameters.AddWithValue("categorieId", activite.CategorieId);
                cmd.ExecuteNonQuery();
                activiteListe.Add(activite);
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteActivite(int id)
        {
            foreach(var activite in activiteListe)
            {
                if(activite.Id == id)
                {
                    activiteListe.Remove(activite);
                    return true;
                }
            }
            return false;
        }
        public void getAdherents()
        {
            adherentListe.Clear();
            try
            {
                openCon();
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Adherents";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var newAdhrent = new Adherent(
                                reader.GetString("numero"),
                                reader.GetString("nom"),
                                reader.GetString("prenom"),
                                reader.GetDateTime("dateNaissance").ToString("yyyy-MM-dd"),
                                reader.GetString("adresse")
                            );
                            adherentListe.Add(newAdhrent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erreur lors de la récupération des adhérents : " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public Adherent getAdherent(string numero)
        {
            foreach(var adh in adherentListe)
            {
                if(adh.Numero == numero)
                {
                    return adh;
                }
            }
            return null;
        }
        /* mise a jours des table */
        public bool updateAdherents(Adherent adh)
        {

            try
            {
                openCon();
           
                    var cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE adherents SET nom=@nom, prenom=@prenom, dateNaissance=@dateNaissance,adresse=@adresse WHERE numero=@numero";
                    cmd.Parameters.AddWithValue("nom", adh.Nom);
                    cmd.Parameters.AddWithValue("prenom", adh.Prenom);
                    cmd.Parameters.AddWithValue("dateNaissance", DateTime.Parse(adh.DateNaissance).ToString("yyyyy-mm-dd"));
                    cmd.Parameters.AddWithValue("adresse", adh.Adresse);
                    cmd.Parameters.AddWithValue("numero", adh.Numero);
                    cmd.ExecuteNonQuery();
                con.Close ();
                return true;
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public bool deleteAdherent(string numero)
        {
            try{
                openCon();

                var cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM adherents WHERE numero=@numero";
              
                cmd.Parameters.AddWithValue("numero", numero);
                cmd.ExecuteNonQuery();
                foreach (var adh in adherentListe)
                {
                    if (adh.Numero == numero)
                    {
                       adherentListe.Remove(adh);
                    }
                }
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public bool ajouterAdherent(Adherent adh)
        {
            try
            {
                openCon();
                MySqlCommand cmd = new MySqlCommand("ajouterAdherent");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nomAdherent",adh.Nom);
                cmd.Parameters.AddWithValue("prenomAdherent",adh.Prenom);
                cmd.Parameters.AddWithValue("dateNaissanceAdherent", adh.DateNaissance);
                cmd.Parameters.AddWithValue("adresseAdherent",adh.Adresse);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /* Opertation sur la partie categorie */

        public void getAllCategories()
        {
            openCon();
            categorieListe.Clear();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM categories";
                var reader = cmd.ExecuteReader();
                categorieListe = new ObservableCollection<Categorie>();
                while (reader.Read())
                {
                    categorieListe.Add(
                        new Categorie(reader.GetString("nom"), reader.GetString("imageLink"),
                        reader.GetInt32("id"))
                        );
                }
                reader.Close();

            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public bool ajouterCategorie(Categorie categorie )
        {
            try
            {
                openCon();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO categories(nom,imageLink) VALUES(@nom,@imageLink)";
                cmd.Parameters.AddWithValue("nom", categorie.Nom);
                cmd.Parameters.AddWithValue("imageLink", categorie.ImageLink);
                cmd.ExecuteNonQuery();
                categorieListe.Add(categorie);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public Categorie getCategorie(string id) {
            foreach(var cat in categorieListe)
            {
                if(cat.Id == int.Parse(id))
                {
                    return cat;
                }
            }
            return null;
        }

        public bool deleteCategorie(string id)
        {
            openCon();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM categories WHERE id=@id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                categorieListe.Remove(getCategorie(id));
                activiteListe.Clear();
                getActivites();
                con.Close();

                return true;

            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        public bool updateCategorie(Categorie cat)
        {
            openCon();
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE categories SET nom=@nom, imageLink=@imageLink WHERE id=@id";
                cmd.Parameters.AddWithValue("nom", cat.Nom);
                cmd.Parameters.AddWithValue("imageLink", cat.ImageLink);
                cmd.Parameters.AddWithValue("id", cat.Id.ToString());
                cmd.ExecuteNonQuery();
                activiteListe.Clear();
                getActivites();
                con.Close();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        /* Authentification */
        public bool connexionAdherent(string numero)
        {
            bool etat = false;
            openCon();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Adherents WHERE numero=@numero";
            cmd.Parameters.AddWithValue("numero", numero);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {
                adherent = new Adherent
                (
                    reader.GetString("nom"),
                    reader.GetString("prenom"),
                    reader.GetString("numero"),
                    reader["dateNaissance"].ToString(),
                    reader.GetString("adresse")
                );
                etat = true;
               
            }
            reader.Close();
            con.Close();
            return etat;
        }
        public bool connexionAdministrateur(string nom, string mdp)
        {
            bool etat = false;
            openCon();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Administrateurs WHERE nom=@nom AND mdp=@mdp";
            cmd.Parameters.AddWithValue("nom", nom);
            cmd.Parameters.AddWithValue("mdp", mdp);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                administration = new Administration
                (
                    reader.GetInt32("id"),
                    reader.GetString("nom"),
                    reader.GetString("mdp")
                 
                );
                etat = true;

            }
            reader.Close();
            con.Close();
            return etat;
        }
        public void deconnexion()
        {
            adherent = null;
            administration = null;
        }


        // SECTION STATISTIQUE

        public int GetTotalAdherent()
        {
            return adherentListe.Count;
        }

        public int GetTotalActivite()
        {
            Singleton.getInstance().getActivites();
            return activiteListe.Count;
        }

        public int GetTotalcategorie()
        {
            return categorieListe.Count;
        }

        public int GetTotalSeance()
        {
            Singleton.getInstance().getSeances();
            return seancesListe.Count;
        }

        public void rechercherAdherentNom(string v)
        {
         
        }



       
        /// /////////////////////////////////////////////////// ///






        public ObservableCollection<Seances> GetSeancesParAdherent(string numeroAdherent)
        {
            var seancesFiltrees = new ObservableCollection<Seances>();

            foreach (var seance in seancesListe)
            {
               
                if (seance.IdAdherent == numeroAdherent)
                {
                    seancesFiltrees.Add(seance);
                }
            }

            return seancesFiltrees;
        }


        public ObservableCollection<Seances> GetSeancesDisponiblesPourActivite(int idActivite)
        {
            var seancesDisponibles = new ObservableCollection<Seances>();

            foreach (var seance in seancesListe)
            {
                if (seance.IdActivite == idActivite && seance.NombrePlaceRestante > 0)
                {
                    seancesDisponibles.Add(seance);
                }
            }

            return seancesDisponibles;
        }

        public Dictionary<string, int> GetNombreAdherentsParActivite()
        {
            var adherentsParActivite = new Dictionary<string, int>();

            foreach (var activite in activiteListe)
            {
                int count = seancesListe
                    .Where(s => s.IdActivite == activite.Id)
                    .Select(s => s.IdAdherent)
                    .Distinct()
                    .Count();

                adherentsParActivite[activite.Nom] = count;
            }

            return adherentsParActivite;
        }


        public void AjouterOuModifierNote(int idParticipation, int note)
{
    openCon();
    try
    {
        var cmd = con.CreateCommand();
        cmd.CommandText = "UPDATE participations SET note = @note WHERE idParticipation = @idParticipation";
        cmd.Parameters.AddWithValue("@note", note);
        cmd.Parameters.AddWithValue("@idParticipation", idParticipation);
        cmd.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Erreur lors de l'ajout ou de la modification de la note : {ex.Message}");
    }
    finally
    {
        con.Close();
    }
}


        public Dictionary<int, double> GetMoyenneNotesParActivite()
        {
            var moyenneNotes = new Dictionary<int, double>();

            foreach (var activite in activiteListe)
            {
                Debug.WriteLine($"Traitement de l'activité : {activite.Nom} (ID : {activite.Id})");

                foreach (var participation in participationsListe)
                {
                    var seanceAssociee = seancesListe.FirstOrDefault(s => s.Id == participation.IdSeance);

                    if (seanceAssociee != null && seanceAssociee.IdActivite == activite.Id)
                    {
                        Debug.WriteLine($"Participation liée : idParticipation={participation.IdParticipation}, idSeance={participation.IdSeance}, note={participation.Note}, Activité={activite.Nom}");
                    }
                }

                var notes = participationsListe
                    .Where(p => seancesListe.Any(s =>
                        s.Id == p.IdSeance &&
                        s.IdActivite == activite.Id))
                    .Select(p => p.Note);

                Debug.WriteLine($"Notes trouvées pour {activite.Nom} : {string.Join(", ", notes)}");

                moyenneNotes[activite.Id] = notes.Any() ? notes.Average() : 0;

                Debug.WriteLine($"Moyenne calculée pour {activite.Nom} : {moyenneNotes[activite.Id]}");
            }

            return moyenneNotes;
        }

        public bool EstDejaInscrit(int idSeance, string numeroAdherent)
        {
            return participationsListe.Any(p => p.IdSeance == idSeance && p.NumeroAdherent == numeroAdherent);
        }



    }
}
