using System;

namespace projet_ensemenc_RechMathieu_UngMaxime
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rng = new Random(); 

            // Création d'une tomate
            Tomate tomate = new Tomate();

            Console.WriteLine("*** Etat initial de la plante ***");
            Console.WriteLine(tomate);

            Console.WriteLine("\n*** Simulation de croissance ***");
            tomate.Pousser(0.8); // 80% de conditions favorables -> faudra determiner un calcul automatique

            Console.WriteLine("\n*** Tentative d'infection ***");
            tomate.AttraperMaladie(rng);

            Console.WriteLine("\n*** Etat final de la plante ***");
            Console.WriteLine(tomate);

        }
    }
}
