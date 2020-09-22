namespace BloodDonationApp.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class BloodDonationAppEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    BloodGroup = table.Column<int>(nullable: false),
                    RhesusFactor = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ExaminationDate = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    AdressDescription = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonationRequests",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    PublishedOn = table.Column<DateTime>(nullable: false),
                    EmergencyStatus = table.Column<int>(nullable: false),
                    ContactId = table.Column<string>(nullable: true),
                    BloodTypeId = table.Column<string>(nullable: true),
                    NeededQuantity = table.Column<double>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationRequests_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonationRequests_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DiseaseName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DiseaseStatus = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diseases_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    ContactId = table.Column<string>(nullable: true),
                    LocationId = table.Column<string>(nullable: true),
                    DonorAveilableStatus = table.Column<int>(nullable: false),
                    BloodTypeId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donors_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donors_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donors_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonationEvents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DateOfDonation = table.Column<DateTime>(nullable: false),
                    DonationRequestId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationEvents_DonationRequests_DonationRequestId",
                        column: x => x.DonationRequestId,
                        principalTable: "DonationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationsDonors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DonorId = table.Column<string>(nullable: true),
                    ExaminationId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationsDonors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationsDonors_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationsDonors_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonorsDonationEvents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DonorId = table.Column<string>(nullable: true),
                    DonationEventId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorsDonationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorsDonationEvents_DonationEvents_DonationEventId",
                        column: x => x.DonationEventId,
                        principalTable: "DonationEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorsDonationEvents_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BloodBags",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    CollectionDate = table.Column<DateTime>(nullable: false),
                    DonorId = table.Column<string>(nullable: true),
                    BloodTypeId = table.Column<string>(nullable: true),
                    BloodBankId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodBags_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodBags_Donors_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ContactId = table.Column<string>(nullable: true),
                    LocationId = table.Column<string>(nullable: true),
                    BloodBankId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitals_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hospitals_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BloodBanks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodBanks_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HospitalsDonationRequests",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    DonationRequestId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalsDonationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalsDonationRequests_DonationRequests_DonationRequestId",
                        column: x => x.DonationRequestId,
                        principalTable: "DonationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HospitalsDonationRequests_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    NeededQuantity = table.Column<double>(nullable: false),
                    RecipientEmergency = table.Column<int>(nullable: false),
                    ContactId = table.Column<string>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    BloodTypeId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipients_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipients_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipients_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipientsDonationRequests",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    RecipientId = table.Column<string>(nullable: true),
                    DonationRequestId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientsDonationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipientsDonationRequests_DonationRequests_DonationRequestId",
                        column: x => x.DonationRequestId,
                        principalTable: "DonationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipientsDonationRequests_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodBags_BloodBankId",
                table: "BloodBags",
                column: "BloodBankId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBags_BloodTypeId",
                table: "BloodBags",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBags_DonorId",
                table: "BloodBags",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBags_IsDeleted",
                table: "BloodBags",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBanks_HospitalId",
                table: "BloodBanks",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBanks_IsDeleted",
                table: "BloodBanks",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BloodTypes_IsDeleted",
                table: "BloodTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_IsDeleted",
                table: "Contacts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_ExaminationId",
                table: "Diseases",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_IsDeleted",
                table: "Diseases",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DonationEvents_DonationRequestId",
                table: "DonationEvents",
                column: "DonationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationEvents_IsDeleted",
                table: "DonationEvents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequests_BloodTypeId",
                table: "DonationRequests",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequests_ContactId",
                table: "DonationRequests",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRequests_IsDeleted",
                table: "DonationRequests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_BloodTypeId",
                table: "Donors",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_ContactId",
                table: "Donors",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_IsDeleted",
                table: "Donors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_LocationId",
                table: "Donors",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorsDonationEvents_DonationEventId",
                table: "DonorsDonationEvents",
                column: "DonationEventId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorsDonationEvents_DonorId",
                table: "DonorsDonationEvents",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorsDonationEvents_IsDeleted",
                table: "DonorsDonationEvents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_IsDeleted",
                table: "Examinations",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationsDonors_DonorId",
                table: "ExaminationsDonors",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationsDonors_ExaminationId",
                table: "ExaminationsDonors",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationsDonors_IsDeleted",
                table: "ExaminationsDonors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_BloodBankId",
                table: "Hospitals",
                column: "BloodBankId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_ContactId",
                table: "Hospitals",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_IsDeleted",
                table: "Hospitals",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_LocationId",
                table: "Hospitals",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalsDonationRequests_DonationRequestId",
                table: "HospitalsDonationRequests",
                column: "DonationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalsDonationRequests_HospitalId",
                table: "HospitalsDonationRequests",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalsDonationRequests_IsDeleted",
                table: "HospitalsDonationRequests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_IsDeleted",
                table: "Locations",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_BloodTypeId",
                table: "Recipients",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_ContactId",
                table: "Recipients",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_HospitalId",
                table: "Recipients",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_IsDeleted",
                table: "Recipients",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientsDonationRequests_DonationRequestId",
                table: "RecipientsDonationRequests",
                column: "DonationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientsDonationRequests_IsDeleted",
                table: "RecipientsDonationRequests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientsDonationRequests_RecipientId",
                table: "RecipientsDonationRequests",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodBags_BloodBanks_BloodBankId",
                table: "BloodBags",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_BloodBanks_BloodBankId",
                table: "Hospitals",
                column: "BloodBankId",
                principalTable: "BloodBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_BloodBanks_BloodBankId",
                table: "Hospitals");

            migrationBuilder.DropTable(
                name: "BloodBags");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "DonorsDonationEvents");

            migrationBuilder.DropTable(
                name: "ExaminationsDonors");

            migrationBuilder.DropTable(
                name: "HospitalsDonationRequests");

            migrationBuilder.DropTable(
                name: "RecipientsDonationRequests");

            migrationBuilder.DropTable(
                name: "DonationEvents");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "DonationRequests");

            migrationBuilder.DropTable(
                name: "BloodTypes");

            migrationBuilder.DropTable(
                name: "BloodBanks");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
