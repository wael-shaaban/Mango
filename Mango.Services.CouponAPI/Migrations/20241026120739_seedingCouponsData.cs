using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedingCouponsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[,]
                {
                    { 1, "DISCOUNT10", 10.0, 100 },
                    { 2, "SAVE15", 15.0, 150 },
                    { 3, "SPRING20", 20.0, 200 },
                    { 4, "SUMMER25", 25.0, 250 },
                    { 5, "FALL30", 30.0, 300 },
                    { 6, "WINTER35", 35.0, 350 },
                    { 7, "NEWYEAR40", 40.0, 400 },
                    { 8, "HOLIDAY45", 45.0, 450 },
                    { 9, "FLASH50", 50.0, 500 },
                    { 10, "SUPER55", 55.0, 550 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 10);
        }
    }
}
