using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Laitue : Plante
    {
        public Laitue() : base(
            nom: "Laitue",
            estVivace: false,
            estComestible: true,
            saisonsSemis: new List<string> { "Automne" },
            typeTerrainPrefere: "Argile",
            espacement: 20,
            surfaceNecessaire: 0.1,
            vitesseCroissance: 1.8,
            besoinEau: 1.3,
            besoinLuminosite: 65,
            plageTemperature: Tuple.Create(5.0, 20.0),
            maladiesPotentielles: new List<string> { "Sclerotinia" },
            esperanceDeVie: 7,
            productionMax: 3
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables >= 0.3)
            {
                double croissance = VitesseCroissance * tauxConditionsFavorables;
                if (estMalade || tauxConditionsFavorables < 0.4) croissance *= 0.3;
                Taille += croissance;
                Console.WriteLine($"🥬 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🥬 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= EsperanceDeVie * 0.8 || Taille > 18.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🥬 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}