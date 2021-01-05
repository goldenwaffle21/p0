using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaWorld.Storing.Migrations
{
    public partial class UserIdNowOnlyGeneratedOnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APizzaModel_Order_OrderId",
                table: "APizzaModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Topping_APizzaModel_APizzaModelId",
                table: "Topping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topping",
                table: "Topping");

            migrationBuilder.DropIndex(
                name: "IX_Topping_APizzaModelId",
                table: "Topping");

            migrationBuilder.DropIndex(
                name: "IX_APizzaModel_OrderId",
                table: "APizzaModel");

            migrationBuilder.DropColumn(
                name: "APizzaModelId",
                table: "Topping");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "APizzaModel");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topping",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Topping",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topping",
                table: "Topping",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Topping",
                table: "Topping");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Topping");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topping",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "APizzaModelId",
                table: "Topping",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "APizzaModel",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topping",
                table: "Topping",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Topping_APizzaModelId",
                table: "Topping",
                column: "APizzaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_APizzaModel_OrderId",
                table: "APizzaModel",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_APizzaModel_Order_OrderId",
                table: "APizzaModel",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Topping_APizzaModel_APizzaModelId",
                table: "Topping",
                column: "APizzaModelId",
                principalTable: "APizzaModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
