using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Lavande : Plante
    {
        public Lavande() : base(
            nom: "Lavande",
            estVivace: true,
            estComestible: false,
            saisonsSemis: new List<string> { "Printemps", "Ete" },
            typeTerrainPrefere: "Sable",
            espacement: 25,
            surfaceNecessaire: 0.15,
            vitesseCroissance: 1.8,
            besoinEau: 0.7,
            besoinLuminosite: 85,
            plageTemperature: Tuple.Create(15.0, 35.0),
            maladiesPotentielles: new List<string> { "Septoriose" },
            esperanceDeVie: 15,
            productionMax: 2
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables >= 0.5)
            {
                double croissance = VitesseCroissance * tauxConditionsFavorables;
                if (estMalade) croissance *= 0.7;
                Taille += croissance;
                Console.WriteLine($"💐 {Nom} a poussé de {croissance:F1} cm. Taille : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"💐 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= 5 && Taille >= 15.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"💐 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}
