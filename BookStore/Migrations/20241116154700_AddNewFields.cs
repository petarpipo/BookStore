using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class AddNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Book",
                newName: "Overview");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Author",
                newName: "Picture");

            migrationBuilder.RenameColumn(
                name: "Genres",
                table: "Author",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Author",
                newName: "About");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InStock",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderCount",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "OrderCount",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Overview",
                table: "Book",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Author",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Author",
                newName: "Genres");

            migrationBuilder.RenameColumn(
                name: "About",
                table: "Author",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
