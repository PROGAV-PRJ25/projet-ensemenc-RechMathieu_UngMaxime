using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Carotte : Plante
    {
        public Carotte() : base(
            nom: "Carotte",
            estVivace: false,
            estComestible: true,
            saisonsSemis: new List<string> { "Printemps", "Ete", "Automne", "Hiver" },
            typeTerrainPrefere: "Sable",
            espacement: 10,
            surfaceNecessaire: 0.08,
            vitesseCroissance: 1.2,
            besoinEau: 1.0,
            besoinLuminosite: 60,
            plageTemperature: Tuple.Create(10.0, 24.0),
            maladiesPotentielles: new List<string> { "Alternariose" },
            esperanceDeVie: 9,
            productionMax: 6
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables > 0.35)
            {
                double croissance = VitesseCroissance + (tauxConditionsFavorables * 0.5);
                if (estMalade) croissance *= 0.5;
                Taille += croissance;
                Console.WriteLine($"🥕 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🥕 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return Taille >= 12.0 && age >= 5;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🥕 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}
