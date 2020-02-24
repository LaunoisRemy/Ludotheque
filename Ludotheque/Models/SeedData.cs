using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ludotheque.Data;
using System;
using System.Linq;

namespace Ludotheque.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LudothequeContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LudothequeContext>>()))
            {
                // Look for any jeu.
                if (context.Jeu.Any())
                {
                    return;   // DB has been seeded
                }

                context.Jeu.AddRange(
                    new Jeu
                    {
                        Nom = "Abyss",
                        Description = "Jeu dans les abysses",
                        Prix = 34,
                        NbMaxJoueur = 4,
                        NbMinJoueur = 2,
                        AgeMinimum = 8,
                        Categorie = "Jeu plateau"
                    },
                    new Jeu
                    {
                        Nom = "Secret Hitler",
                        Description = "jeu avec rôles cachés",
                        Prix = 0,
                        NbMaxJoueur = 8,
                        NbMinJoueur = 6,
                        AgeMinimum = 8,
                        Categorie = "Jeu mensonges"
                    }, 
                    new Jeu
                    {
                        Nom = "Complot",
                        Description = "jeu avec rôles cachés",
                        Prix = 15.99m,
                        NbMaxJoueur = 8,
                        NbMinJoueur = 6,
                        AgeMinimum = 8,
                        Categorie = "Jeu cartes"
                    }, new Jeu
                    {
                        Nom = "Abyss",
                        Description = "Jeu dans les abysses",
                        Prix = 34,
                        NbMaxJoueur = 4,
                        NbMinJoueur = 2,
                        AgeMinimum = 8,
                        Categorie = "Jeu plateau"
                    },
                    new Jeu
                    {
                        Nom = "Secret Hitler",
                        Description = "jeu avec rôles cachés",
                        Prix = 0,
                        NbMaxJoueur = 8,
                        NbMinJoueur = 6,
                        AgeMinimum = 8,
                        Categorie = "Jeu mensonges"
                    },
                    new Jeu
                    {
                        Nom = "Complot",
                        Description = "jeu avec rôles cachés",
                        Prix = 15.99m,
                        NbMaxJoueur = 8,
                        NbMinJoueur = 6,
                        AgeMinimum = 8,
                        Categorie = "Jeu cartes"
                    },new Jeu
                    {
                        Nom = "Abyss",
                        Description = "Jeu dans les abysses",
                        Prix = 34,
                        NbMaxJoueur = 4,
                        NbMinJoueur = 2,
                        AgeMinimum = 8,
                        Categorie = "Jeu plateau"
                    },
                    new Jeu
                    {
                        Nom = "Secret Hitler",
                        Description = "jeu avec rôles cachés",
                        Prix = 0,
                        NbMaxJoueur = 8,
                        NbMinJoueur = 6,
                        AgeMinimum = 8,
                        Categorie = "Jeu mensonges"
                    }, 
                    new Jeu
                    {
                        Nom = "Complot",
                        Description = "jeu avec rôles cachés",
                        Prix = 15.99m,
                        NbMaxJoueur = 8,
                        NbMinJoueur = 6,
                        AgeMinimum = 8,
                        Categorie = "Jeu cartes"
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
