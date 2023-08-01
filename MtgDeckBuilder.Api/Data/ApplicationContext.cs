using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace MtgDeckBuilder.Api.Data;

public class ApplicationContext : DbContext
{
    public DbSet<DeckItem> DeckItems { get; set; } = null!;

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeckItem>().HasData(
            new DeckItem
            {
                Id = 1,
                // Commander = new Card
                // {
                //     // CardId = 1,
                //     Name = "Slimefoot, the Stowaway",
                //     ScryfallId = "e8815cd9-7032-445a-aebc-cfc19bd51ee4"
                // },
                Name = "Golgari Saprolings", 
                // NinetyNine = {}
            },
            new DeckItem
            {
                Id = 2,
                // Commander = new Card
                // {
                //     // CardId = 2,
                //     Name = "Nicol Bolas, the Ravager // Nicol Bolas, the Arisen",
                //     ScryfallId = "7b215968-93a6-4278-ac61-4e3e8c3c3943"
                // },
                Name = "Grixis Fun Stuff",
                // NinetyNine = {}
            }
        );
        
        modelBuilder.Entity<DeckItem>().OwnsOne(p => p.Commander).HasData(
            new
            {
                DeckItemId = 1,
                Name = "Slimefoot, the Stowaway", 
                ScryfallId = "e8815cd9-7032-445a-aebc-cfc19bd51ee4"
            },
            new
            {
                DeckItemId = 2,
                Name = "Nicol Bolas, the Ravager // Nicol Bolas, the Arisen",
                ScryfallId = "7b215968-93a6-4278-ac61-4e3e8c3c3943"
            }
        );
    }
}
