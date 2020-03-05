using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ludotheque.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ludotheque.Areas.Identity.Data;

namespace Ludotheque.Models
{
    public class SeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new LudothequeAccountContext(
            serviceProvider.GetRequiredService<DbContextOptions<LudothequeAccountContext>>()))
            {
                var userManager = serviceProvider.GetService<UserManager<LudothequeUser>>();
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();


                await SeedDB(context, userManager, roleManager);
            }

        }
     
        public static  async Task SeedDB(LudothequeAccountContext context, UserManager<LudothequeUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            //======================================= Delete all data (temporary) =======================================

            //DropAllData(context);


            if (!roleManager.RoleExistsAsync("Admin").Result )
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = "Admin"
                };
                await roleManager.CreateAsync(identityRole);

            }
            if (userManager.FindByEmailAsync("spprtludotheque@gmail.com").Result == null)
            {
                LudothequeUser identityUser = new LudothequeUser
                {
                    UserName = "spprtludotheque@gmail.com",
                    Email = "spprtludotheque@gmail.com",
                    EmailConfirmed = true
                };
                IdentityResult result = userManager.CreateAsync(identityUser, "Password.1234").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(identityUser,"Admin").Wait();
                }

            };
                //======================================= Add Editors =======================================
                if (!context.Editors.Any())
                {
                    context.Editors.AddRange(

                    );
                    var editors = new Editor[]
                    {
                        new Editor
                        {
                            Name = "Asmodee",
                            Description = "Editeur Francais"
                        },
                        new Editor
                        {
                            Name = "Geek Attitude Games",
                        },
                        new Editor
                        {
                            Name = "Repos Production",
                            Description = "éditeur européen de jeux de société"
                        }
                    };
                    AddInDataBase(editors, context);

                }
            //======================================= Add Illustrators =======================================
            if (!context.Illustrators.Any())
            {
                context.Illustrators.AddRange(

                );
                var illu = new Illustrator[]
                {
                        new Illustrator
                        {
                            FirstName = "Alex ",
                            LastName = "Pierangelini"
                        },
                        new Illustrator
                        {
                            FirstName = "Xavier ",
                            LastName = "Colette"
                        }
                };
                AddInDataBase(illu, context);

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
                    AddInDataBase(diff, context);

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
                        },
                        new Theme
                        {
                            Name = "Science fiction"
                        }

                    };
                    AddInDataBase(themes, context);

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
                        },
                        new MaterialSupport
                        {
                            Name = "Figurines"
                        }

                    };
                    AddInDataBase(ms, context);

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
                        },
                        new Mechanism
                        {
                            Name = "Physique"
                        },
                        new Mechanism
                        {
                            Name = "Devinettes"
                        }

                    };
                    AddInDataBase(mecha, context);

                }

                //======================================= TAll difficulties =======================================

                var difficultiesList = context.Difficulties;
                var ediors = context.Editors;
                var illustrat = context.Illustrators;

                //======================================= Add Games =======================================

                // Look for any games.
                if (!context.Games.Any())
                {
                var games = new Game[]
                {
                        new Game
                        {
                            Name = "Abyss",
                            Description = "Depuis des siècles, les Atlantes règnent sur les profondeurs des Océans. Leur royaume, Abyss, est respecté de tous les peuples alliés, heureux d'y trouver une protection contre les redoutables monstres sous-marins. Bientôt, le trône Atlante sera libre. Pour y accéder, fédérez les meilleurs représentants des peuples alliés, recrutez des seigneurs Atlantes et contrôlez les principaux territoires du royaume.",
                            Price = 37.99m,
                            MaxPlayer = 4,
                            MinPlayer = 2,
                            MinimumAge = 14,
                            DifficultyId = difficultiesList.Single(s => s.label == Label.Hard).Id,
                            EditorId = ediors.Single(s => s.Name == "Asmodee").Id,
                            IllustratorId = illustrat.Single(s => s.LastName =="Colette").Id,
                            Validate = true
                        },
                        new Game
                        {
                            Name = "Secret Hitler",
                            Description = "jeu avec rôles cachés",
                            Price = 0,
                            MaxPlayer = 10,
                            MinPlayer = 5,
                            MinimumAge = 18,
                            Validate = true
                        },
                        new Game
                        {
                            Name = "Complot",
                            Description = "jeu avec rôles cachés",
                            Price = 11.99m,
                            MaxPlayer = 8,
                            MinPlayer = 2,
                            MinimumAge = 8,
                            Validate = true
                        }, new Game
                        {
                            Name = "Bang the bullet",
                            Description = "Jeu western ",
                            Price = 36.00m,
                            MaxPlayer = 8,
                            MinPlayer = 3,
                            MinimumAge = 8,
                            Validate = true
                        }, new Game
                        {
                            Name = "Bang",
                            Description = "Jeu western",
                            Price = 18,
                            MaxPlayer = 7,
                            MinPlayer = 4,
                            MinimumAge = 8,
                            Validate = true
                        }, new Game
                        {
                            Name = "Sheriff de Nottingham",
                            Description = "Jeu de commerce",
                            Price = 18,
                            MaxPlayer = 5,
                            MinPlayer = 3,
                            MinimumAge = 13,
                            Validate = true
                        }, new Game
                        {
                            Name = "When I dream",
                            Description = "Jeu de devinettes",
                            Price = 18,
                            MaxPlayer = 10,
                            MinPlayer = 4,
                            MinimumAge = 8,
                            GameTime = 30,
                            EditorId = ediors.Single(s => s.Name == "Repos Production").Id,
                            Validate = false
                        }, new Game
                        {
                            Name = "Not Alone",
                            Description = "Jeu de d'exploration",
                            Price = 18,
                            MaxPlayer = 10,
                            MinPlayer = 4,
                            MinimumAge = 8,
                            GameTime = 30 ,
                            EditorId = ediors.Single(s => s.Name == "Geek Attitude Games").Id,
                            Validate = true
                        }
                    };
                    AddInDataBase(games, context);

                    //======================================= Add Relation Theme =======================================
                    var contextGames = context.Games;
                    var contextTheme = context.Theme;

                    var ThemesGames = new ThemesGames[]
                    {
                        new ThemesGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "Abyss").Id,
                            ThemeId = contextTheme.Single(s => s.Name == "Abysse").Id,

                        },new ThemesGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "When I Dream").Id,
                            ThemeId = contextTheme.Single(s => s.Name == "Rêve").Id,

                        },new ThemesGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "Not Alone").Id,
                            ThemeId = contextTheme.Single(s => s.Name == "Science fiction").Id,

                        }

                    };
                    AddInDataBase(ThemesGames, context);
                    //======================================= Add Relation Material =======================================
                    var contextMaterialSupport = context.MaterialSupport;

                    var materialSupportsGames = new MaterialSupportsGames[]
                    {
                        new MaterialSupportsGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "Abyss").Id,
                            MaterialSupportId = contextMaterialSupport.Single(s => s.Name == "Plateau").Id,

                        }, new MaterialSupportsGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "Abyss").Id,
                            MaterialSupportId = contextMaterialSupport.Single(s => s.Name == "Cartes").Id,

                        },new MaterialSupportsGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "When I Dream").Id,
                            MaterialSupportId = contextMaterialSupport.Single(s => s.Name == "Cartes").Id,

                        }

                    };
                    AddInDataBase(materialSupportsGames, context);
                //======================================= Add Mechansim =======================================
                var contextMechansim = context.Mechanism;

                var MechaGames = new MechanismsGames[]
                {
                        new MechanismsGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "Abyss").Id,
                            MechanismId = contextMaterialSupport.Single(s => s.Name == "Economie").Id,

                        }, new MechanismsGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "Bang").Id,
                            MechanismId = contextMaterialSupport.Single(s => s.Name == "Mensonges").Id,

                        }, new MechanismsGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "Bang").Id,
                            MechanismId = contextMaterialSupport.Single(s => s.Name == "Collaboration").Id,

                        },new MechanismsGames()
                        {
                            GameId = contextGames.Single(s => s.Name == "When I Dream").Id,
                            MechanismId = contextMaterialSupport.Single(s => s.Name == "Devinettes").Id,

                        }

                };
                AddInDataBase(materialSupportsGames, context);


            }

        }
        

        public static void DropAllData(LudothequeAccountContext context)
        {
            context.Games.RemoveRange(context.Games);
            context.Editors.RemoveRange(context.Editors);
            context.Difficulties.RemoveRange(context.Difficulties);
            context.Illustrators.RemoveRange(context.Illustrators);
            context.MaterialSupport.RemoveRange(context.MaterialSupport);
            context.Mechanism.RemoveRange(context.Mechanism);
            context.Theme.RemoveRange(context.Theme);
            context.SaveChanges();
        }

        public static void AddInDataBase<T>(T[] data, LudothequeAccountContext context)
        {
            foreach (var d in data)
            {
                context.Add(d);
            }
            context.SaveChanges();

        }
    }
}
