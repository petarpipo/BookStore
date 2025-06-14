using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class AddReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Book_BookId",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DownvoteCount",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpvoteCount",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Book_BookId",
                table: "Review",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Book_BookId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "DownvoteCount",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "UpvoteCount",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Book_BookId",
                table: "Review",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id");
        }
    }
}
