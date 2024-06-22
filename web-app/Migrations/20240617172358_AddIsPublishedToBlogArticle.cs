using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_app.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPublishedToBlogArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "BlogArticles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "BlogArticles");
        }
    }
}
