using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Aubergine : Plante
    {
        public Aubergine() : base(
            nom: "Aubergine",
            estVivace: false,
            estComestible: true,
            saisonsSemis: new List<string> { "Printemps" },
            typeTerrainPrefere: "Argile",
            espacement: 40,
            surfaceNecessaire: 0.3,
            vitesseCroissance: 3.2,
            besoinEau: 2.2,
            besoinLuminosite: 90,
            plageTemperature: Tuple.Create(20.0, 35.0),
            maladiesPotentielles: new List<string> { "Verticilliose" },
            esperanceDeVie: 10,
            productionMax: 5
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables >= 0.3)
            {
                double croissance = VitesseCroissance * (0.8 + 0.4 * tauxConditionsFavorables);
                if (estMalade) croissance *= 0.4;
                Taille += croissance;
                Console.WriteLine($"🍆 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🍆 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= 6 && Taille > 30.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🍆 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}
