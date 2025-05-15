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

                Console.Write("Choisissez une action : ");
                string choix = Console.ReadLine()!;

                choixAction:
                switch (choix)
                {
                    case "1":
                        SemerPlante();
                        break;
                    case "2":
                        ArroserPlantes();
                        break;
                    case "3":
                        RecolterPlantes();
                        break;
                    case "4":
                        SimulerSemaine();
                        semaine++;
                        break;
                    case "5":
                        continuer = false;
                        Console.WriteLine("Merci d’avoir joué !");
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
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
                Console.WriteLine(plante.ToString());
            }
        }

        private void SemerPlante()
        {
            Console.Write("Quelle plante voulez-vous semer ? :\n1) : Tulipe\n2) : A venir...");
            ConsoleKeyInfo entree = Console.ReadKey()!;
            int numeroPlante;
            if (char.IsDigit(entree.KeyChar))
            {
                numeroPlante = int.Parse(entree.KeyChar.ToString());
            }
            else numeroPlante = 1;

            choixPlante:
            switch (numeroPlante)
            {
                case 1:
                    Tulipe nouvellePlante = new Tulipe();
                    plantes.Add(nouvellePlante);
                    Console.WriteLine("🌷 Tulipe plantée.");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCette plante n'existe pas !\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto choixPlante;
            }

        }

        private void ArroserPlantes()
        {
            Console.WriteLine("Vous avez arrosé toutes les plantes.");
        }

        private void RecolterPlantes()
        {
            // Toute plante ayant dépassé la moitié de son espérance de vie peut être récoltée
            int recoltees = plantes.RemoveAll(p => semaine >= p.EsperanceDeVie / 2);
            Console.WriteLine($"{recoltees} plante(s) récoltée(s) !");
        }

        private void SimulerSemaine()
        {
            foreach (var plante in plantes.ToList())
            {
                double tauxConditions = rng.NextDouble(); // Simule la météo/conditions
                plante.Pousser(tauxConditions);
                plante.AttraperMaladie(rng);

                if (!plante.VerifierSurvie(tauxConditions))
                {
                    Console.WriteLine($"La plante {plante.Nom} est morte cette semaine.");
                    plantes.Remove(plante);
                }
            }
        }
    }
}
