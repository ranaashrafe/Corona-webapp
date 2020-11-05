using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaWeb.Migrations
{
    public partial class ijqweeeoiuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "MedicalStateModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "MedicalStateModels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
