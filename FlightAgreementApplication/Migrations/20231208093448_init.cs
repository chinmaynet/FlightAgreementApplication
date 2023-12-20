using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightAgreementApplication.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirlineManagers",
                columns: table => new
                {
                    AirlineManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineManagerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirlineManagerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirlineManagerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirlineManagerLandLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineManagers", x => x.AirlineManagerId);
                });

            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirlineEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirlinePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirlineLandLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirlineContactPreferences = table.Column<int>(type: "int", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineID);
                });

            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    AirportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirportCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.AirportId);
                });

            migrationBuilder.CreateTable(
                name: "FlightSegment",
                columns: table => new
                {
                    FlightSegmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SegmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSegment", x => x.FlightSegmentID);
                });

            migrationBuilder.CreateTable(
                name: "MasterContractParameters",
                columns: table => new
                {
                    MasterContractParameterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParameterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterContractParameters", x => x.MasterContractParameterID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    SeasonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeasonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.SeasonID);
                });

            migrationBuilder.CreateTable(
                name: "SpecialServices",
                columns: table => new
                {
                    SSID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SSRCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSRName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surcharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialServices", x => x.SSID);
                });

            migrationBuilder.CreateTable(
                name: "TourOperators",
                columns: table => new
                {
                    TourOperatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourOperatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourOperatorAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourOperatorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourOperatorPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourOperatorLandLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourOperatorContactPreferences = table.Column<int>(type: "int", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourOperators", x => x.TourOperatorId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightType = table.Column<int>(type: "int", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureAirportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationAirportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PremiumSeatsNo = table.Column<int>(type: "int", nullable: false),
                    EconomySeatsNo = table.Column<int>(type: "int", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightID);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "AirlineID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Flights_Airport_DepartureAirportId",
                        column: x => x.DepartureAirportId,
                        principalTable: "Airport",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Flights_Airport_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalTable: "Airport",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GeneralTaxes",
                columns: table => new
                {
                    TaxID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Applicability = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    ValidityStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidityEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaxIncluded = table.Column<bool>(type: "bit", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralTaxes", x => x.TaxID);
                    table.ForeignKey(
                        name: "FK_GeneralTaxes_Airport_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airport",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterContract",
                columns: table => new
                {
                    MasterContractID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    AirlineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourOperatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermsAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenewalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalWorkflow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminationClause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfidentialityAndNonDisclosure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterContract", x => x.MasterContractID);
                    table.ForeignKey(
                        name: "FK_MasterContract_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "AirlineID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterContract_TourOperators_TourOperatorId",
                        column: x => x.TourOperatorId,
                        principalTable: "TourOperators",
                        principalColumn: "TourOperatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourOperatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AirlineManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRole_AirlineManagers_AirlineManagerId",
                        column: x => x.AirlineManagerId,
                        principalTable: "AirlineManagers",
                        principalColumn: "AirlineManagerId");
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RId",
                        column: x => x.RId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_TourOperators_TourOperatorId",
                        column: x => x.TourOperatorId,
                        principalTable: "TourOperators",
                        principalColumn: "TourOperatorId");
                    table.ForeignKey(
                        name: "FK_UserRole_users_UId",
                        column: x => x.UId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightContract",
                columns: table => new
                {
                    FlightContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightContract", x => x.FlightContractId);
                    table.ForeignKey(
                        name: "FK_FlightContract_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonFlightsAssociations",
                columns: table => new
                {
                    AssociationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonFlightsAssociations", x => x.AssociationID);
                    table.ForeignKey(
                        name: "FK_SeasonFlightsAssociations_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonFlightsAssociations_Seasons_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Seasons",
                        principalColumn: "SeasonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnexRequest",
                columns: table => new
                {
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterContractID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourOperatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    ValidityStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidityEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    AnnexContractDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnexRequest", x => x.AnnexRequestID);
                    table.ForeignKey(
                        name: "FK_AnnexRequest_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "AirlineID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnnexRequest_MasterContract_MasterContractID",
                        column: x => x.MasterContractID,
                        principalTable: "MasterContract",
                        principalColumn: "MasterContractID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnnexRequest_Seasons_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Seasons",
                        principalColumn: "SeasonID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnnexRequest_TourOperators_TourOperatorID",
                        column: x => x.TourOperatorID,
                        principalTable: "TourOperators",
                        principalColumn: "TourOperatorId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AnnexFlights",
                columns: table => new
                {
                    AnnexFlightsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnexFlights", x => x.AnnexFlightsID);
                    table.ForeignKey(
                        name: "FK_AnnexFlights_AnnexRequest_AnnexRequestID",
                        column: x => x.AnnexRequestID,
                        principalTable: "AnnexRequest",
                        principalColumn: "AnnexRequestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnexFlights_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnexQuotation",
                columns: table => new
                {
                    AnnexQuotationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuotationDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnexQuotation", x => x.AnnexQuotationID);
                    table.ForeignKey(
                        name: "FK_AnnexQuotation_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "AirlineID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnexQuotation_AnnexRequest_AnnexRequestID",
                        column: x => x.AnnexRequestID,
                        principalTable: "AnnexRequest",
                        principalColumn: "AnnexRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnexRequestParameter",
                columns: table => new
                {
                    AnnexRequestParameterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterContractParameterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterContractParametersMasterContractParameterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParameterValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnexRequestParameter", x => x.AnnexRequestParameterID);
                    table.ForeignKey(
                        name: "FK_AnnexRequestParameter_AnnexRequest_AnnexRequestID",
                        column: x => x.AnnexRequestID,
                        principalTable: "AnnexRequest",
                        principalColumn: "AnnexRequestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnexRequestParameter_MasterContractParameters_MasterContractParametersMasterContractParameterID",
                        column: x => x.MasterContractParametersMasterContractParameterID,
                        principalTable: "MasterContractParameters",
                        principalColumn: "MasterContractParameterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnexRequestSpecialServices",
                columns: table => new
                {
                    AnnexRequestSpecialServicesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SSID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialServicesSSID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightSegmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnexRequestSpecialServices", x => x.AnnexRequestSpecialServicesID);
                    table.ForeignKey(
                        name: "FK_AnnexRequestSpecialServices_AnnexRequest_AnnexRequestID",
                        column: x => x.AnnexRequestID,
                        principalTable: "AnnexRequest",
                        principalColumn: "AnnexRequestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnexRequestSpecialServices_FlightSegment_FlightSegmentID",
                        column: x => x.FlightSegmentID,
                        principalTable: "FlightSegment",
                        principalColumn: "FlightSegmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnexRequestSpecialServices_SpecialServices_SpecialServicesSSID",
                        column: x => x.SpecialServicesSSID,
                        principalTable: "SpecialServices",
                        principalColumn: "SSID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseSettings",
                columns: table => new
                {
                    ReleaseSettingsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseSettings", x => x.ReleaseSettingsID);
                    table.ForeignKey(
                        name: "FK_ReleaseSettings_AnnexRequest_AnnexRequestID",
                        column: x => x.AnnexRequestID,
                        principalTable: "AnnexRequest",
                        principalColumn: "AnnexRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    TariffID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightSegmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TariffType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TariffCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSRIncluded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surcharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValidityStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidityEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MIN = table.Column<int>(type: "int", nullable: false),
                    CHDDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    INDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBlockSpace = table.Column<bool>(type: "bit", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.TariffID);
                    table.ForeignKey(
                        name: "FK_Tariffs_AnnexRequest_AnnexRequestID",
                        column: x => x.AnnexRequestID,
                        principalTable: "AnnexRequest",
                        principalColumn: "AnnexRequestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tariffs_FlightSegment_FlightSegmentID",
                        column: x => x.FlightSegmentID,
                        principalTable: "FlightSegment",
                        principalColumn: "FlightSegmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnexTaxes",
                columns: table => new
                {
                    AnnexTaxID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexFlightsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralTaxesTaxID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnexTaxes", x => x.AnnexTaxID);
                    table.ForeignKey(
                        name: "FK_AnnexTaxes_AnnexFlights_AnnexFlightsID",
                        column: x => x.AnnexFlightsID,
                        principalTable: "AnnexFlights",
                        principalColumn: "AnnexFlightsID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnnexTaxes_AnnexRequest_AnnexRequestID",
                        column: x => x.AnnexRequestID,
                        principalTable: "AnnexRequest",
                        principalColumn: "AnnexRequestID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnnexTaxes_GeneralTaxes_GeneralTaxesTaxID",
                        column: x => x.GeneralTaxesTaxID,
                        principalTable: "GeneralTaxes",
                        principalColumn: "TaxID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AnnexQuotationFlights",
                columns: table => new
                {
                    AnnexQuotationFlightsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexQuotationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnexRequestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnexQuotationFlights", x => x.AnnexQuotationFlightsID);
                    table.ForeignKey(
                        name: "FK_AnnexQuotationFlights_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "AirlineID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnnexQuotationFlights_AnnexQuotation_AnnexQuotationID",
                        column: x => x.AnnexQuotationID,
                        principalTable: "AnnexQuotation",
                        principalColumn: "AnnexQuotationID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnnexQuotationFlights_AnnexRequest_AnnexRequestID",
                        column: x => x.AnnexRequestID,
                        principalTable: "AnnexRequest",
                        principalColumn: "AnnexRequestID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnnexQuotationFlights_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseFlightSettings",
                columns: table => new
                {
                    ReleaseFlightSettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseSettingsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Days = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseSeats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    DeletedStatus = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseFlightSettings", x => x.ReleaseFlightSettingsId);
                    table.ForeignKey(
                        name: "FK_ReleaseFlightSettings_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReleaseFlightSettings_ReleaseSettings_ReleaseSettingsID",
                        column: x => x.ReleaseSettingsID,
                        principalTable: "ReleaseSettings",
                        principalColumn: "ReleaseSettingsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnexFlights_AnnexRequestID",
                table: "AnnexFlights",
                column: "AnnexRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexFlights_FlightID",
                table: "AnnexFlights",
                column: "FlightID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexQuotation_AirlineID",
                table: "AnnexQuotation",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexQuotation_AnnexRequestID",
                table: "AnnexQuotation",
                column: "AnnexRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexQuotationFlights_AirlineID",
                table: "AnnexQuotationFlights",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexQuotationFlights_AnnexQuotationID",
                table: "AnnexQuotationFlights",
                column: "AnnexQuotationID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexQuotationFlights_AnnexRequestID",
                table: "AnnexQuotationFlights",
                column: "AnnexRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexQuotationFlights_FlightID",
                table: "AnnexQuotationFlights",
                column: "FlightID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequest_AirlineID",
                table: "AnnexRequest",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequest_MasterContractID",
                table: "AnnexRequest",
                column: "MasterContractID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequest_SeasonID",
                table: "AnnexRequest",
                column: "SeasonID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequest_TourOperatorID",
                table: "AnnexRequest",
                column: "TourOperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequestParameter_AnnexRequestID",
                table: "AnnexRequestParameter",
                column: "AnnexRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequestParameter_MasterContractParametersMasterContractParameterID",
                table: "AnnexRequestParameter",
                column: "MasterContractParametersMasterContractParameterID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequestSpecialServices_AnnexRequestID",
                table: "AnnexRequestSpecialServices",
                column: "AnnexRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequestSpecialServices_FlightSegmentID",
                table: "AnnexRequestSpecialServices",
                column: "FlightSegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexRequestSpecialServices_SpecialServicesSSID",
                table: "AnnexRequestSpecialServices",
                column: "SpecialServicesSSID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexTaxes_AnnexFlightsID",
                table: "AnnexTaxes",
                column: "AnnexFlightsID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexTaxes_AnnexRequestID",
                table: "AnnexTaxes",
                column: "AnnexRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnexTaxes_GeneralTaxesTaxID",
                table: "AnnexTaxes",
                column: "GeneralTaxesTaxID");

            migrationBuilder.CreateIndex(
                name: "IX_FlightContract_FlightID",
                table: "FlightContract",
                column: "FlightID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineID",
                table: "Flights",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureAirportId",
                table: "Flights",
                column: "DepartureAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationAirportId",
                table: "Flights",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralTaxes_AirportId",
                table: "GeneralTaxes",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterContract_AirlineID",
                table: "MasterContract",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterContract_TourOperatorId",
                table: "MasterContract",
                column: "TourOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseFlightSettings_FlightID",
                table: "ReleaseFlightSettings",
                column: "FlightID");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseFlightSettings_ReleaseSettingsID",
                table: "ReleaseFlightSettings",
                column: "ReleaseSettingsID");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseSettings_AnnexRequestID",
                table: "ReleaseSettings",
                column: "AnnexRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonFlightsAssociations_FlightID",
                table: "SeasonFlightsAssociations",
                column: "FlightID");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonFlightsAssociations_SeasonID",
                table: "SeasonFlightsAssociations",
                column: "SeasonID");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_AnnexRequestID",
                table: "Tariffs",
                column: "AnnexRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_FlightSegmentID",
                table: "Tariffs",
                column: "FlightSegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_AirlineManagerId",
                table: "UserRole",
                column: "AirlineManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RId",
                table: "UserRole",
                column: "RId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_TourOperatorId",
                table: "UserRole",
                column: "TourOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UId",
                table: "UserRole",
                column: "UId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnexQuotationFlights");

            migrationBuilder.DropTable(
                name: "AnnexRequestParameter");

            migrationBuilder.DropTable(
                name: "AnnexRequestSpecialServices");

            migrationBuilder.DropTable(
                name: "AnnexTaxes");

            migrationBuilder.DropTable(
                name: "FlightContract");

            migrationBuilder.DropTable(
                name: "ReleaseFlightSettings");

            migrationBuilder.DropTable(
                name: "SeasonFlightsAssociations");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "AnnexQuotation");

            migrationBuilder.DropTable(
                name: "MasterContractParameters");

            migrationBuilder.DropTable(
                name: "SpecialServices");

            migrationBuilder.DropTable(
                name: "AnnexFlights");

            migrationBuilder.DropTable(
                name: "GeneralTaxes");

            migrationBuilder.DropTable(
                name: "ReleaseSettings");

            migrationBuilder.DropTable(
                name: "FlightSegment");

            migrationBuilder.DropTable(
                name: "AirlineManagers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "AnnexRequest");

            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropTable(
                name: "MasterContract");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "TourOperators");
        }
    }
}
