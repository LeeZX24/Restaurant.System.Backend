using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Restaurant.System.Data.Migrations
{
    /// <inheritdoc />
    public partial class _2ndDbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffRoles",
                table: "StaffRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NavigationRoles",
                table: "NavigationRoles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "StaffRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "StaffRoles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "StaffUsername",
                table: "StaffRoles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NavigationRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "NavigationRoles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffRoles",
                table: "StaffRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NavigationRoles",
                table: "NavigationRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StaffRoles_StaffUsername_RoleCode",
                table: "StaffRoles",
                columns: new[] { "StaffUsername", "RoleCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavigationRoles_NavigationGuid_RoleCode",
                table: "NavigationRoles",
                columns: new[] { "NavigationGuid", "RoleCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffRoles",
                table: "StaffRoles");

            migrationBuilder.DropIndex(
                name: "IX_StaffRoles_StaffUsername_RoleCode",
                table: "StaffRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NavigationRoles",
                table: "NavigationRoles");

            migrationBuilder.DropIndex(
                name: "IX_NavigationRoles_NavigationGuid_RoleCode",
                table: "NavigationRoles");

            migrationBuilder.AlterColumn<string>(
                name: "StaffUsername",
                table: "StaffRoles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "StaffRoles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "StaffRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "NavigationRoles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NavigationRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffRoles",
                table: "StaffRoles",
                columns: new[] { "StaffUsername", "RoleCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NavigationRoles",
                table: "NavigationRoles",
                columns: new[] { "NavigationGuid", "RoleCode" });
        }
    }
}
