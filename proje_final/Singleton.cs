﻿using Microsoft.UI.Xaml.Controls;
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
        public static MainWindow mainWindow;
        public static Frame mainFrame;
        MySqlConnection con;

        public Singleton()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq14;Uid=2304249;Pwd=2304249;");
        }

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
        public void getAdherents()
        {
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
                            Debug.WriteLine(newAdhrent.Numero);
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
            try
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM categories";
                var reader = cmd.ExecuteReader();
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
         
       
    }
}