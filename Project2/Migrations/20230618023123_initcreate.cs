using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project2.Migrations
{
    public partial class initcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Users_ComplainedOnId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Addresses_AddressId",
                table: "Delivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Marketers_Addresses_AddressId",
                table: "Marketers");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_userId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Marketers_AddressId",
                table: "Marketers");

            migrationBuilder.DropIndex(
                name: "IX_Delivers_AddressId",
                table: "Delivers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ComplainedOnId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AccountCach",
                table: "Marketers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Marketers");

            migrationBuilder.DropColumn(
                name: "AccountCach",
                table: "Delivers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Delivers");

            migrationBuilder.DropColumn(
                name: "AccountCach",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "age",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ComplainedOnId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "complaintType",
                table: "Complaints");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Notifications",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Notifications",
                newName: "Body");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_userId",
                table: "Notifications",
                newName: "IX_Notifications_SenderId");

            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "productType",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fromAge",
                table: "Popularizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "toAge",
                table: "Popularizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "zone",
                table: "Popularizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "RecieverId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Marketers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "zone",
                table: "Marketers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MarketerId",
                table: "Delivers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Delivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "zone",
                table: "Delivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MarketerId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "zone",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserUID",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecieverId",
                table: "Notifications",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_MarketerId",
                table: "Delivers",
                column: "MarketerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MarketerId",
                table: "Customers",
                column: "MarketerId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserUID",
                table: "Complaints",
                column: "UserUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Users_UserUID",
                table: "Complaints",
                column: "UserUID",
                principalTable: "Users",
                principalColumn: "UID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Marketers_MarketerId",
                table: "Customers",
                column: "MarketerId",
                principalTable: "Marketers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Marketers_MarketerId",
                table: "Delivers",
                column: "MarketerId",
                principalTable: "Marketers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_RecieverId",
                table: "Notifications",
                column: "RecieverId",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Users_UserUID",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Marketers_MarketerId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivers_Marketers_MarketerId",
                table: "Delivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_RecieverId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SenderId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_RecieverId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Delivers_MarketerId",
                table: "Delivers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_MarketerId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserUID",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "photo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "productType",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "fromAge",
                table: "Popularizations");

            migrationBuilder.DropColumn(
                name: "toAge",
                table: "Popularizations");

            migrationBuilder.DropColumn(
                name: "zone",
                table: "Popularizations");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Marketers");

            migrationBuilder.DropColumn(
                name: "zone",
                table: "Marketers");

            migrationBuilder.DropColumn(
                name: "MarketerId",
                table: "Delivers");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Delivers");

            migrationBuilder.DropColumn(
                name: "zone",
                table: "Delivers");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MarketerId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "zone",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UserUID",
                table: "Complaints");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Notifications",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Notifications",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                newName: "IX_Notifications_userId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Orders",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AccountCach",
                table: "Marketers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Marketers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccountCach",
                table: "Delivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Delivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccountCach",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComplainedOnId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "complaintType",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marketers_AddressId",
                table: "Marketers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivers_AddressId",
                table: "Delivers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ComplainedOnId",
                table: "Complaints",
                column: "ComplainedOnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Users_ComplainedOnId",
                table: "Complaints",
                column: "ComplainedOnId",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivers_Addresses_AddressId",
                table: "Delivers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marketers_Addresses_AddressId",
                table: "Marketers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_userId",
                table: "Notifications",
                column: "userId",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
