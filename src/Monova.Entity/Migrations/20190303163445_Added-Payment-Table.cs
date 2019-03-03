using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monova.Entity.Migrations
{
    public partial class AddedPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    SubscriptionId = table.Column<Guid>(nullable: false),
                    Provider = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
