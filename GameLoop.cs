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
        private List<int> semainesPlantation = new List<int>(); // Age des plantes semées
        private List<string> nomsPlantesRecoltees = new List<string>();
        private List<int> quantitesRecoltees = new List<int>();

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
                Console.WriteLine("2. Arroser une plante");
                Console.WriteLine("3. Récolter une plante");
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
                        semaine++;
                        SimulerSemaine();
                        break;
                    case 5:
                        continuer = false;
                        AfficherSynthese();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Choix invalide.");
                        Console.ForegroundColor = ConsoleColor.White;
                        goto choixAction;
                }
            }
        }

        private int AgePlante(int index)
        { return semaine - semainesPlantation[index]; }

        private void AfficherEtatPlantes()
        {
            if (plantes.Count == 0)
            {
                Console.WriteLine("Aucune plante dans le potager.");
                return;
            }

            for (int i = 0; i < plantes.Count; i++)
            {
                Console.WriteLine($"{plantes[i]} | Âge : {AgePlante(i)} semaines.");
            }
        }

        private Plante ChoisirPlante()
        {
            Console.WriteLine("Quelle plante voulez-vous semer ? :\n1) : Tulipe\n2) : Tomate");
            while (true)
            {
                ConsoleKeyInfo entree = Console.ReadKey();
                Console.WriteLine();
                switch (entree.KeyChar)
                {
                    case '1': Console.WriteLine("🌷 Tulipe choisie."); return new Tulipe();
                    case '2': Console.WriteLine("🍅 Tomate choisie."); return new Tomate();
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cette plante n'existe pas !");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }

        private Terrain ChoisirTerrain()
        {
            Console.WriteLine("Choisissez le type de terrain où planter :\n1) Terre\n2) Sable\n3) Argile");
            while (true)
            {
                ConsoleKeyInfo entree = Console.ReadKey();
                Console.WriteLine();
                switch (entree.KeyChar)
                {
                    case '1': Console.WriteLine("🌱 Terrain Terre choisi."); return new Terre();
                    case '2': Console.WriteLine("🏖️ Terrain Sable choisi."); return new Sable();
                    case '3': Console.WriteLine("🧱 Terrain Argile choisi."); return new Argile();
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ce terrain n'existe pas !");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }

        private void SemerPlante()
        {
            Plante nouvellePlante = ChoisirPlante();
            Console.WriteLine(nouvellePlante.AfficherProprietes());

            // Vérification de la saison de semis
            string saisonActuelle = new Saison(semaine, rng).Nom.ToString();
            if (!nouvellePlante.SaisonsSemis.Contains(saisonActuelle))
            {
                Console.WriteLine($"\n🚫 {nouvellePlante.Nom} ne peut être semée qu'en : {string.Join(", ", nouvellePlante.SaisonsSemis)}.");
                return;
            }

            Terrain terrain = ChoisirTerrain();
            Console.WriteLine(terrain.AfficherProprietes());

            // Confirmation
            Console.WriteLine("\n✅ Confirmer la plantation ? (o pour oui, autre touche pour annuler)");
            ConsoleKeyInfo confirmation = Console.ReadKey();
            if (confirmation.KeyChar != 'o' && confirmation.KeyChar != 'O')
            {
                Console.WriteLine("\n🌱 Plantation annulée.");
                return;
            }

            nouvellePlante.AssocierTerrain(terrain);
            plantes.Add(nouvellePlante);
            semainesPlantation.Add(semaine);
            Console.WriteLine($"\n{nouvellePlante.Nom} plantée sur terrain : {terrain.Nom}.");
        }

        private void ArroserPlantes()
        {
            if (plantes.Count == 0)
            {
                Console.WriteLine("Aucune plante à arroser.");
                return;
            }
            Console.WriteLine("\nChoisissez la plante à arroser :");
            for (int i = 0; i < plantes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plantes[i].Nom} ({plantes[i].Taille:F1} cm)");
            }
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= plantes.Count)
            {
                plantes[index - 1].TerrainAssocie?.AjouterEau(0.1);
                Console.WriteLine($"💧 {plantes[index - 1].Nom} a été arrosée (0.1L/m² ajouté).");
            }
            else
            {
                Console.WriteLine("❌ commande invalide.");
            }
        }

        private void RecolterPlantes()
        {
            if (plantes.Count == 0)
            {
                Console.WriteLine("Aucune plante à récolter.");
                return;
            }

            Console.WriteLine("\nChoisissez la plante à récolter :");
            for (int i = 0; i < plantes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plantes[i].Nom} - Taille: {plantes[i].Taille:F1} cm - Vie: {AgePlante(i)}/{plantes[i].EsperanceDeVie} sem."); // MODIFIÉ
            }
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= plantes.Count)
            {
                int i = index - 1;
                // Les plantes qui ont atteints plus de la moitié de leurs espérances de vie peuvent 
                if (plantes[i].PeutEtreRecoltee(AgePlante(i)))
                {
                    string nom = plantes[i].Nom;
                    if (!nomsPlantesRecoltees.Contains(nom))
                    {
                        nomsPlantesRecoltees.Add(nom);
                        quantitesRecoltees.Add(1);
                    }
                    else
                    {
                        int idx = nomsPlantesRecoltees.IndexOf(nom);
                        quantitesRecoltees[idx]++;
                    }
                    plantes.RemoveAt(i);
                    semainesPlantation.RemoveAt(i);
                    Console.WriteLine($"✅ {nom} récoltée avec succès.");
                }
                else
                {
                    Console.WriteLine("⚠️ Cette plante est trop jeune pour être récoltée.");
                }
            }
            else
            {
                Console.WriteLine("❌ Commande invalide.");
            }
        }

        private void SimulerSemaine()
        {
            // Determination saison et Météo
            Saison saison = new Saison(semaine, rng);
            double temperature = saison.GenererTemperature();
            double precipitation = saison.GenererPrecipitations();
            double luminosite = saison.GenererLuminosite();
            Console.WriteLine($"\n📅 {saison} | 🌡️ {temperature}°C | 🌧️ {precipitation:F1} L/m² | ☀️ {luminosite:F0}% \n");

            // Absorption pluie par Terrain
            foreach (var plante in plantes)
            { plante.TerrainAssocie?.AjouterEau(precipitation); }

            // Determination nouvelle état de la plante
            Console.WriteLine("📈 Évolution hebdomadaire des plantes :");
            foreach (var plante in plantes.ToList())
            {
                double tauxConditions = plante.CalculerTauxConditions(temperature, luminosite);
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

        // Synthèse de fin de partie
        private void AfficherSynthese()
        {
            Console.WriteLine("\n🧾 Synthèse de votre potager :");
            if (nomsPlantesRecoltees.Count == 0)
            {
                Console.WriteLine("Aucune plante récoltée.");
            }
            else
            {
                for (int i = 0; i < nomsPlantesRecoltees.Count; i++)
                    Console.WriteLine($"- {nomsPlantesRecoltees[i]} : {quantitesRecoltees[i]} récolte(s)");
            }
            Console.WriteLine("Merci d'avoir joué !");
        }
    }
}
