using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public abstract class Plante
    {
        // --- ATTRIBUTS PRIVÉS ---
        private string nom;
        private bool estVivace; // true = vivace, false = annuelle
        private bool estComestible;
        private List<string> saisonsSemis; // Hiver, Printemps, Ete, Automne
        private string typeTerrainPrefere; // Terre, Sable, Argile
        private double espacement; // entre 2 plants en cm
        private double surfaceNecessaire; // en m²
        private double vitesseCroissance; // en cm/semaine
        private double besoinEau; // en L/semaine
        private double besoinLuminosite; // en % de luminsité par semaine
        private Tuple<double, double> plageTemperature; // min et max en °C
        private List<string> maladiesPotentielles;
        private double esperanceDeVie; // en semaine
        private int productionMax; // Nombre maximal de produits (fruits, légumes, etc.)
        private Terrain terrainAssocie = null!;

        // --- ATTRIBUTS PROTEGES ---
        protected bool estMalade = false;

        // --- PROPRIÉTÉS PUBLIQUES ---
        public double Taille { get; protected set; } // protected set pour accès par les classes dérivées
        public string Nom { get => nom; }
        public bool EstVivace { get => estVivace; }
        public bool EstComestible { get => estComestible; }
        public List<string> SaisonsSemis { get => saisonsSemis; }
        public string TypeTerrainPrefere { get => typeTerrainPrefere; }
        public double Espacement { get => espacement; }
        public double SurfaceNecessaire { get => surfaceNecessaire; }
        public double VitesseCroissance { get => vitesseCroissance; }
        public double BesoinEau { get => besoinEau; }
        public double BesoinLuminosite { get => besoinLuminosite; }
        public Tuple<double, double> PlageTemperature { get => plageTemperature; }
        public List<string> MaladiesPotentielles { get => maladiesPotentielles; }
        public double EsperanceDeVie { get => esperanceDeVie; }
        public int ProductionMax { get => productionMax; }
        public Terrain TerrainAssocie { get { return terrainAssocie; } }
        public bool EstMalade => estMalade;

        // --- CONSTRUCTEUR ---
        public Plante(string nom, bool estVivace, bool estComestible, List<string> saisonsSemis,
                      string typeTerrainPrefere, double espacement, double surfaceNecessaire,
                      double vitesseCroissance, double besoinEau, double besoinLuminosite,
                      Tuple<double, double> plageTemperature, List<string> maladiesPotentielles,
                      double esperanceDeVie, int productionMax)
        {
            this.nom = nom;
            this.estVivace = estVivace;
            this.estComestible = estComestible;
            this.saisonsSemis = saisonsSemis;
            this.typeTerrainPrefere = typeTerrainPrefere;
            this.espacement = espacement;
            this.surfaceNecessaire = surfaceNecessaire;
            this.vitesseCroissance = vitesseCroissance;
            this.besoinEau = besoinEau;
            this.besoinLuminosite = besoinLuminosite;
            this.plageTemperature = plageTemperature;
            this.maladiesPotentielles = maladiesPotentielles;
            this.esperanceDeVie = esperanceDeVie;
            this.productionMax = productionMax;
            this.Taille = 0.0;

        }


        // --- MÉTHODES ABSTRAITES ---

        // Fait pousser la plante en fonction du taux de conditions favorables
        public abstract void Pousser(double tauxConditionsFavorables);
        // 🔧 Conditions de récolte
        public abstract bool PeutEtreRecoltee(int age);


        // --- MÉTHODES VIRTUAL ---

        // Comportement standard pour attraper les maladies (15% de chances d'en attraper)
        public virtual void AttraperMaladie(Random rng)
        {
            if (MaladiesPotentielles.Count > 0 && rng.NextDouble() < 0.15)
            {
                string maladie = MaladiesPotentielles[rng.Next(MaladiesPotentielles.Count)];
                estMalade = true;
                Console.WriteLine($"⚠️ {Nom} a été infectée par : {maladie} !");
            }
            else
            {
                estMalade = false;
                Console.WriteLine($"{Nom} est en bonne santé.");
            }
        }

        // --- MÉTHODES COMMUNES ---

        //Association d'un terrain à la plante
        public void AssocierTerrain(Terrain terrain)
        { this.terrainAssocie = terrain; }

        // Calcul du taux de conditions optimales à la survie
        public double CalculerTauxConditions(double temperature, double luminosite)
        {
            if (terrainAssocie == null)
                return 0.0;

            double score = 0;
            int critères = 5; // Nombre de critères

            // critère 1. Type de terrain préféré : si planté dans terrain préféré 1/1, sinon 0/1
            if (terrainAssocie.Nom == TypeTerrainPrefere) score += 1.0;

            // critère 2. Eau : comparer QuantiteEauActuelle * surface vs BesoinEau
            double eauDispo = terrainAssocie.QuantiteEauActuelle * SurfaceNecessaire;
            double diffEau = Math.Abs(eauDispo - BesoinEau); // Pas de differenciation entre trop et pas assez d'eau pour le moment
            score += 1.0 - Math.Min(1.0, diffEau / 5.0); // Tolérance ±5L

            // critère 3. Fertilité
            score += terrainAssocie.Fertilite / 100.0;

            // Critère 4 : Température
            if (temperature >= PlageTemperature.Item1 && temperature <= PlageTemperature.Item2)
            {
                score += 1.0;
            }
            else
            {
                double ecart = Math.Min(Math.Abs(temperature - PlageTemperature.Item1),
                                        Math.Abs(temperature - PlageTemperature.Item2));
                score += 1.0 - Math.Min(1.0, ecart / 5.0); // marge de 5°C
            }

            // Critère 5 : Luminosité
            double diffLumi = Math.Abs(luminosite - BesoinLuminosite); // en %
            score += 1.0 - Math.Min(1.0, diffLumi / 10.0); // tolérance ±10%

            return score / critères;
        }

        // Vérifie si la plante survit selon le taux de respect des conditions optimales
        public bool VerifierSurvie(double tauxConditionsRespectees)
        {
            return tauxConditionsRespectees >= 0.5;
        }

        public void Grignoter(Random rng, int bacheDeployee)
        {
            if (estComestible)
            {
                double tailleAvant = Taille;
                double degats = Math.Max(rng.NextDouble() * 2.0 - bacheDeployee, 0);
                Taille = Math.Max(Taille - degats, 0);
                Console.WriteLine($"{Nom} a été grignotée ! Elle a perdu {tailleAvant - Taille:F1} cm...");
            }
            else
            {
                Console.WriteLine($"{Nom} n'est pas comestible, elle a résisté à la visite du rat !");
            }
        }


        // Affichage détaillée des propriétés du type de plante
        public string AfficherProprietes()
        {
            return $"\n📋 Fiche plante : {Nom}\n" +
                $"- Type : {(EstVivace ? "Vivace" : "Annuelle")}\n" +
                $"- Terrain préféré : {TypeTerrainPrefere}\n" +
                $"- Saisons de semis : {string.Join(", ", SaisonsSemis)}\n" +
                $"- Besoin en eau : {BesoinEau} L/semaine\n" +
                $"- Besoin en lumière : {BesoinLuminosite}%\n" +
                $"- Température idéale : {PlageTemperature.Item1}°C à {PlageTemperature.Item2}°C\n" +
                $"- Espérance de vie : {EsperanceDeVie} semaines\n" +
                $"- Production max : {ProductionMax}";
        }

        // Affichage synthétique de l'état de la plante
        public override string ToString()
        {
            string etat = estMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🌿 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"}| {etat}";
        }

    }
}