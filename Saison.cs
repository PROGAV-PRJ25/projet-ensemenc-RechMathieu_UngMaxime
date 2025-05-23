using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public enum SaisonNom { Hiver, Printemps, Ete, Automne }

    public class Saison
    {
        public SaisonNom Nom { get; private set; }
        public int NumeroSemaine { get; private set; }
        private Random rng;

        public Saison(int semaine, Random rng)
        {
            NumeroSemaine = semaine;
            this.rng = rng;
            Nom = DeterminerSaison(semaine);
        }

        // Déterminer la saison en fonction de la semaine
        private SaisonNom DeterminerSaison(int semaine)
        {
            int s = (semaine - 1) % 52;
            if (s < 13) return SaisonNom.Hiver;
            if (s < 26) return SaisonNom.Printemps;
            if (s < 39) return SaisonNom.Ete;
            return SaisonNom.Automne;
        }

        // Générer la température en fonction de la saison en °C
        public double GenererTemperature()
        {
            switch (Nom)
            {
                case SaisonNom.Hiver:
                    return rng.Next(-2, 10);
                case SaisonNom.Printemps:
                    return rng.Next(8, 20);
                case SaisonNom.Ete:
                    return rng.Next(18, 35);
                case SaisonNom.Automne:
                    return rng.Next(5, 18);
                default:
                    return rng.Next(10, 20);
            }
        }

        // Générer les précipitations en fonction de la saison en L/m²
        public double GenererPrecipitations()
        {
            switch (Nom)
            {
                case SaisonNom.Hiver:
                    return rng.NextDouble() * 5.0; // Saison plus humide
                case SaisonNom.Printemps:
                    return rng.NextDouble() * 4.0;
                case SaisonNom.Ete:
                    return rng.NextDouble() * 2.0; // Saison plus sèche
                case SaisonNom.Automne:
                    return rng.NextDouble() * 4.0;
                default:
                    return rng.NextDouble() * 3.0;
            }
        }

        public double GenererLuminosite()
        {
            switch (Nom)
            {
                case SaisonNom.Ete: return rng.Next(75, 100);    
                case SaisonNom.Printemps: return rng.Next(60, 85);
                case SaisonNom.Automne: return rng.Next(40, 70);
                case SaisonNom.Hiver: return rng.Next(20, 50);   
                default: return rng.Next(40, 80);
            }
        }


        public override string ToString()
        {
            return $"🗓️ Semaine {NumeroSemaine} - Saison : {Nom}";
        }
    }
}