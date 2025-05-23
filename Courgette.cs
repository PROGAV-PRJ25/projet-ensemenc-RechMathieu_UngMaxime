using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Courgette : Plante
    {
        public Courgette() : base(
            nom: "Courgette",
            estVivace: false,
            estComestible: true,
            saisonsSemis: new List<string> { "Printemps", "Ete" },
            typeTerrainPrefere: "Terre",
            espacement: 50,
            surfaceNecessaire: 0.5,
            vitesseCroissance: 4.0,
            besoinEau: 2.0,
            besoinLuminosite: 85,
            plageTemperature: Tuple.Create(18.0, 35.0),
            maladiesPotentielles: new List<string> { "Oïdium" },
            esperanceDeVie: 8,
            productionMax: 6
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables > 0.5)
            {
                double croissance = VitesseCroissance * tauxConditionsFavorables;
                if (estMalade) croissance *= 0.6;
                Taille += croissance;
                Console.WriteLine($"🥒 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🥒 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= 4 && Taille >= 20.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🥒 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}
