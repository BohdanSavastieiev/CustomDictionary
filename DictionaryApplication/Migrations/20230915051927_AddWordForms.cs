using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddWordForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordForm",
                schema: "Dict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LexemeId = table.Column<int>(type: "int", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordForm_Lexemes_LexemeId",
                        column: x => x.LexemeId,
                        principalSchema: "Dict",
                        principalTable: "Lexemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordForm_LexemeId",
                schema: "Dict",
                table: "WordForm",
                column: "LexemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordForm",
                schema: "Dict");
        }
    }
}
