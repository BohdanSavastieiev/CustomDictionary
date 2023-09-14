using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddLingoInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Transcription",
                schema: "Dict",
                table: "Lexemes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LexemeInformations",
                schema: "Dict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranslatedLexemeId = table.Column<int>(type: "int", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LexemeInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LexemeInformations_Lexemes_TranslatedLexemeId",
                        column: x => x.TranslatedLexemeId,
                        principalSchema: "Dict",
                        principalTable: "Lexemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelatedLexemes",
                schema: "Dict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LexemeInformationId = table.Column<int>(type: "int", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedLexemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedLexemes_LexemeInformations_LexemeInformationId",
                        column: x => x.LexemeInformationId,
                        principalSchema: "Dict",
                        principalTable: "LexemeInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsageExamples",
                schema: "Dict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LexemeInformationId = table.Column<int>(type: "int", nullable: false),
                    NativeExample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslatedExample = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageExamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageExamples_LexemeInformations_LexemeInformationId",
                        column: x => x.LexemeInformationId,
                        principalSchema: "Dict",
                        principalTable: "LexemeInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LexemeInformations_TranslatedLexemeId",
                schema: "Dict",
                table: "LexemeInformations",
                column: "TranslatedLexemeId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedLexemes_LexemeInformationId",
                schema: "Dict",
                table: "RelatedLexemes",
                column: "LexemeInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageExamples_LexemeInformationId",
                schema: "Dict",
                table: "UsageExamples",
                column: "LexemeInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedLexemes",
                schema: "Dict");

            migrationBuilder.DropTable(
                name: "UsageExamples",
                schema: "Dict");

            migrationBuilder.DropTable(
                name: "LexemeInformations",
                schema: "Dict");

            migrationBuilder.DropColumn(
                name: "Transcription",
                schema: "Dict",
                table: "Lexemes");
        }
    }
}
