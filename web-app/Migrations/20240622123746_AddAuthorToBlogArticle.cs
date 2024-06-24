using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_app.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorToBlogArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BlogArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "BlogArticles");
        }
    }
}
