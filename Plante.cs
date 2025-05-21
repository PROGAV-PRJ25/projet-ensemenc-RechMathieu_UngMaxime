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
        private Terrain terrainAssocie;

        // --- PROPRIÉTÉS PUBLIQUES ---
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
        // Classe protégé pour accès par les classes dérivées
        public double Taille { get; protected set; }

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
            this.terrainAssocie = null; // 🔧 INIT terrain à null

        }


        // --- MÉTHODES ABSTRAITES ---

        // Fait pousser la plante en fonction du taux de conditions favorables
        public abstract void Pousser(double tauxConditionsFavorables);

        // Simule la contamination par une maladie
        public abstract void AttraperMaladie(Random rng);


        // --- MÉTHODES COMMUNES ---

        //Association d'un terrain à la plante
        public void AssocierTerrain(Terrain terrain)
        {
            this.terrainAssocie = terrain;
        }

        // Calcul du taux de conditions optimales à la survie

        public double CalculerTauxConditions(double temperature)
        {
            if (terrainAssocie == null)
                return 0.0;

            double score = 0;
            int critères = 4; // Nombre de critères

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

            return score / critères;
        }

        // Vérifie si la plante survit selon le taux de respect des conditions optimales
        public bool VerifierSurvie(double tauxConditionsRespectees)
        {
            return tauxConditionsRespectees >= 0.5;
        }

        // Affiche un résumé de l'état de la plante
        public override string ToString()
        {
            return $"Nom: {Nom}, Vivace: {EstVivace}, Comestible: {EstComestible}, Terrain préféré: {TypeTerrainPrefere}, Taille: {Taille} cm";
        }

    }
}