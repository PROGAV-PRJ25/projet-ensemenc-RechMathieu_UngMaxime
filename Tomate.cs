using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Tomate : Plante
    {
        public Tomate() : base(
            nom: "Tomate",
            estVivace: false,
            estComestible: true,
            saisonsSemis: new List<string> { "Printemps" },
            typeTerrainPrefere: "Terre",
            espacement: 30,
            surfaceNecessaire: 0.2,
            vitesseCroissance: 5.0,
            besoinEau: 1.5,
            besoinLuminosite: 80,
            plageTemperature: Tuple.Create(15.0, 30.0),
            maladiesPotentielles: new List<string> { "Mildiou", "Oïdium" },
            esperanceDeVie: 6,
            productionMax: 8
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables > 0.4)
            {
                double croissance = VitesseCroissance * tauxConditionsFavorables;
                if (estMalade) croissance *= 0.3;
                Taille += croissance;
                Console.WriteLine($"🍅 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🍅 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= EsperanceDeVie / 2;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🍅 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}