using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateUserMessageForHasBeenOpenProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecivedTime",
                table: "UserMessages");

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenOpen",
                table: "UserMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenOpen",
                table: "UserMessages");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecivedTime",
                table: "UserMessages",
                type: "datetime2",
                nullable: true);
        }
    }
}
