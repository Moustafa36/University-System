using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaelyCenter.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_departments_Dept_Id",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "Dept_Id",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_departments_Dept_Id",
                table: "Students",
                column: "Dept_Id",
                principalTable: "departments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_departments_Dept_Id",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "Dept_Id",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_departments_Dept_Id",
                table: "Students",
                column: "Dept_Id",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
