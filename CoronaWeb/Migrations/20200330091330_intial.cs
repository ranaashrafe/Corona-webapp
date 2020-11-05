using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaWeb.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiseaseModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticsModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Infected = table.Column<double>(nullable: false),
                    Dead = table.Column<double>(nullable: false),
                    Cured = table.Column<double>(nullable: false),
                    CountryModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Flag = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    StatisticsModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryModels_StatisticsModels_StatisticsModelId",
                        column: x => x.StatisticsModelId,
                        principalTable: "StatisticsModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsSourceModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    CountryModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsSourceModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsSourceModels_CountryModels_CountryModelId",
                        column: x => x.CountryModelId,
                        principalTable: "CountryModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SOSModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<int>(nullable: false),
                    CountryModelId = table.Column<int>(nullable: false),
                    SOSModel = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOSModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOSModels_CountryModels_SOSModel",
                        column: x => x.SOSModel,
                        principalTable: "CountryModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    InfictionStatus = table.Column<bool>(nullable: false),
                    CountryModelId = table.Column<int>(nullable: false),
                    UserModel = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModels_CountryModels_UserModel",
                        column: x => x.UserModel,
                        principalTable: "CountryModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    NewsSourceModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsModels_NewsSourceModels_NewsSourceModelId",
                        column: x => x.NewsSourceModelId,
                        principalTable: "NewsSourceModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Speed = table.Column<double>(nullable: false),
                    UserModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationModels_UserModels_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "UserModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalStateModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserModelId = table.Column<int>(nullable: false),
                    DiseaseModelId = table.Column<int>(nullable: false),
                    BloodType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalStateModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalStateModels_DiseaseModels_DiseaseModelId",
                        column: x => x.DiseaseModelId,
                        principalTable: "DiseaseModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalStateModels_UserModels_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "UserModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryModels_StatisticsModelId",
                table: "CountryModels",
                column: "StatisticsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationModels_UserModelId",
                table: "LocationModels",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStateModels_DiseaseModelId",
                table: "MedicalStateModels",
                column: "DiseaseModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStateModels_UserModelId",
                table: "MedicalStateModels",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsModels_NewsSourceModelId",
                table: "NewsModels",
                column: "NewsSourceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsSourceModels_CountryModelId",
                table: "NewsSourceModels",
                column: "CountryModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SOSModels_SOSModel",
                table: "SOSModels",
                column: "SOSModel");

            migrationBuilder.CreateIndex(
                name: "IX_UserModels_UserModel",
                table: "UserModels",
                column: "UserModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationModels");

            migrationBuilder.DropTable(
                name: "MedicalStateModels");

            migrationBuilder.DropTable(
                name: "NewsModels");

            migrationBuilder.DropTable(
                name: "SOSModels");

            migrationBuilder.DropTable(
                name: "DiseaseModels");

            migrationBuilder.DropTable(
                name: "UserModels");

            migrationBuilder.DropTable(
                name: "NewsSourceModels");

            migrationBuilder.DropTable(
                name: "CountryModels");

            migrationBuilder.DropTable(
                name: "StatisticsModels");
        }
    }
}
