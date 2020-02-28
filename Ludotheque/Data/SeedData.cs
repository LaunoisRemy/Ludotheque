using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ludotheque.Data;
using System;
using System.Collections.Generic;
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

                //======================================= Add Editors =======================================
                if (!context.Editors.Any())
                {
                    context.Editors.AddRange(
                        new Editor
                        {
                            Name = "Asmodee",
                            Description = "Editeur Francais"
                        }
                    );
                }

                //======================================= Add Difficulty =======================================
                if (!context.Difficulties.Any())
                {
                    var diff = new Difficulty[]
                    {
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

                    };
                    foreach (var d in diff)
                    {
                        context.Difficulties.Add(d);
                    }
                    context.SaveChanges();
                }

                //======================================= Add Theme =======================================
                if (!context.Theme.Any())
                {
                    var themes = new Theme[]
                    {
                        new Theme
                        {
                            Name  = "FarWest"
                        },
                        new Theme
                        {
                            Name = "Abysse"
                        },
                        new Theme
                        {
                            Name = "Rêve"
                        }

                    };
                    foreach (var t in themes)
                    {
                        context.Theme.Add(t);
                    }
                }

                //======================================= Add Material Supports =======================================
                if (!context.MaterialSupport.Any())
                {
                    var ms = new MaterialSupport[]
                    {
                        new MaterialSupport
                        {
                            Name  = "Dé"
                        },
                        new MaterialSupport
                        {
                            Name = "Cartes"
                        },
                        new MaterialSupport
                        {
                            Name = "Plateau"
                        }

                    };
                    foreach (var m in ms)
                    {
                        context.MaterialSupport.Add(m);
                    }
                }

                //======================================= Add Mechanisms =======================================
                // Look for any games.
                if (!context.Mechanism.Any())
                {
                    var mecha = new Mechanism[]
                    {
                        new Mechanism
                        {
                            Name  = "Triche"
                        },
                        new Mechanism
                        {
                            Name = "Mensonges"
                        },
                        new Mechanism
                        {
                            Name = "Collaboration"
                        },
                        new Mechanism
                        {
                            Name = "Economie"
                        }

                    };
                    foreach (var m in mecha)
                    {
                        context.Mechanism.Add(m);
                    }
                }

                context.SaveChanges();

                //======================================= Add Games =======================================
                var difficultiesList = context.Difficulties;
                var mechanismsList = from m in context.Mechanism
                    where m.Name == "Collaboration" || m.Name == "Economie"
                                     select m;
                ICollection<Mechanism> AbyssMechanisms = new List<Mechanism>();
                foreach (var mechanism in mechanismsList)
                {
                 AbyssMechanisms.Add(mechanism);   
                }

                // Look for any games.
                if (!context.Games.Any())
                {
                    context.Games.AddRange(
                    new Game
                    {
                        Name = "Abyss",
                        Description = "Depuis des siècles, les Atlantes règnent sur les profondeurs des Océans. Leur royaume, Abyss, est respecté de tous les peuples alliés, heureux d'y trouver une protection contre les redoutables monstres sous-marins. Bientôt, le trône Atlante sera libre. Pour y accéder, fédérez les meilleurs représentants des peuples alliés, recrutez des seigneurs Atlantes et contrôlez les principaux territoires du royaume.",
                        Price = 37.99m,
                        MaxPlayer = 4,
                        MinPlayer = 2,
                        MinimumAge = 14,
                        DifficultyId = difficultiesList.Single(s => s.label == Label.Hard).Id
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
                        MinimumAge = 8,
                    }, new Game
                    {
                        Name = "Sheriff de Nottingham",
                        Description = "Jeu de commerce",
                        Price = 18,
                        MaxPlayer = 5,
                        MinPlayer = 3,
                        MinimumAge = 13,
                    }
                );
                    context.SaveChanges();
                }
                
            }
        }
    }
}
