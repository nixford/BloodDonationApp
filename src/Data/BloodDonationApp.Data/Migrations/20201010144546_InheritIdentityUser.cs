namespace BloodDonationApp.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InheritIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Recipients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Recipients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Recipients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Recipients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Recipients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Recipients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Hospitals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Hospitals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Hospitals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Hospitals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Hospitals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Hospitals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Donors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Donors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Donors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Donors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Donors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Donors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Donors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Donors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Donors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Donors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Donors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Donors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Donors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Donors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Donors");
        }
    }
}
