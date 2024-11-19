using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DegirmenciGida.Product.Infrastructure.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UpdateDateTime",
                table: "Products",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "IsRecordValid",
                table: "Products",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Products",
                newName: "IsRecordValid");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Products",
                newName: "UpdateDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDateTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
