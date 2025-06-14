using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class UpdateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "ImgSrc",
                table: "Book",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Author",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Genre",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_BookId",
                table: "Genre",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Book_BookId",
                table: "Genre",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Book_BookId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_BookId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Genre");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Book",
                newName: "ImgSrc");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Author",
                newName: "Picture");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
