using System;
using System.Collections.Generic;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Tulipe : Plante
    {
        public Tulipe() : base(
            nom: "Tulipe",
            estVivace: true,
            estComestible: false,
            saisonsSemis: new List<string> { "Automne" },
            typeTerrainPrefere: "Terre",
            espacement: 10,
            surfaceNecessaire: 0.1,
            vitesseCroissance: 1.0,
            besoinEau: 1.5,
            besoinLuminosite: 70,
            plageTemperature: Tuple.Create(5.0, 20.0),
            maladiesPotentielles: new List<string> { "Botrytis", "Fusariose" },
            esperanceDeVie: 12,
            productionMax: 1
        ) { }

        public override void Pousser(double tauxConditionsFavorables)
        {
            if (tauxConditionsFavorables > 0.4)
            {
                double croissanceHebdo = tauxConditionsFavorables > 0.5 ? VitesseCroissance * tauxConditionsFavorables : VitesseCroissance * 0.2;
                if (estMalade) croissanceHebdo *= 0.4;
                Taille += croissanceHebdo;
                Console.WriteLine($"🌷 {Nom} a poussé de {croissance:F1} cm. Hauteur : {Taille:F1} cm");
            }
            else
            {
                Console.WriteLine($"🌷 {Nom} n’a pas poussé cette semaine faute de bonnes conditions.");
            }
        }

        public override bool PeutEtreRecoltee(int age)
        {
            return age >= 4 && Taille >= 15.0;
        }

        public override string ToString()
        {
            string etat = EstMalade ? "❌ est Malade" : "✅ n'est pas Malade";
            return $"🌷 {Nom} | Taille : {Taille:F1} cm | Terrain : {TerrainAssocie?.Nom ?? "Aucun"} | {etat}";
        }
    }
}

