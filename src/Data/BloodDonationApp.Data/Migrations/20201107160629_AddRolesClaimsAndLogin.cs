using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonationApp.Data.Migrations
{
    public partial class AddRolesClaimsAndLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hospitals_IsDeleted",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
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

            migrationBuilder.AddColumn<string>(
                name: "DonorId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonorId",
                table: "AspNetUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientId",
                table: "AspNetUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonorId",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientId",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_DonorId",
                table: "AspNetUserRoles",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RecipientId",
                table: "AspNetUserRoles",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_DonorId",
                table: "AspNetUserLogins",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_RecipientId",
                table: "AspNetUserLogins",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_DonorId",
                table: "AspNetUserClaims",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_RecipientId",
                table: "AspNetUserClaims",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Donors_DonorId",
                table: "AspNetUserClaims",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Recipients_RecipientId",
                table: "AspNetUserClaims",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Donors_DonorId",
                table: "AspNetUserLogins",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Recipients_RecipientId",
                table: "AspNetUserLogins",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Donors_DonorId",
                table: "AspNetUserRoles",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Recipients_RecipientId",
                table: "AspNetUserRoles",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Donors_DonorId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Recipients_RecipientId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Donors_DonorId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Recipients_RecipientId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Donors_DonorId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Recipients_RecipientId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_DonorId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RecipientId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_DonorId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_RecipientId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_DonorId",
                table: "AspNetUserClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_RecipientId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "AspNetUserClaims");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Hospitals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Hospitals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Hospitals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Hospitals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Hospitals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Hospitals",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Hospitals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Hospitals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Hospitals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_IsDeleted",
                table: "Hospitals",
                column: "IsDeleted");
        }
    }
}
