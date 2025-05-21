using System;
using System.Collections.Generic;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Tulipe : Plante
    {
        public Tulipe(string nom = "Tulipe") : base(
            nom,
            estVivace: true,
            estComestible: false,
            saisonsSemis: new List<string> { "Automne" }, // les bulbes se plantent à l'automne
            typeTerrainPrefere: "Terre",
            espacement: 10,                // en cm
            surfaceNecessaire: 0.1,        // en m²
            vitesseCroissance: 1.0,        // cm/semaine
            besoinEau: 1.5,                // L/semaine
            besoinLuminosite: 70,          // % de lumière
            plageTemperature: Tuple.Create(5.0, 20.0), // température idéale en °C
            maladiesPotentielles: new List<string> { "Botrytis", "Fusariose" },
            esperanceDeVie: 12,            // en semaines
            productionMax: 1               // 1 fleur par bulbe
        )
        { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            double croissanceHebdo = VitesseCroissance * tauxConditionsFavorables;
            Taille += croissanceHebdo;
            Console.WriteLine($"{Nom} a poussé de {croissanceHebdo:F1} cm cette semaine. Hauteur totale : {taille:F1} cm.");
        }

        public override void AttraperMaladie(Random rng)
        {
            if (MaladiesPotentielles.Count > 0 && rng.NextDouble() < 0.15) // 15% de risque
            {
                string maladie = MaladiesPotentielles[rng.Next(MaladiesPotentielles.Count)];
                Console.WriteLine($"⚠️ {Nom} a été infectée par : {maladie} !");
            }
        }

        public override string ToString()
        {
            return $"🌷 {Nom} - Vivace : {EstVivace}, Terrain : {TypeTerrainPrefere}, Hauteur : {Taille:F1} cm";
        }
    }
}
