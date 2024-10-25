using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FileProcessor.Migrations
{
    /// <inheritdoc />
    public partial class ProductTableSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Category", "Description", "Discount", "ImageName", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Heads", "A robot head with an unusually large eye and teloscpic neck -- excellent for exploring high spaces.", 0.20000000000000001, "head-big-eye.png", "Large Cyclops", 1220.5 },
                    { 2, "Heads", "A friendly robot head with two eyes and a smile -- great for domestic use.", 0.20000000000000001, "head-friendly.png", "Friendly Bot", 945.0 },
                    { 3, "Heads", "A large three-eyed head with a shredder for a mouth -- great for crushing light medals or shredding documents.", 0.0, "head-shredder.png", "Shredder", 1275.5 },
                    { 4, "Heads", "A simple single-eyed head -- simple and inexpensive.", 0.0, "head-single-eye.png", "Small Cyclops", 750.0 },
                    { 5, "Heads", "A robot head with three oscillating eyes -- excellent for surveillance.", 0.0, "head-surveillance.png", "Surveillance", 1255.5 },
                    { 6, "Arms", "An articulated arm with a claw -- great for reaching around corners or working in tight spaces.", 0.0, "arm-articulated-claw.png", "Articulated Arm", 275.0 },
                    { 7, "Arms", "An arm with two independent claws -- great when you need an extra hand. Need four hands? Equip your bot with two of these arms.", 0.0, "arm-dual-claw.png", "Two Clawed Arm", 285.0 },
                    { 8, "Arms", "A telescoping arm with a grabber.", 0.0, "arm-grabber.png", "Grabber Arm", 205.5 },
                    { 9, "Arms", "An arm with a propeller -- good for propulsion or as a cooling fan.", 0.10000000000000001, "arm-propeller.png", "Propeller Arm", 230.0 },
                    { 10, "Arms", "A short and stubby arm with a claw -- simple, but cheap.", 0.0, "arm-stubby-claw.png", "Stubby Claw Arm", 125.0 },
                    { 11, "Torsos", "A torso that can bend slightly at the waist and equiped with a heat guage.", 0.0, "torso-flexible-gauged.png", "Flexible Gauged Torso", 1575.0 },
                    { 12, "Torsos", "A less flexible torso with a battery gauge.", 0.0, "torso-gauged.png", "Gauged Torso", 1385.0 },
                    { 13, "Torsos", "A simple torso with a pouch for carrying items.", 0.0, "torso-pouch.png", "Pouch Torso", 785.0 },
                    { 14, "Bases", "A two wheeled base with an accelerometer for stability.", 0.0, "base-double-wheel.png", "Double Wheeled Base", 895.0 },
                    { 15, "Bases", "A rocket base capable of high speed, controlled flight.", 0.0, "base-rocket.png", "Rocket Base", 1520.5 },
                    { 16, "Bases", "A single-wheeled base with an accelerometer capable of higher speeds and navigating rougher terrain than the two-wheeled variety.", 0.10000000000000001, "base-single-wheel.png", "Single Wheeled Base", 1190.5 },
                    { 17, "Bases", "A spring base - great for reaching high places.", 0.0, "base-spring.png", "Spring Base", 1190.5 },
                    { 18, "Bases", "An inexpensive three-wheeled base. only capable of slow speeds and can only function on smooth surfaces.", 0.0, "base-triple-wheel.png", "Triple Wheeled Base", 700.5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 18);
        }
    }
}
