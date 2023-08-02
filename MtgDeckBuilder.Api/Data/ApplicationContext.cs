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
                Name = "Golgari Saprolings", 
            },
            new DeckItem
            {
                Id = 2,
                Name = "Grixis Fun Stuff",
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

        modelBuilder.Entity<DeckItem>().OwnsMany(p => p.NinetyNine, a =>
        {
            a.WithOwner().HasForeignKey("DeckItemId");
            a.Property<int>("Id");
            a.HasKey("Id");
            a.HasData(
                new
                {
                    Id = 3,
                    DeckItemId = 1,
                    Name = "Dictate of Erebos",
                    ScryfallId = "9f06db70-95f9-41eb-8e5f-8bc56fd34c09"
                },
                new
                {
                    Id = 4,
                    DeckItemId = 1,
                    Name = "Tendershoot Dryad",
                    ScryfallId = "a159830a-698f-423c-9da0-a734b210ecab"
                },
                new
                {
                    Id = 5,
                    DeckItemId = 2,
                    Name = "Royal Assassin",
                    ScryfallId = "d12e8109-8215-46b5-a0af-fe7e4b6b10b0"
                }
            );
        });
    }
}
