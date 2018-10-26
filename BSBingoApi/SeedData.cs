using System;
using System.Linq;
using System.Threading.Tasks;
using BSBingoApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BSBingoApi
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestUser(
                services.GetRequiredService<RoleManager<UserRoleEntity>>(),
                services.GetRequiredService<UserManager<UserEntity>>()
            );

            await AddTestData(services.GetRequiredService<BSBingoApiDbContext>());
        }

        public static async Task AddTestData(BSBingoApiDbContext context)
        {
            if (context.BSWords.Any())
            {
                return;
            }


            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Udvikling",
                Category = "IT"

            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Implementere",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Team",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Teambuilding",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Samarbejde",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Lærende",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Strategi",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Commitment",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Målrettet",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Handle af",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Synlig (ledelse)",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Handleplan",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Selvbærende",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Sparring",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Innovativ",
                Category = "IT"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Perspektivere",
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Melde tilbage",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Kick-off møde",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Resultatsøgende\tVisioner",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Udfordring(er)",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Markedsorienteret",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Signal(ere)",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Profil(ere)",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Procesorienteret",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Kommunikativ",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Mål og resultatkrav",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "På sigt",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Melde ud",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Ressourcer",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Bruger",
                Category = "IT"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Tema(tiserende)",
                Category = "Generelt"
            });
            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Dialog",
                Category = "Generelt"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "dk",
                BsWord = "Scenarie",
                Category = "Generelt"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Game changer",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Disruptor",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Incentivise",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Tribe",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Omnichannel",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Vlogger",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Mumpreneur",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "seniorpreneur",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "photopreneur",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "hairpreneur",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "...preneur",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Lean",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Snapshot",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Ninja",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Guru",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Capture",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Drilldown",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Journey",
                Category = "IT"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Roadmap",
                Category = "IT"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Low-hanging fruit",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Take it offline",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Touch base",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Come-to-Jesus meeting",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Flick through an email",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Traction",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "On my radar",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "All hands on deck",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Has legs",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "Like a boss",
                Category = "Generel"
            });

            context.BSWords.Add(new BSWordEntity
            {
                Language = "en",
                BsWord = "You’ve got this",
                Category = "Generel"
            });

            await context.SaveChangesAsync();

        }


        private static async Task AddTestUser(
            RoleManager<UserRoleEntity> roleManager,
            UserManager<UserEntity> userManager)
        {
            var dataExists = roleManager.Roles.Any() || userManager.Users.Any();
            if (dataExists)
            {
                return;
            }

            await roleManager.CreateAsync(new UserRoleEntity("Admin"));

            var user = new UserEntity
            {
                Email = "ios@friisconsult.com",
                UserName = "ios@friisconsult.com",
                FirstName = "Per",
                LastName = "Friis",
                CreatedAt = DateTimeOffset.UtcNow
            };

            await userManager.CreateAsync(user, "Ub?r)um2%NPZMazakG9cwf8G");

            await userManager.AddToRoleAsync(user, "Admin");
            await userManager.UpdateAsync(user);

            await roleManager.CreateAsync(new UserRoleEntity("api"));
            var apiuser = new UserEntity
            {
                Email = "api@friismobility.com",
                UserName = "api@friismobility.com",
                FirstName = "API",
                LastName = "Friis Mobility ApS",
                CreatedAt = DateTimeOffset.UtcNow
            };

            await userManager.CreateAsync(apiuser, "Ub?r)um2%NPZMazakG9cwf8G");
            await userManager.AddToRoleAsync(apiuser, "api");
            await userManager.AddToRoleAsync(apiuser, "Admin");

            await userManager.UpdateAsync(apiuser);

            var testUser = new UserEntity
            {
                Email = "test@friismobility.com",
                UserName = "test@friismobility.com",
                FirstName = "Test",
                LastName = "User",
                CreatedAt = DateTimeOffset.UtcNow
            };

            await userManager.CreateAsync(testUser, "Nescafe18?");

        }
    }
}
