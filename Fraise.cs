using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Fraise : Plante
    {
        public Fraise() : base(
            nom: "Fraise",
            estVivace: true,
            estComestible: true,
            saisonsSemis: new List<string> { "Printemps" },
            typeTerrainPrefere: "Terre",
            espacement: 20,
            surfaceNecessaire: 0.15,
            vitesseCroissance: 2.0,
            besoinEau: 1.2,
            besoinLuminosite: 70,
            plageTemperature: Tuple.Create(10.0, 25.0),
            maladiesPotentielles: new List<string> { "Botrytis", "Oïdium" },
            esperanceDeVie: 10,
            productionMax: 12
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables > 0.4)
            {
                double croissance = VitesseCroissance * (0.5 + tauxConditionsFavorables);
                if (estMalade) croissance *= 0.6;
                Taille += croissance;
                Console.WriteLine($"🍓 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🍓 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= 5 && Taille >= 10.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🍓 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}