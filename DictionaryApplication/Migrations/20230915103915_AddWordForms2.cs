using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddWordForms2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordForm_Lexemes_LexemeId",
                schema: "Dict",
                table: "WordForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordForm",
                schema: "Dict",
                table: "WordForm");

            migrationBuilder.RenameTable(
                name: "WordForm",
                schema: "Dict",
                newName: "WordForms",
                newSchema: "Dict");

            migrationBuilder.RenameIndex(
                name: "IX_WordForm_LexemeId",
                schema: "Dict",
                table: "WordForms",
                newName: "IX_WordForms_LexemeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordForms",
                schema: "Dict",
                table: "WordForms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WordForms_Lexemes_LexemeId",
                schema: "Dict",
                table: "WordForms",
                column: "LexemeId",
                principalSchema: "Dict",
                principalTable: "Lexemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordForms_Lexemes_LexemeId",
                schema: "Dict",
                table: "WordForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordForms",
                schema: "Dict",
                table: "WordForms");

            migrationBuilder.RenameTable(
                name: "WordForms",
                schema: "Dict",
                newName: "WordForm",
                newSchema: "Dict");

            migrationBuilder.RenameIndex(
                name: "IX_WordForms_LexemeId",
                schema: "Dict",
                table: "WordForm",
                newName: "IX_WordForm_LexemeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordForm",
                schema: "Dict",
                table: "WordForm",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WordForm_Lexemes_LexemeId",
                schema: "Dict",
                table: "WordForm",
                column: "LexemeId",
                principalSchema: "Dict",
                principalTable: "Lexemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
