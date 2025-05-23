using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public abstract class Terrain
    {
        // --- ATTRIBUTS PRIVÉS ---
        private string nom;
        private double capaciteEau;
        private double fertilite;
        private double quantiteEauActuelle;

        // --- PROPRIÉTÉS PUBLIQUES ---
        public string Nom { get { return nom; } }
        public double CapaciteEau { get { return capaciteEau; } }
        public double Fertilite { get { return fertilite; } }
        public double QuantiteEauActuelle { get { return quantiteEauActuelle; } }


        // --- CONSTRUCTEUR ---
        public Terrain(string nom, double capaciteEau, double fertilite)
        {
            this.nom = nom;
            this.capaciteEau = capaciteEau;
            this.fertilite = fertilite;
            this.quantiteEauActuelle = capaciteEau / 2;
        }

        // --- MÉTHODES COMMUNES ---
        public void AjouterEau(double quantite)
        {
            quantiteEauActuelle += quantite;

            if (quantiteEauActuelle > capaciteEau)
                quantiteEauActuelle = capaciteEau; // 🔧 Ne jamais dépasser la capacité max
        }
        
        // Affichage détaillée des propriétés du type de terrain
        public string AfficherProprietes()
        {
            return $"\n🌍 Fiche terrain : {Nom}\n" +
                $"- Capacité d'eau : {CapaciteEau} L/m²\n" +
                $"- Fertilité : {Fertilite}/100\n" +
                $"- Eau actuelle : {QuantiteEauActuelle:F1} L/m²";
        }

    }
}