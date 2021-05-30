using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_HealthCare.Migrations
{
    public partial class ForPasswordEncryption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Password", "UsersInfo");

            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "UsersInfo",
                nullable: false,
                defaultValue: "Rum123"
               // oldClrType: typeof(string),
               // oldType: "nvarchar(max)"
               );

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordKey",
                table: "UsersInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordKey",
                table: "UsersInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UsersInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]));
        }
    }
}
