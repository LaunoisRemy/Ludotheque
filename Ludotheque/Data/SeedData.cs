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
                if (context.Editors.Any())
                {
                    return;   // DB has been seeded
                }

                context.Editors.AddRange(
                    new Editor
                    {
                        Name = "Asmodee",
                        Description = "Editeur Francais"
                    }
                    );
                if (context.Difficulties.Any())
                {
                    return;   // DB has been seeded
                }

                context.Difficulties.AddRange(
                    new Difficulty
                    {
                        label  = Label.Easy
                    },
                    new Difficulty
                    {
                        label = Label.Moderate
                    },
                    new Difficulty
                    {
                        label = Label.Hard
                    }
                );

                // Look for any games.
                if (context.Games.Any())
                {
                    return;   // DB has been seeded
                }

                context.Games.AddRange(
                    new Game
                    {
                        Name = "Abyss",
                        Description = "Depuis des siècles, les Atlantes règnent sur les profondeurs des Océans. Leur royaume, Abyss, est respecté de tous les peuples alliés, heureux d'y trouver une protection contre les redoutables monstres sous-marins. Bientôt, le trône Atlante sera libre. Pour y accéder, fédérez les meilleurs représentants des peuples alliés, recrutez des seigneurs Atlantes et contrôlez les principaux territoires du royaume.",
                        Price = 37.99m,
                        MaxPlayer = 4,
                        MinPlayer = 2,
                        MinimumAge = 14
                    },
                    new Game
                    {
                        Name = "Secret Hitler",
                        Description = "jeu avec rôles cachés",
                        Price = 0,
                        MaxPlayer = 10,
                        MinPlayer = 5,
                        MinimumAge = 18
                    }, 
                    new Game
                    {
                        Name = "Complot",
                        Description = "jeu avec rôles cachés",
                        Price = 11.99m,
                        MaxPlayer = 8,
                        MinPlayer = 2,
                        MinimumAge = 8
                    }, new Game
                    {
                        Name = "Bang the bullet",
                        Description = "Jeu western ",
                        Price = 36.00m,
                        MaxPlayer = 8,
                        MinPlayer = 3,
                        MinimumAge = 8
                    }, new Game
                    {
                        Name = "Bang",
                        Description = "Jeu western",
                        Price = 18,
                        MaxPlayer = 7,
                        MinPlayer = 4,
                        MinimumAge = 8
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
