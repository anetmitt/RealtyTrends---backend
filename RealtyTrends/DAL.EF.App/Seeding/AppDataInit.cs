using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Seeding;

public class AppDataInit
{
    private static readonly Guid AdminId = Guid.Parse("fb409184-0255-462b-8fec-fe89f77426c6");
    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }
    
    public static void SeedUser(UserManager<AppUser> userManager)
    {
        (Guid id, string email, string pwd) userData = (AdminId, "admin@realtytrends.com", "lill@Kapsas19");
        var user = userManager.FindByEmailAsync(userData.email).Result;
        
        if (user == null)
        {
            user = new AppUser
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                FirstName = "Admin",
                LastName = "RealtyTrends",
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, userData.pwd).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Can not seed the user");
            }
        }
    }

    private static void SeedRole(RoleManager<AppRole> roleManager)
    {
        var role = roleManager.FindByNameAsync("admin").Result;

        if (role != null) return;
        
        role = new AppRole { Name = "admin" };
        
        var result = roleManager.CreateAsync(role).Result;
        
        if (!result.Succeeded)
        {
            throw new ApplicationException($"Can not seed the role");
        }
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ApplicationDbContext context)
    {
        SeedUser(userManager);
        SeedRole(roleManager);
        context.SaveChanges();
        AddRoleToTheUser(userManager, roleManager);
    }

    private static void AddRoleToTheUser(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        var role = roleManager.FindByNameAsync("admin").Result;
        var user = userManager.FindByEmailAsync("admin@realtytrends.com").Result;
        userManager.AddToRoleAsync(user, role!.Name).Wait();
    }

    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedAppDataFilterTypes(context);
        context.SaveChanges();
        SeedAppDataPropertyTypes(context);
        SeedAppDataWebsites(context);
        SeedAppDataPropertyFields(context);
        SeedAppDataRegionTypes(context);
        SeedAppDataTransactionTypes(context);
        context.SaveChanges();
    }
    
    private static void SeedAppDataFilterTypes(ApplicationDbContext context)
    {
        if (context.FilterTypes.Any()) return;
        
        var filterTypes = new List<FilterType>
        {
            new() { Name = "Property type" },
            new() { Name = "Transaction type" },
            new() { Name = "Price Per Unit" },
            new() { Name = "Rooms" },
            new() { Name = "Area" },
            new() { Name = "Floors" },
            new() { Name = "Energy class" },
            new() { Name = "County" },
            new() { Name = "City" },
            new() { Name = "Parish" },
            new() { Name = "Street" }
        };

        context.FilterTypes.AddRange(filterTypes);
    }

    private static void SeedAppDataPropertyTypes(ApplicationDbContext context)
    {
        if (context.PropertyTypes.Any()) return;
        var propertyTypes = new List<PropertyType>
        {
            new() { Name = "Apartment" },
            new() { Name = "House" },
            new() { Name = "House Share" },
            new() { Name = "Cottage" },
            new() { Name = "Commercial Space" },
            new() { Name = "Land" },
            new() { Name = "Garage" },
            new() { Name = "New Project" },
        };

        context.PropertyTypes.AddRange(propertyTypes);
    }

    private static void SeedAppDataWebsites(ApplicationDbContext context)
    {
        if (context.Websites.Any()) return;
        var websites = new List<Website>
        {
            new() { Url = "kv.ee" , Name = "KV"},
            new() { Url = "city24.ee", Name = "City24"}
        };
        
        context.Websites.AddRange(websites);
    }

    private static void SeedAppDataPropertyFields(ApplicationDbContext context)
    {
        if (context.PropertyFields.Any()) return;
        var propertyFields = new List<PropertyField>
        {
            new() { Name = "Price" },
            new() { Name = "Rooms" },
            new() { Name = "Area" },
            new() { Name = "Price Per Unit" }
        };

        context.PropertyFields.AddRange(propertyFields);
    }

    private static void SeedAppDataRegionTypes(ApplicationDbContext context)
    {
        if (context.RegionTypes.Any()) return;
        var regionTypes = new List<RegionType>
        {
            new() { Name = "County" },
            new() { Name = "City" },
            new() { Name = "Parish" },
            new() { Name = "Street" }
        };
        
        context.RegionTypes.AddRange(regionTypes);
    }

    private static void SeedAppDataTransactionTypes(ApplicationDbContext context)
    {
        if (context.TransactionTypes.Any()) return;
        var transactionTypes = new List<TransactionType>
        {
            new() { Name = "Sale" },
            new() { Name = "Rent" }
        };

        context.TransactionTypes.AddRange(transactionTypes);
    }
}