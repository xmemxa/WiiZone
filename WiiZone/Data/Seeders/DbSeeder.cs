namespace WiiZoneNowy.Data;
using Microsoft.EntityFrameworkCore;
using WiiZoneNowy.Models;

public static class Seeder
{
    public static async Task SeedDatabase(IServiceProvider services)
    {
        
        await using var scope = services.CreateAsyncScope();
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

        await using var db = await factory.CreateDbContextAsync();
        

        if (db.Games.Any()) return;
        
        var games = new List<Game>
        {
            new() { Title = "Wii Sports Resort",
                    CoverImage = "https://cdn.mobygames.com/5dcd94c4-abe5-11ed-ba5c-02420a00010a.webp",
                    Genre      = "Sports",
                    ReleaseYear= 2009,
                    Price      = 9.99m,
                    IsReserved = true,
                    TimesRented= 1 },

            new() { Title = "Mario Kart Wii",
                    CoverImage = "https://cdn.mobygames.com/covers/282939-mario-kart-wii-wii-front-cover.jpg",
                    Genre      = "Racing",
                    ReleaseYear= 2008,
                    Price      = 14.99m,
                    IsReserved = true,
                    TimesRented= 1 },

            new() { Title = "Mario Party 9",
                    CoverImage = "https://cdn.mobygames.com/covers/11044040-mario-party-9-wii-front-cover.jpg",
                    Genre      = "Party",
                    ReleaseYear= 2012,
                    Price      = 6.99m,
                    IsReserved = false,
                    TimesRented= 1 },

            new() { Title = "New Super Mario Bros. Wii",
                    CoverImage = "https://cdn.mobygames.com/covers/5787470-new-super-mario-bros-wii-wii-front-cover.jpg",
                    Genre      = "Platformer",
                    ReleaseYear= 2009,
                    Price      = 10.49m,
                    IsReserved = false,
                    TimesRented= 0 },
        };

        var clients = new List<Client>
        {
            new() { Name="Emily Clark",  Email="emily.clark@example.com",  Phone="+44123456789" },
            new() { Name="James Miller", Email="james.miller@example.com", Phone="+44789012345" },
            new() { Name="Olivia Scott", Email="olivia.scott@example.com", Phone="+44712345678" },
            new() { Name="Daniel White", Email="daniel.white@example.com", Phone="+44876543210" },
        };

        db.Games.AddRange(games);
        db.Clients.AddRange(clients);
        db.SaveChanges();               
        
        var tags = new List<Tag>
        {
            new() { Name = "Local Multiplayer" },
            new() { Name = "Online Multiplayer"},
            new() { Name = "Family-Friendly"   },
            new() { Name = "Party"            }
        };
        db.Tags.AddRange(tags);
        db.SaveChanges();
        
        var gameTags = new List<GameTag>
        {
            new() { GameId = games.Single(g=>g.Title=="Mario Kart Wii").GameId,
                    TagId  = tags.Single(t=>t.Name=="Local Multiplayer").TagId },

            new() { GameId = games.Single(g=>g.Title=="Mario Kart Wii").GameId,
                    TagId  = tags.Single(t=>t.Name=="Online Multiplayer").TagId },

            new() { GameId = games.Single(g=>g.Title=="Mario Party 9").GameId,
                    TagId  = tags.Single(t=>t.Name=="Party").TagId },

            new() { GameId = games.Single(g=>g.Title=="Wii Sports Resort").GameId,
                    TagId  = tags.Single(t=>t.Name=="Family-Friendly").TagId },
        };
        db.GameTags.AddRange(gameTags);
        
        var today = DateTime.Today;
        var reservations = new List<Reservation>
        {
            new()
            {
                GameId      = games.Single(g=>g.Title=="Wii Sports Resort").GameId,
                ClientId    = clients[0].ClientId,
                StartDate   = today.AddDays(-10),
                EndDate     = today.AddDays(-2),
                IsReturned  = false        
            },
            
            new()
            {
                GameId      = games.Single(g=>g.Title=="Mario Kart Wii").GameId,
                ClientId    = clients[1].ClientId,
                StartDate   = today,
                EndDate     = today.AddDays(7),
                IsReturned  = false
            },
            
            new()
            {
                GameId      = games.Single(g=>g.Title=="Mario Party 9").GameId,
                ClientId    = clients[2].ClientId,
                StartDate   = today.AddDays(-6),
                EndDate     = today.AddDays(-1),
                IsReturned  = true
            }
        };
        db.Reservations.AddRange(reservations);
        db.SaveChanges();
    }
}
