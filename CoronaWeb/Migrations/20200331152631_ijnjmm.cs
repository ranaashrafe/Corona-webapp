using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaWeb.Migrations
{
    public partial class ijnjmm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GuidId",
                table: "UserModels",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuidId",
                table: "UserModels");
        }
    }
}
