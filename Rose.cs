using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Rose : Plante
    {
        public Rose() : base(
            nom: "Rose",
            estVivace: true,
            estComestible: false,
            saisonsSemis: new List<string> { "Printemps" },
            typeTerrainPrefere: "Argile",
            espacement: 30,
            surfaceNecessaire: 0.2,
            vitesseCroissance: 2.0,
            besoinEau: 1.5,
            besoinLuminosite: 75,
            plageTemperature: Tuple.Create(10.0, 28.0),
            maladiesPotentielles: new List<string> { "Taches noires", "Oïdium" },
            esperanceDeVie: 20,
            productionMax: 3
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables >= 0.3)
            {
                double croissance = VitesseCroissance * tauxConditionsFavorables;
                if (estMalade) croissance *= 0.6;
                Taille += croissance;
                Console.WriteLine($"🌹 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🌹 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= 6 && Taille >= 30.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🌹 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}
