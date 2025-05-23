using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    public class Tomate : Plante
    {
        // --- Constructeur ---
        public Tomate()
            : base(
                  nom: "Tomate",
                  estVivace: false,
                  estComestible: true,
                  saisonsSemis: new List<string> { "Printemps" },
                  typeTerrainPrefere: "Terre",
                  espacement: 30.0,
                  surfaceNecessaire: 0.2,
                  vitesseCroissance: 5.0, // 5 cm/semaine
                  besoinEau: 1.5, // litres/semaine
                  besoinLuminosite: 80.0, // en %
                  plageTemperature: Tuple.Create(15.0, 30.0),
                  maladiesPotentielles: new List<string> { "Mildiou", "Oïdium" },
                  esperanceDeVie: 6.0, // Semaines
                  productionMax: 8
                  )
            {}
        
        public override void Pousser(double tauxConditionsFavorables)
        {
            if (VerifierSurvie(tauxConditionsFavorables)) // Verifie si conditions remplis pour que la plante soit vivante
            {
                double croissance = VitesseCroissance * tauxConditionsFavorables;
                if (estMalade) croissance *= 0.5; // Ralentissement croissance si malade
                Taille += croissance;
                Console.WriteLine($"La tomate a poussé, taille actuelle: {Taille} cm");
            }
            else
            {
                Console.WriteLine("La tomate n'a pas survécu aux mauvaises conditions...");
            }
        }

    }
}