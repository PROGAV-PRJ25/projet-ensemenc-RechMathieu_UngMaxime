using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Tournesol : Plante
    {
        public Tournesol() : base(
            nom: "Tournesol",
            estVivace: false,
            estComestible: false,
            saisonsSemis: new List<string> { "Printemps" },
            typeTerrainPrefere: "Terre",
            espacement: 30,
            surfaceNecessaire: 0.2,
            vitesseCroissance: 4.0,
            besoinEau: 0.8,
            besoinLuminosite: 90,
            plageTemperature: Tuple.Create(18.0, 30.0),
            maladiesPotentielles: new List<string> { "Rouille", "Pucerons" },
            esperanceDeVie: 8,
            productionMax: 1
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables >= 0.4)
            {
                double croissance = VitesseCroissance * tauxConditionsFavorables;
                if (estMalade) croissance *= 0.7;
                Taille += croissance;
                Console.WriteLine($"🌻 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🌻 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= 6 && Taille > 100.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🌻 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}
