using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Restaurant.System.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "Members_CustomerId_fkey",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSection_MenuCategories_CategoryCode",
                table: "MenuSection");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSection_MenuSchedule_ScheduleCode",
                table: "MenuSection");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSection_Menus_MenuCode",
                table: "MenuSection");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staffs_StaffUsername",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffRoles_Roles_RoleCode",
                table: "StaffRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffRoles_Staffs_StaffUsername",
                table: "StaffRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffRoles",
                table: "StaffRoles");

            migrationBuilder.DropIndex(
                name: "IX_StaffRoles_RoleCode",
                table: "StaffRoles");

            migrationBuilder.DropIndex(
                name: "IX_StaffRoles_StaffUsername",
                table: "StaffRoles");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_MemberId",
                table: "Addresses");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Roles_RoleCode",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuSection",
                table: "MenuSection");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MenuSchedule_ScheduleCode",
                table: "MenuSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuSchedule",
                table: "MenuSchedule");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Menus_MenuCode",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.RenameTable(
                name: "MenuSection",
                newName: "MenuSections");

            migrationBuilder.RenameTable(
                name: "MenuSchedule",
                newName: "MenuSchedules");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Menu");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSection_ScheduleCode",
                table: "MenuSections",
                newName: "IX_MenuSections_ScheduleCode");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSection_MenuCode",
                table: "MenuSections",
                newName: "IX_MenuSections_MenuCode");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSection_CategoryCode",
                table: "MenuSections",
                newName: "IX_MenuSections_CategoryCode");

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

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "StaffRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "StaffRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "Roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffRoles",
                table: "StaffRoles",
                columns: new[] { "StaffUsername", "RoleCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuSections",
                table: "MenuSections",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MenuSchedules_ScheduleCode",
                table: "MenuSchedules",
                column: "ScheduleCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuSchedules",
                table: "MenuSchedules",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Menu_MenuCode",
                table: "Menu",
                column: "MenuCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Navigations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Label = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Route = table.Column<string>(type: "text", nullable: true),
                    NavigationOrder = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    NavigationGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentItemGuid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Navigations", x => x.Id);
                    table.UniqueConstraint("AK_Navigations_NavigationGuid", x => x.NavigationGuid);
                    table.ForeignKey(
                        name: "FK_Navigations_Navigations_ParentItemGuid",
                        column: x => x.ParentItemGuid,
                        principalTable: "Navigations",
                        principalColumn: "NavigationGuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NavigationRoles",
                columns: table => new
                {
                    RoleCode = table.Column<string>(type: "text", nullable: false),
                    NavigationGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CanView = table.Column<bool>(type: "boolean", nullable: false),
                    CanCreate = table.Column<bool>(type: "boolean", nullable: false),
                    CanUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    CanDelete = table.Column<bool>(type: "boolean", nullable: false),
                    NavigationId = table.Column<int>(type: "integer", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationRoles", x => new { x.NavigationGuid, x.RoleCode });
                    table.ForeignKey(
                        name: "FK_NavigationRoles_Navigations_NavigationId",
                        column: x => x.NavigationId,
                        principalTable: "Navigations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NavigationRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffRoles_RoleId",
                table: "StaffRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffRoles_StaffId",
                table: "StaffRoles",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_MemberId",
                table: "Addresses",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NavigationRoles_NavigationId",
                table: "NavigationRoles",
                column: "NavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_NavigationRoles_RoleId",
                table: "NavigationRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Navigations_ParentItemGuid",
                table: "Navigations",
                column: "ParentItemGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_Members_Customers_CustomerId",
                table: "Members",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSections_MenuCategories_CategoryCode",
                table: "MenuSections",
                column: "CategoryCode",
                principalTable: "MenuCategories",
                principalColumn: "CategoryCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSections_MenuSchedules_ScheduleCode",
                table: "MenuSections",
                column: "ScheduleCode",
                principalTable: "MenuSchedules",
                principalColumn: "ScheduleCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSections_Menu_MenuCode",
                table: "MenuSections",
                column: "MenuCode",
                principalTable: "Menu",
                principalColumn: "MenuCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staffs_StaffUsername",
                table: "Orders",
                column: "StaffUsername",
                principalTable: "Staffs",
                principalColumn: "Username",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffRoles_Roles_RoleId",
                table: "StaffRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffRoles_Staffs_StaffId",
                table: "StaffRoles",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "Members_CustomerId_fkey",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSections_MenuCategories_CategoryCode",
                table: "MenuSections");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSections_MenuSchedules_ScheduleCode",
                table: "MenuSections");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSections_Menu_MenuCode",
                table: "MenuSections");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staffs_StaffUsername",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffRoles_Roles_RoleId",
                table: "StaffRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffRoles_Staffs_StaffId",
                table: "StaffRoles");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "NavigationRoles");

            migrationBuilder.DropTable(
                name: "Navigations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffRoles",
                table: "StaffRoles");

            migrationBuilder.DropIndex(
                name: "IX_StaffRoles_RoleId",
                table: "StaffRoles");

            migrationBuilder.DropIndex(
                name: "IX_StaffRoles_StaffId",
                table: "StaffRoles");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_MemberId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuSections",
                table: "MenuSections");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MenuSchedules_ScheduleCode",
                table: "MenuSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuSchedules",
                table: "MenuSchedules");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Menu_MenuCode",
                table: "Menu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "StaffRoles");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "StaffRoles");

            migrationBuilder.RenameTable(
                name: "MenuSections",
                newName: "MenuSection");

            migrationBuilder.RenameTable(
                name: "MenuSchedules",
                newName: "MenuSchedule");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Menus");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSections_ScheduleCode",
                table: "MenuSection",
                newName: "IX_MenuSection_ScheduleCode");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSections_MenuCode",
                table: "MenuSection",
                newName: "IX_MenuSection_MenuCode");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSections_CategoryCode",
                table: "MenuSection",
                newName: "IX_MenuSection_CategoryCode");

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

            migrationBuilder.AlterColumn<string>(
                name: "RoleCode",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffRoles",
                table: "StaffRoles",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Roles_RoleCode",
                table: "Roles",
                column: "RoleCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuSection",
                table: "MenuSection",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MenuSchedule_ScheduleCode",
                table: "MenuSchedule",
                column: "ScheduleCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuSchedule",
                table: "MenuSchedule",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Menus_MenuCode",
                table: "Menus",
                column: "MenuCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StaffRoles_RoleCode",
                table: "StaffRoles",
                column: "RoleCode");

            migrationBuilder.CreateIndex(
                name: "IX_StaffRoles_StaffUsername",
                table: "StaffRoles",
                column: "StaffUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddForeignKey(
                name: "FK_Members_Customers_CustomerId",
                table: "Members",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSection_MenuCategories_CategoryCode",
                table: "MenuSection",
                column: "CategoryCode",
                principalTable: "MenuCategories",
                principalColumn: "CategoryCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSection_MenuSchedule_ScheduleCode",
                table: "MenuSection",
                column: "ScheduleCode",
                principalTable: "MenuSchedule",
                principalColumn: "ScheduleCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSection_Menus_MenuCode",
                table: "MenuSection",
                column: "MenuCode",
                principalTable: "Menus",
                principalColumn: "MenuCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staffs_StaffUsername",
                table: "Orders",
                column: "StaffUsername",
                principalTable: "Staffs",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffRoles_Roles_RoleCode",
                table: "StaffRoles",
                column: "RoleCode",
                principalTable: "Roles",
                principalColumn: "RoleCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffRoles_Staffs_StaffUsername",
                table: "StaffRoles",
                column: "StaffUsername",
                principalTable: "Staffs",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
