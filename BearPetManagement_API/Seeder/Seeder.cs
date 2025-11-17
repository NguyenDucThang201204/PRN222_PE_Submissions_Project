using BearPetManagement_Repository.ApplicationDatabaseContext;
using BearPetManagement_Repository.Models;
using BearPetManagement_Repository.NewFolder;

namespace BearPetManagement_API.Seeder
{
    public class Seeder(ApplicationDbContext context)
    {
        public async Task Seed()
        {
            if (await context.Database.CanConnectAsync())
            {
                if (!context.BearAccounts.Any())
                {
                    var users = GetPrimaryUsers();
                    context.BearAccounts.AddRange(users);
                    await context.SaveChangesAsync();
                }

                if (!context.BearTypes.Any())
                {
                    var types = GetBearTypes();
                    context.BearTypes.AddRange(types);
                    await context.SaveChangesAsync();
                }

                if (!context.BearProfiles.Any())
                {
                    var bears = GetBears();
                    context.BearProfiles.AddRange(bears);
                    await context.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<BearType> GetBearTypes()
        {
            return new List<BearType>
            {
                new BearType
                {
                  BearTypeName = "Polar Bear",
                  Origin = "South",
                  Description = "Description",
                },
                new BearType
                {
                  BearTypeName = "Brown Bear",
                  Origin = "North",
                  Description = "Description",
                },
                new BearType
                {
                  BearTypeName = "Black Bear",
                  Origin = "West",
                  Description = "Description",
                },
            };
        }

        private IEnumerable<BearProfile> GetBears()
        {
            return new List<BearProfile>
            {
                new BearProfile
                {
                    BearProfileId = 1,
                    BearName = "Bear A",
                    BearTypeId = 1,
                    BearWeight = 250,
                    Characteristics = "Characteristics",
                    CareNeeds = "CareNeeds",

                },
                  new BearProfile
                {
                                          BearProfileId = 2,

                    BearName = "Bear B",
                    BearTypeId = 2,
                    BearWeight = 250,
                    Characteristics = "Characteristics",
                    CareNeeds = "CareNeeds",

                },  new BearProfile
                {
                                        BearProfileId = 3,

                    BearName = "Bear C",
                    BearTypeId = 3,
                    BearWeight = 250,
                    Characteristics = "Characteristics",
                    CareNeeds = "CareNeeds",

                },
            };
        }

        private IEnumerable<BearAccount> GetPrimaryUsers()
        {
            return new List<BearAccount>
            {
                new BearAccount
                {
                    RoleId = 1,
                    UserName = "admin@bear.com",
                    Email = "admin@bear.com",
                    Password = "123456",
                    FullName = "admin",
                    Phone = "0869255375",
                },
                new BearAccount
                {
                    RoleId = 2,
                    UserName = "manager@bear.com",
                    Email = "manager@bear.com",
                    Password = "123456",
                    FullName = "manager",
                    Phone = "0869255375",
                },
                new BearAccount
                {
                    RoleId = 3,
                    UserName = "staff@bear.com",
                    Email = "staff@bear.com",
                    Password = "123456",
                    FullName = "staff",
                    Phone = "0869255375",
                },
                new BearAccount
                {
                    RoleId = 4,
                    UserName = "member@bear.com",
                    Email = "member@bear.com",
                    Password = "123456",
                    FullName = "member",
                    Phone = "0869255375"
                },
            };
        }

    }
}
