using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApplication.Migrations
{
    /// <inheritdoc />
    public partial class Fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LexemeInformations_Lexemes_TranslatedLexemeId",
                schema: "Dict",
                table: "LexemeInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedLexemes_LexemeInformations_LexemeInformationId",
                schema: "Dict",
                table: "RelatedLexemes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageExamples_LexemeInformations_LexemeInformationId",
                schema: "Dict",
                table: "UsageExamples");

            migrationBuilder.AddForeignKey(
                name: "FK_LexemeInformations_Lexemes_TranslatedLexemeId",
                schema: "Dict",
                table: "LexemeInformations",
                column: "TranslatedLexemeId",
                principalSchema: "Dict",
                principalTable: "Lexemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedLexemes_LexemeInformations_LexemeInformationId",
                schema: "Dict",
                table: "RelatedLexemes",
                column: "LexemeInformationId",
                principalSchema: "Dict",
                principalTable: "LexemeInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageExamples_LexemeInformations_LexemeInformationId",
                schema: "Dict",
                table: "UsageExamples",
                column: "LexemeInformationId",
                principalSchema: "Dict",
                principalTable: "LexemeInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LexemeInformations_Lexemes_TranslatedLexemeId",
                schema: "Dict",
                table: "LexemeInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedLexemes_LexemeInformations_LexemeInformationId",
                schema: "Dict",
                table: "RelatedLexemes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageExamples_LexemeInformations_LexemeInformationId",
                schema: "Dict",
                table: "UsageExamples");

            migrationBuilder.AddForeignKey(
                name: "FK_LexemeInformations_Lexemes_TranslatedLexemeId",
                schema: "Dict",
                table: "LexemeInformations",
                column: "TranslatedLexemeId",
                principalSchema: "Dict",
                principalTable: "Lexemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedLexemes_LexemeInformations_LexemeInformationId",
                schema: "Dict",
                table: "RelatedLexemes",
                column: "LexemeInformationId",
                principalSchema: "Dict",
                principalTable: "LexemeInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageExamples_LexemeInformations_LexemeInformationId",
                schema: "Dict",
                table: "UsageExamples",
                column: "LexemeInformationId",
                principalSchema: "Dict",
                principalTable: "LexemeInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
