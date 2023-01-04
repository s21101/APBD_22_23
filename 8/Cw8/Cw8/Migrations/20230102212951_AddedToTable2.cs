using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cw8.Migrations
{
    /// <inheritdoc />
    public partial class AddedToTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicament_Medicaments_IdMedicament",
                table: "Prescription_Medicament");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicament_Prescriptions_IdPrescription",
                table: "Prescription_Medicament");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctor_IdDoctor",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_IdPatient",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicaments",
                table: "Medicaments");

            migrationBuilder.RenameTable(
                name: "Prescriptions",
                newName: "Prescription");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "Patient");

            migrationBuilder.RenameTable(
                name: "Medicaments",
                newName: "Medicament");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_IdPatient",
                table: "Prescription",
                newName: "IX_Prescription_IdPatient");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_IdDoctor",
                table: "Prescription",
                newName: "IX_Prescription_IdDoctor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription",
                column: "IdPrescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient",
                table: "Patient",
                column: "IdPatient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicament",
                table: "Medicament",
                column: "IdMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription",
                column: "IdDoctor",
                principalTable: "Doctor",
                principalColumn: "IdDoctor");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription",
                column: "IdPatient",
                principalTable: "Patient",
                principalColumn: "IdPatient");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicament_Medicament_IdMedicament",
                table: "Prescription_Medicament",
                column: "IdMedicament",
                principalTable: "Medicament",
                principalColumn: "IdMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicament_Prescription_IdPrescription",
                table: "Prescription_Medicament",
                column: "IdPrescription",
                principalTable: "Prescription",
                principalColumn: "IdPrescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicament_Medicament_IdMedicament",
                table: "Prescription_Medicament");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicament_Prescription_IdPrescription",
                table: "Prescription_Medicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient",
                table: "Patient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicament",
                table: "Medicament");

            migrationBuilder.RenameTable(
                name: "Prescription",
                newName: "Prescriptions");

            migrationBuilder.RenameTable(
                name: "Patient",
                newName: "Patients");

            migrationBuilder.RenameTable(
                name: "Medicament",
                newName: "Medicaments");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_IdPatient",
                table: "Prescriptions",
                newName: "IX_Prescriptions_IdPatient");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_IdDoctor",
                table: "Prescriptions",
                newName: "IX_Prescriptions_IdDoctor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions",
                column: "IdPrescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "IdPatient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicaments",
                table: "Medicaments",
                column: "IdMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicament_Medicaments_IdMedicament",
                table: "Prescription_Medicament",
                column: "IdMedicament",
                principalTable: "Medicaments",
                principalColumn: "IdMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicament_Prescriptions_IdPrescription",
                table: "Prescription_Medicament",
                column: "IdPrescription",
                principalTable: "Prescriptions",
                principalColumn: "IdPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctor_IdDoctor",
                table: "Prescriptions",
                column: "IdDoctor",
                principalTable: "Doctor",
                principalColumn: "IdDoctor");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_IdPatient",
                table: "Prescriptions",
                column: "IdPatient",
                principalTable: "Patients",
                principalColumn: "IdPatient");
        }
    }
}
