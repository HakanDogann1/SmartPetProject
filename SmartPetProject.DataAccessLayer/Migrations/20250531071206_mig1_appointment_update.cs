using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPetProject.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig1_appointment_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnimalId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AnimalId",
                table: "Appointments",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Animals_AnimalId",
                table: "Appointments",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Animals_AnimalId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AnimalId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Appointments");
        }
    }
}
