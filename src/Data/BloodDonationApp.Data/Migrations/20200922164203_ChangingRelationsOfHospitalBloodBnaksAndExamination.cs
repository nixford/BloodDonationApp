namespace BloodDonationApp.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangingRelationsOfHospitalBloodBnaksAndExamination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodBanks_Hospitals_HospitalId",
                table: "BloodBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_Diseases_Examinations_ExaminationId",
                table: "Diseases");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_ExaminationId",
                table: "Diseases");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_IsDeleted",
                table: "Diseases");

            migrationBuilder.DropIndex(
                name: "IX_BloodBanks_HospitalId",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "BloodBanks");

            migrationBuilder.AddColumn<string>(
                name: "DiseaseId",
                table: "Examinations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_DiseaseId",
                table: "Examinations",
                column: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Diseases_DiseaseId",
                table: "Examinations",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Diseases_DiseaseId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_DiseaseId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "Examinations");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Diseases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Diseases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExaminationId",
                table: "Diseases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Diseases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Diseases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalId",
                table: "BloodBanks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_ExaminationId",
                table: "Diseases",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_IsDeleted",
                table: "Diseases",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBanks_HospitalId",
                table: "BloodBanks",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodBanks_Hospitals_HospitalId",
                table: "BloodBanks",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Diseases_Examinations_ExaminationId",
                table: "Diseases",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
