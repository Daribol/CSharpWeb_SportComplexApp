using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportComplexApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrainerImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/kiril_raikov.jpg");

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?w=500&h=500&fit=crop");

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/koceto.jpg");

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1512217649539-75b22b15525c?w=500&h=500&fit=crop");

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/grisho.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://nutrigen.bg/_cms/wp-content/uploads/2019/11/Kiril-Raykov-profile-pic-702x1024.jpg");

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://static.dir.bg/uploads/images/2015/08/03/691218/orig.jpg?_=1526561264");

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://toppresa.com/318883/%D0%BA%D0%BE%D1%81%D1%82%D0%B0%D0%B4%D0%B8%D0%BD-%D0%BB%D0%B5%D1%84%D1%82%D0%B5%D1%80%D0%BE%D0%B2-%D0%BE%D1%82-%D0%B3%D0%BE%D1%86%D0%B5-%D0%B4%D0%B5%D0%BB%D1%87%D0%B5%D0%B2-%D0%B5%D0%B4%D0%BD%D0%B0");

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://www.pluvane.com/wp-content/uploads/2021/02/coach_13.jpg");

            migrationBuilder.UpdateData(
                schema: "22180008",
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fbg.wikipedia.org%2Fwiki%2F%25D0%2593%25D1%2580%25D0%25B8%25D0%25B3%25D0%25BE%25D1%2580_%25D0%2594%25D0%25B8%25D0%25BC%25D0%25B8%25D1%2582%25D1%2580%25D0%25BE%25D0%25B2&psig=AOvVaw3qECx0GFabpMtQcgCFtLWW&ust=1755155485839000&source=images&cd=vfe&opi=89978449&ved=2ahUKEwj13avrnYePAxWcb_EDHVP9G7EQjRx6BAgAEBo");
        }
    }
}
