using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using projet_ensemenc_RechMathieu_UngMaxime;

namespace SimulateurPotager
{
    public class Game
    {
        private List<Plante> plantes; // Les plantes en jeu
        private int semaine;
        private int nbActionsRestantes = 5;
        private double probaInvasionRat = 0.05; // 5% de chances au départ de se faire envahir par tour
        private Random rng;
        private List<int> semainesPlantation = new List<int>(); // Age des plantes semées
        private List<string> nomsPlantesRecoltees = new List<string>();
        private List<int> quantitesRecoltees = new List<int>(); 
        private List<int> plantesProtegeesDansSerre = new List<int>();

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

                double tirageRat = rng.NextDouble();
                if (tirageRat < probaInvasionRat) InvasionRat();

                if (nbActionsRestantes == 0)
                {
                    Console.WriteLine("Vous avez épuisé toutes vos actions pour la semaine !\nPassage à la semaine suivante...");
                    nbActionsRestantes = 5;
                    SimulerSemaine();
                }

                Console.WriteLine($"\n--- Semaine {semaine} ---");

                // Afficher l’état actuel du jardin
                AfficherEtatPlantes();

                // Actions disponibles
                Console.WriteLine($"Il vous reste {nbActionsRestantes} actions cette semaine.\nVoulez-vous :");
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
                        nbActionsRestantes = 5;
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
                nbActionsRestantes--;
            }
        }

        private void InvasionRat()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠⚠ ALERTE !! ⚠⚠\nUn RAT s'est infiltré dans votre potager ! Il commence à ronger vos plantes !");
            Console.ForegroundColor = ConsoleColor.White;

            bool ratPresent = true;
            int toursRat = 0;
            int bacheDeployee = 0;

            while (ratPresent && toursRat < 3)
            {
                Console.WriteLine($"\n🐀 Le rat rôde dans le jardin... (Tour {toursRat + 1}/3)");

                if (plantes.Count > 0)
                {
                    int victime = rng.Next(plantes.Count);
                    if (plantesProtegeesDansSerre.Contains(victime))
                    {
                        Console.WriteLine($"🛡️ {plantes[victime].Nom} est à l’abri dans la serre !\nLe rat n'a pas pu la grignoter.");
                    }
                    else
                    {
                        plantes[victime].Grignoter(rng, bacheDeployee);
                    }
                }
                AfficherEtatPlantes();
                plantesProtegeesDansSerre.Clear();
                bacheDeployee = 0;

                Console.WriteLine("\nActions d’urgence possibles :");
                Console.WriteLine("1. Faire du bruit (peut effrayer le rat)");
                Console.WriteLine("2. Fermer la serre (protection temporaire de certaines plantes)");
                Console.WriteLine("3. Déployer une bâche (rend toutes les plantes plus difficiles à grignoter)");
                Console.WriteLine("4. Installer un épouvantail (retarde la prochaine invasion)");
                Console.WriteLine("5. ☣ Terre brûlée ☣ : exterminez l'envahisseur au prix de vos plantations (il le mérite)");
                Console.WriteLine("👉 Choisissez une action d’urgence : ");
                ConsoleKeyInfo action = Console.ReadKey();
                Console.WriteLine();

                switch (action.KeyChar)
                {
                    case '1':
                        if (rng.NextDouble() > 0.7)
                        {
                            Console.WriteLine("🔊 Vous avez fait du bruit ! Le rat s’est enfui !");
                            ratPresent = false;
                        }
                        else
                        {
                            Console.WriteLine("😐 Le rat n’est pas impressionné...");
                        }
                        break;

                    case '2':
                        if (plantes.Count == 0)
                        {
                            Console.WriteLine("🚪 Serre fermée, mais aucune plante à protéger.");
                            break;
                        }

                        Console.WriteLine("🏡 Vous pouvez mettre 2 plantes à l’abri dans la serre.");
                        List<int> indexProteges = new List<int>();

                        for (int i = 0; i < 2 && i < plantes.Count; i++)
                        {
                            Console.WriteLine("\nSélectionnez la plante à protéger :");
                            for (int j = 0; j < plantes.Count; j++)
                            {
                                if (!indexProteges.Contains(j))
                                    Console.WriteLine($"{j + 1}. {plantes[j].Nom} ({plantes[j].Taille:F1} cm)");
                            }

                            Console.Write("> ");
                            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= plantes.Count && !indexProteges.Contains(choix - 1))
                            {
                                indexProteges.Add(choix - 1);
                                Console.WriteLine($"✅ {plantes[choix - 1].Nom} est protégée dans la serre.");
                            }
                            else
                            {
                                Console.WriteLine("❌ Choix invalide ou plante déjà protégée.");
                                i--; // Retenter ce tour
                            }
                        }

                        // Enregistrer les plantes protégées dans cette itération
                        plantesProtegeesDansSerre = indexProteges;
                        break;


                    case '3':
                        Console.WriteLine("🛡️ La bâche limite les dégâts pour ce tour.");
                        bacheDeployee = 1;
                        break;

                    case '4':
                        Console.WriteLine("🎭 Vous installez un épouvantail ! Moins de risques à l’avenir.");
                        probaInvasionRat -= rng.NextDouble() * 0.02;
                        if (probaInvasionRat < 0)
                        {
                            probaInvasionRat = 0;
                            Console.WriteLine("Félicitations ! Votre jardin est devenu tellement terrifiant que plus aucun rongeur n'osera s'y aventurer !");
                        }
                        break;
                    case '5':
                        Console.WriteLine("...🤨 Aucune maltraitance animale chez nous, votre identité a été signalée aux associations concernées.");
                        Console.WriteLine("Vous êtes également privé d'action 😤");
                        break;

                    default:
                        Console.WriteLine("❌ Action non reconnue. Le rat en profite !");
                        break;
                }

                toursRat++;
            }

            Console.WriteLine("✅ Fin de l’invasion de rats (pour cette fois). Restez vigilant !");
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
            Console.WriteLine("Quelle plante voulez-vous semer ? :\n1) : Tulipe\n2) : Tomate\n3) : Aubergine\n4) : Carotte\n5) : Courgette\n6) : Fraise\n7) : Laitue\n8) : Lavande\n9) : Menthe\n10) : Radis\n11) : Rose\n12) : Tournesol");
            while (true)
            {
                ConsoleKeyInfo entree = Console.ReadKey();
                Console.WriteLine();
                switch (entree.KeyChar.ToString())
                {
                    case "1": Console.WriteLine("🌷 Tulipe choisie."); return new Tulipe();
                    case "2": Console.WriteLine("🍅 Tomate choisie."); return new Tomate();
                    case "3": Console.WriteLine("🍆 Aubergine choisie."); return new Aubergine();
                    case "4": Console.WriteLine("🥕 Carotte choisie."); return new Carotte();
                    case "5": Console.WriteLine("🥒 Courgette choisie."); return new Courgette();
                    case "6": Console.WriteLine("🍓 Fraise choisie."); return new Fraise();
                    case "7": Console.WriteLine("🥬 Laitue choisie."); return new Laitue();
                    case "8": Console.WriteLine("💐 Lavande choisie."); return new Lavande();
                    case "9": Console.WriteLine("🌿 Menthe choisie."); return new Menthe();
                    case "10": Console.WriteLine("🌱 Radis choisi."); return new Radis();
                    case "11": Console.WriteLine("🌹 Rose choisie."); return new Rose();
                    case "12": Console.WriteLine("🌻 Tournesol choisi."); return new Tournesol();
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

            // Determination nouvel état de la plante
            Console.WriteLine("📈 Évolution hebdomadaire des plantes :");
            foreach (var plante in plantes.ToList())
            {
                double tauxConditions = plante.CalculerTauxConditions(temperature, luminosite);
                double tailleAvant = plante.Taille;
                plante.Pousser(tauxConditions);
                plante.AttraperMaladie(rng);

                if (plante.Taille > tailleAvant)
                    Console.WriteLine($"→ {plante.Nom} a grandi de {plante.Taille - tailleAvant:F1} cm.");

                if (!plante.VerifierSurvie(tauxConditions) || AgePlante(plantes.IndexOf(plante)) == plante.EsperanceDeVie)
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
