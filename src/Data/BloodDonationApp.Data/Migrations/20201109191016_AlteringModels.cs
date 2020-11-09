using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonationApp.Data.Migrations
{
    public partial class AlteringModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationsDonors_DonorsData_DonorId",
                table: "ExaminationsDonors");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationsDonors_DonorId",
                table: "ExaminationsDonors");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "ExaminationsDonors");

            migrationBuilder.AddColumn<string>(
                name: "DonorDataId",
                table: "ExaminationsDonors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUsersDonorsData",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    DonorDataId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersDonorsData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersDonorsData_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersDonorsData_DonorsData_DonorDataId",
                        column: x => x.DonorDataId,
                        principalTable: "DonorsData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsersHospitalsData",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    HospitalDataId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersHospitalsData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersHospitalsData_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersHospitalsData_HospitaslData_HospitalDataId",
                        column: x => x.HospitalDataId,
                        principalTable: "HospitaslData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationsDonors_DonorDataId",
                table: "ExaminationsDonors",
                column: "DonorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersDonorsData_ApplicationUserId",
                table: "ApplicationUsersDonorsData",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersDonorsData_DonorDataId",
                table: "ApplicationUsersDonorsData",
                column: "DonorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersDonorsData_IsDeleted",
                table: "ApplicationUsersDonorsData",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersHospitalsData_ApplicationUserId",
                table: "ApplicationUsersHospitalsData",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersHospitalsData_HospitalDataId",
                table: "ApplicationUsersHospitalsData",
                column: "HospitalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersHospitalsData_IsDeleted",
                table: "ApplicationUsersHospitalsData",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationsDonors_DonorsData_DonorDataId",
                table: "ExaminationsDonors",
                column: "DonorDataId",
                principalTable: "DonorsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationsDonors_DonorsData_DonorDataId",
                table: "ExaminationsDonors");

            migrationBuilder.DropTable(
                name: "ApplicationUsersDonorsData");

            migrationBuilder.DropTable(
                name: "ApplicationUsersHospitalsData");

            migrationBuilder.DropIndex(
                name: "IX_ExaminationsDonors_DonorDataId",
                table: "ExaminationsDonors");

            migrationBuilder.DropColumn(
                name: "DonorDataId",
                table: "ExaminationsDonors");

            migrationBuilder.AddColumn<string>(
                name: "DonorId",
                table: "ExaminationsDonors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationsDonors_DonorId",
                table: "ExaminationsDonors",
                column: "DonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationsDonors_DonorsData_DonorId",
                table: "ExaminationsDonors",
                column: "DonorId",
                principalTable: "DonorsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
