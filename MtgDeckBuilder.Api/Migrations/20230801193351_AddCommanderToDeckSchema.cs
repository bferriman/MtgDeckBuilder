using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MtgDeckBuilder.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCommanderToDeckSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Commander_Name",
                table: "DeckItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Commander_ScryfallId",
                table: "DeckItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "DeckItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Commander_Name", "Commander_ScryfallId" },
                values: new object[] { "Slimefoot, the Stowaway", "e8815cd9-7032-445a-aebc-cfc19bd51ee4" });

            migrationBuilder.UpdateData(
                table: "DeckItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Commander_Name", "Commander_ScryfallId" },
                values: new object[] { "Nicol Bolas, the Ravager // Nicol Bolas, the Arisen", "7b215968-93a6-4278-ac61-4e3e8c3c3943" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commander_Name",
                table: "DeckItems");

            migrationBuilder.DropColumn(
                name: "Commander_ScryfallId",
                table: "DeckItems");
        }
    }
}
