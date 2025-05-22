using System;
using System.Collections.Generic;
using System.Linq;
using projet_ensemenc_RechMathieu_UngMaxime;

namespace SimulateurPotager
{
    public class Game
    {
        private List<Plante> plantes; // Les plantes en jeu
        private int semaine;
        private Random rng;

        public Game()
        {
            plantes = new List<Plante>();
            semaine = 1;
            rng = new Random();
        }

        public void Start()
        {
            Console.WriteLine("Bienvenue dans le simulateur de potager ENSemenC !");
            Console.WriteLine("Simulation de votre potager en cours...\n");

            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine($"\n--- Semaine {semaine} ---");

                // Afficher l’état actuel du jardin
                AfficherEtatPlantes();

                // Actions disponibles
                Console.WriteLine("Actions disponibles :");
                Console.WriteLine("1. Semer une graine");
                Console.WriteLine("2. Arroser");
                Console.WriteLine("3. Récolter");
                Console.WriteLine("4. Passer à la semaine suivante");
                Console.WriteLine("5. Quitter le jeu");

                choixAction:
                Console.Write("Choisissez une action : ");
                ConsoleKeyInfo entree = Console.ReadKey()!;
                int choix;
                if (char.IsDigit(entree.KeyChar))
                {
                    choix = int.Parse(entree.KeyChar.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entrée invalide.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto choixAction;
                }

                switch (choix)
                {
                    case 1:
                        SemerPlante();
                        break;
                    case 2:
                        ArroserPlantes();
                        break;
                    case 3:
                        RecolterPlantes();
                        break;
                    case 4:
                        SimulerSemaine();
                        semaine++;
                        break;
                    case 5:
                        continuer = false;
                        Console.WriteLine("Merci d’avoir joué !");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Choix invalide.");
                        Console.ForegroundColor = ConsoleColor.White;
                        goto choixAction;
                }
            }
        }

        private void AfficherEtatPlantes()
        {
            if (plantes.Count == 0)
            {
                Console.WriteLine("Aucune plante dans le potager.");
                return;
            }

            foreach (var plante in plantes)
            {
                Console.WriteLine(plante.AfficherResume());
            }
        }

        private void SemerPlante()
        {
            Console.WriteLine("Quelle plante voulez-vous semer ? :\n1) : Tulipe\n2) : Tomate");
            ConsoleKeyInfo entree = Console.ReadKey()!;
            int numeroPlante;
            if (char.IsDigit(entree.KeyChar))
            {
                numeroPlante = int.Parse(entree.KeyChar.ToString());
            }
            else numeroPlante = 0;

            choixPlante:
            Plante nouvellePlante = null;
            switch (numeroPlante)
            {
                case 1:
                    nouvellePlante = new Tulipe();
                    Console.WriteLine("🌷 Tulipe choisie.");
                    break;
                case 2:
                    nouvellePlante = new Tomate();
                    Console.WriteLine("🍅 Tomate chosie.");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCette plante n'existe pas !\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto choixPlante;
            }
            Console.WriteLine(nouvellePlante.AfficherProprietes());

            // Choix terrain
            choixTerrain:
            Console.WriteLine("\nChoisissez le type de terrain où planter :\n1) Terre\n2) Sable\n3) Argile\n> ");
            ConsoleKeyInfo terrainEntree = Console.ReadKey()!;
            int numeroTerrain;
            if (char.IsDigit(terrainEntree.KeyChar))
            {
                numeroTerrain = int.Parse(terrainEntree.KeyChar.ToString());
            }
            else numeroTerrain = 0;

            Terrain terrain = null;
            switch (numeroTerrain)
            {
                case 1:
                    terrain = new Terre();
                    Console.WriteLine("🌱 Terrain Terre choisi.");
                    break;
                case 2:
                    terrain = new Sable();
                    Console.WriteLine("🏖️ Terrain Sable choisi.");
                    break;
                case 3:
                    terrain = new Argile();
                    Console.WriteLine("🧱 Terrain Argile choisi.");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCe terrain n'existe pas !");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto choixTerrain;
            }
            Console.WriteLine(terrain.AfficherProprietes());

            // Confirmation de la plantation
            Console.WriteLine("\n✅ Confirmer la plantation ? (o pour oui, autre touche pour annuler)");
            ConsoleKeyInfo confirmation = Console.ReadKey();
            if (confirmation.KeyChar != 'o' && confirmation.KeyChar != 'O')
            {
                Console.WriteLine("\n🌱 Plantation annulée.");
                return;
            }

            nouvellePlante.AssocierTerrain(terrain);
            plantes.Add(nouvellePlante);
            Console.WriteLine($"\n{nouvellePlante.Nom} plantée sur terrain : {terrain.Nom}.");
        }

        private void ArroserPlantes()
        {
            foreach (var plante in plantes)
            {
                if (plante.TerrainAssocie != null)
                {
                    plante.TerrainAssocie.AjouterEau(1.0); // Ajoute 1L d'eau/m²
                }
            }
            Console.WriteLine("Vous avez arrosé toutes les plantes (+1L/m² chacune).");
        }


        private void RecolterPlantes()
        {
            // Toute plante ayant dépassé la moitié de son espérance de vie peut être récoltée
            int recoltees = plantes.RemoveAll(p => semaine >= p.EsperanceDeVie / 2);
            Console.WriteLine($"{recoltees} plante(s) récoltée(s) !");
        }

        private void SimulerSemaine()
        {
            // Determination saison et Météo
            Saison saison = new Saison(semaine, rng);
            double temperature = saison.GenererTemperature();
            double precipitation = saison.GenererPrecipitations();
            Console.WriteLine($"\n📅 {saison} | 🌡️ {temperature}°C | 🌧️ {precipitation:F1} L/m²\n");

            // Absorption pluie par Terrain
            foreach (var plante in plantes)
            {
                plante.TerrainAssocie?.AjouterEau(precipitation);
            }

            // Determination nouvelle état de la plante
            Console.WriteLine("📈 Évolution hebdomadaire des plantes :");
            foreach (var plante in plantes.ToList())
            {
                double tauxConditions = plante.CalculerTauxConditions(temperature);
                double tailleAvant = plante.Taille;
                plante.Pousser(tauxConditions);
                plante.AttraperMaladie(rng);

                if (plante.Taille > tailleAvant)
                    Console.WriteLine($"→ {plante.Nom} a grandi de {plante.Taille - tailleAvant:F1} cm.");

                if (!plante.VerifierSurvie(tauxConditions))
                {
                    Console.WriteLine($"💀 La plante {plante.Nom} est morte cette semaine.");
                    plantes.Remove(plante);
                }
            }
        }
    }
}
