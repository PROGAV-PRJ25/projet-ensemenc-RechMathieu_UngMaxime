using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Menthe : Plante
    {
        public Menthe() : base(
            nom: "Menthe",
            estVivace: true,
            estComestible: true,
            saisonsSemis: new List<string> { "Printemps" },
            typeTerrainPrefere: "Argile",
            espacement: 25,
            surfaceNecessaire: 0.1,
            vitesseCroissance: 3.5,
            besoinEau: 2.5,
            besoinLuminosite: 50,
            plageTemperature: Tuple.Create(10.0, 28.0),
            maladiesPotentielles: new List<string> { "Rouille" },
            esperanceDeVie: 14,
            productionMax: 5
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables > 0.2)
            {
                double croissance = (tauxConditionsFavorables > 0.8 ? 1.2 : 1.0) * VitesseCroissance * tauxConditionsFavorables;
                if (estMalade) croissance *= 0.5;
                Taille += croissance;
                Console.WriteLine($"🌿 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🌿 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= 4 && Taille >= 10.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🌿 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}
