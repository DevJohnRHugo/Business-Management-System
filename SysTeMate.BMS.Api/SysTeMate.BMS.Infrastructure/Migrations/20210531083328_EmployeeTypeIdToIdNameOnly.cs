using Microsoft.EntityFrameworkCore.Migrations;

namespace SysTeMate.BMS.Infrastructure.Migrations
{
    public partial class EmployeeTypeIdToIdNameOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes");

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "EmployeeTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "EmployeeTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "EmployeeTypeId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "EmployeeTypes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeeTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes",
                column: "Id");

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Manager" });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Supervisor" });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Staff" });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes");

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeeTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeTypes");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId",
                table: "EmployeeTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes",
                column: "EmployeeTypeId");

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "EmployeeTypeId", "Name" },
                values: new object[] { 1, "Manager" });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "EmployeeTypeId", "Name" },
                values: new object[] { 2, "Supervisor" });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "EmployeeTypeId", "Name" },
                values: new object[] { 3, "Staff" });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "EmployeeTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
