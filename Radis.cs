using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Radis : Plante
    {
        public Radis() : base(
            nom: "Radis",
            estVivace: false,
            estComestible: true,
            saisonsSemis: new List<string> { "Automne", "Printemps" },
            typeTerrainPrefere: "Sable",
            espacement: 5,
            surfaceNecessaire: 0.05,
            vitesseCroissance: 1.0,
            besoinEau: 0.8,
            besoinLuminosite: 50,
            plageTemperature: Tuple.Create(8.0, 22.0),
            maladiesPotentielles: new List<string> { "Altise" },
            esperanceDeVie: 5,
            productionMax: 2
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables > 0.3)
            {
                double croissance = VitesseCroissance + (tauxConditionsFavorables * 1.0);
                if (estMalade) croissance *= 0.7;
                Taille += croissance;
                Console.WriteLine($"🌱 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🌱 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return Taille >= 6.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🌱 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}
