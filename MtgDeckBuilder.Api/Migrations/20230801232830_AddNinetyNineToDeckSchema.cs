using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MtgDeckBuilder.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddNinetyNineToDeckSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeckItems_NinetyNine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScryfallId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckItems_NinetyNine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeckItems_NinetyNine_DeckItems_DeckItemId",
                        column: x => x.DeckItemId,
                        principalTable: "DeckItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeckItems_NinetyNine",
                columns: new[] { "Id", "DeckItemId", "Name", "ScryfallId" },
                values: new object[,]
                {
                    { 3, 1, "Dictate of Erebos", "9f06db70-95f9-41eb-8e5f-8bc56fd34c09" },
                    { 4, 1, "Tendershoot Dryad", "a159830a-698f-423c-9da0-a734b210ecab" },
                    { 5, 2, "Royal Assassin", "d12e8109-8215-46b5-a0af-fe7e4b6b10b0" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeckItems_NinetyNine_DeckItemId",
                table: "DeckItems_NinetyNine",
                column: "DeckItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeckItems_NinetyNine");
        }
    }
}
