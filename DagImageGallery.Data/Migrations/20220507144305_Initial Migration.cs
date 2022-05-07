using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DagImageGallery.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DogImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImgTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    GalleryImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImgTags_DogImages_GalleryImageId",
                        column: x => x.GalleryImageId,
                        principalTable: "DogImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImgTags_GalleryImageId",
                table: "ImgTags",
                column: "GalleryImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImgTags");

            migrationBuilder.DropTable(
                name: "DogImages");
        }
    }
}
