using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonationApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodBanks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBanks", x => x.Id);
                });

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
                    RhesusFactor = table.Column<int>(nullable: false)
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
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
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
                    DiseaseDescription = table.Column<string>(nullable: true),
                    DiseaseStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
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
                    AdressDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    DiseaseId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HospitalData",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ContactId = table.Column<string>(nullable: true),
                    LocationId = table.Column<string>(nullable: true),
                    BloodBankId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalData_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HospitalData_BloodBanks_BloodBankId",
                        column: x => x.BloodBankId,
                        principalTable: "BloodBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HospitalData_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HospitalData_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonorData",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    ContactId = table.Column<string>(nullable: true),
                    LocationId = table.Column<string>(nullable: true),
                    DonorAveilableStatus = table.Column<int>(nullable: false),
                    BloodTypeId = table.Column<string>(nullable: true),
                    ExaminationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorData_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorData_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorData_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorData_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorData_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserHospitalData",
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
                    table.PrimaryKey("PK_ApplicationUserHospitalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserHospitalData_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserHospitalData_HospitalData_HospitalDataId",
                        column: x => x.HospitalDataId,
                        principalTable: "HospitalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HospitalBloodBanks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    BloodBankId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalBloodBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalBloodBanks_BloodBanks_BloodBankId",
                        column: x => x.BloodBankId,
                        principalTable: "BloodBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HospitalBloodBanks_HospitalData_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "HospitalData",
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
                    HospitalId = table.Column<string>(nullable: true),
                    BloodTypeId = table.Column<string>(nullable: true),
                    HospitalDataId = table.Column<string>(nullable: true)
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
                        name: "FK_Recipients_HospitalData_HospitalDataId",
                        column: x => x.HospitalDataId,
                        principalTable: "HospitalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    HospitalId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    PublishedOn = table.Column<DateTime>(nullable: false),
                    EmergencyStatus = table.Column<int>(nullable: false),
                    BloodTypeId = table.Column<string>(nullable: true),
                    NeededQuantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_HospitalData_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "HospitalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserDonorData",
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
                    table.PrimaryKey("PK_ApplicationUserDonorData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDonorData_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDonorData_DonorData_DonorDataId",
                        column: x => x.DonorDataId,
                        principalTable: "DonorData",
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
                    BloodBankId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodBags_BloodBanks_BloodBankId",
                        column: x => x.BloodBankId,
                        principalTable: "BloodBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodBags_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodBags_DonorData_DonorId",
                        column: x => x.DonorId,
                        principalTable: "DonorData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDonors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DonorDataId = table.Column<string>(nullable: true),
                    ExaminationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDonors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationDonors_DonorData_DonorDataId",
                        column: x => x.DonorDataId,
                        principalTable: "DonorData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDonors_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
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
                    DonationRequestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationEvents_Requests_DonationRequestId",
                        column: x => x.DonationRequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HospitalDonationRequests",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    HospitalDataId = table.Column<string>(nullable: true),
                    RequestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalDonationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalDonationRequests_HospitalData_HospitalDataId",
                        column: x => x.HospitalDataId,
                        principalTable: "HospitalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HospitalDonationRequests_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonorDonationEvents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DonorDataId = table.Column<string>(nullable: true),
                    DonationEventId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorDonationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorDonationEvents_DonationEvents_DonationEventId",
                        column: x => x.DonationEventId,
                        principalTable: "DonationEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorDonationEvents_DonorData_DonorDataId",
                        column: x => x.DonorDataId,
                        principalTable: "DonorData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDonorData_ApplicationUserId",
                table: "ApplicationUserDonorData",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDonorData_DonorDataId",
                table: "ApplicationUserDonorData",
                column: "DonorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDonorData_IsDeleted",
                table: "ApplicationUserDonorData",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserHospitalData_ApplicationUserId",
                table: "ApplicationUserHospitalData",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserHospitalData_HospitalDataId",
                table: "ApplicationUserHospitalData",
                column: "HospitalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserHospitalData_IsDeleted",
                table: "ApplicationUserHospitalData",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_IsDeleted",
                table: "AspNetRoles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IsDeleted",
                table: "AspNetUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_DonorData_ApplicationUserId",
                table: "DonorData",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorData_BloodTypeId",
                table: "DonorData",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorData_ContactId",
                table: "DonorData",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorData_ExaminationId",
                table: "DonorData",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorData_IsDeleted",
                table: "DonorData",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DonorData_LocationId",
                table: "DonorData",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorDonationEvents_DonationEventId",
                table: "DonorDonationEvents",
                column: "DonationEventId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorDonationEvents_DonorDataId",
                table: "DonorDonationEvents",
                column: "DonorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorDonationEvents_IsDeleted",
                table: "DonorDonationEvents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDonors_DonorDataId",
                table: "ExaminationDonors",
                column: "DonorDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDonors_ExaminationId",
                table: "ExaminationDonors",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDonors_IsDeleted",
                table: "ExaminationDonors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_DiseaseId",
                table: "Examinations",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_IsDeleted",
                table: "Examinations",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalBloodBanks_BloodBankId",
                table: "HospitalBloodBanks",
                column: "BloodBankId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalBloodBanks_HospitalId",
                table: "HospitalBloodBanks",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalBloodBanks_IsDeleted",
                table: "HospitalBloodBanks",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalData_ApplicationUserId",
                table: "HospitalData",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalData_BloodBankId",
                table: "HospitalData",
                column: "BloodBankId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalData_ContactId",
                table: "HospitalData",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalData_IsDeleted",
                table: "HospitalData",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalData_LocationId",
                table: "HospitalData",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalDonationRequests_HospitalDataId",
                table: "HospitalDonationRequests",
                column: "HospitalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalDonationRequests_IsDeleted",
                table: "HospitalDonationRequests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalDonationRequests_RequestId",
                table: "HospitalDonationRequests",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_IsDeleted",
                table: "Locations",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_BloodTypeId",
                table: "Recipients",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_HospitalDataId",
                table: "Recipients",
                column: "HospitalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_IsDeleted",
                table: "Recipients",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BloodTypeId",
                table: "Requests",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_HospitalId",
                table: "Requests",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_IsDeleted",
                table: "Requests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_IsDeleted",
                table: "Settings",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserDonorData");

            migrationBuilder.DropTable(
                name: "ApplicationUserHospitalData");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BloodBags");

            migrationBuilder.DropTable(
                name: "DonorDonationEvents");

            migrationBuilder.DropTable(
                name: "ExaminationDonors");

            migrationBuilder.DropTable(
                name: "HospitalBloodBanks");

            migrationBuilder.DropTable(
                name: "HospitalDonationRequests");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DonationEvents");

            migrationBuilder.DropTable(
                name: "DonorData");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "BloodTypes");

            migrationBuilder.DropTable(
                name: "HospitalData");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BloodBanks");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
