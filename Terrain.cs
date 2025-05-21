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

        // --- PROPRIÉTÉS PUBLIQUES ---
        public string Nom { get { return nom; } }
        public double CapaciteEau { get { return capaciteEau; } }
        public double Fertilite { get { return fertilite; } }

        // --- CONSTRUCTEUR ---
        public Terrain(string nom, double capaciteEau, double fertilite)
        {
            this.nom = nom;
            this.capaciteEau = capaciteEau;
            this.fertilite = fertilite;
        }

         public override string ToString()
        {
            return $"{Nom} - Eau : {CapaciteEau} L/m² - Fertilité : {Fertilite}/100";
        }
    }
}