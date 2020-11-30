﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Piranha.Data.EF.SQLServer.Migrations
{
    public partial class AddContentAndSEO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "Piranha_Sites",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MetaFollow",
                table: "Piranha_Posts",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "MetaIndex",
                table: "Piranha_Posts",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<double>(
                name: "MetaPriority",
                table: "Piranha_Posts",
                nullable: false,
                defaultValue: 0.5);

            migrationBuilder.AddColumn<bool>(
                name: "MetaFollow",
                table: "Piranha_Pages",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "MetaIndex",
                table: "Piranha_Pages",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<double>(
                name: "MetaPriority",
                table: "Piranha_Pages",
                nullable: false,
                defaultValue: 0.5);

            migrationBuilder.CreateTable(
                name: "Piranha_ContentGroups",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    CLRType = table.Column<string>(maxLength: 255, nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Icon = table.Column<string>(maxLength: 64, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_ContentGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_ContentTypes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    CLRType = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Group = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_ContentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Culture = table.Column<string>(maxLength: 6, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Taxonomies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Slug = table.Column<string>(maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    GroupId = table.Column<string>(maxLength: 64, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Taxonomies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_Content",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    TypeId = table.Column<string>(maxLength: 64, nullable: false),
                    PrimaryImageId = table.Column<Guid>(nullable: true),
                    Excerpt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_Content", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_Content_Piranha_Taxonomies_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Piranha_Taxonomies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Piranha_Content_Piranha_ContentTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Piranha_ContentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_ContentFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegionId = table.Column<string>(maxLength: 64, nullable: false),
                    FieldId = table.Column<string>(maxLength: 64, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    CLRType = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ContentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_ContentFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Piranha_ContentFields_Piranha_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Piranha_Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_ContentTaxonomies",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(nullable: false),
                    TaxonomyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_ContentTaxonomies", x => new { x.ContentId, x.TaxonomyId });
                    table.ForeignKey(
                        name: "FK_Piranha_ContentTaxonomies_Piranha_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Piranha_Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Piranha_ContentTaxonomies_Piranha_Taxonomies_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalTable: "Piranha_Taxonomies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_ContentTranslations",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Excerpt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_ContentTranslations", x => new { x.ContentId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_Piranha_ContentTranslations_Piranha_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Piranha_Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Piranha_ContentTranslations_Piranha_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Piranha_Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Piranha_ContentFieldTranslations",
                columns: table => new
                {
                    FieldId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piranha_ContentFieldTranslations", x => new { x.FieldId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_Piranha_ContentFieldTranslations_Piranha_ContentFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Piranha_ContentFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Piranha_ContentFieldTranslations_Piranha_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Piranha_Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Sites_LanguageId",
                table: "Piranha_Sites",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Content_CategoryId",
                table: "Piranha_Content",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Content_TypeId",
                table: "Piranha_Content",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_ContentFields_ContentId_RegionId_FieldId_SortOrder",
                table: "Piranha_ContentFields",
                columns: new[] { "ContentId", "RegionId", "FieldId", "SortOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_ContentFieldTranslations_LanguageId",
                table: "Piranha_ContentFieldTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_ContentTaxonomies_TaxonomyId",
                table: "Piranha_ContentTaxonomies",
                column: "TaxonomyId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_ContentTranslations_LanguageId",
                table: "Piranha_ContentTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Piranha_Taxonomies_GroupId_Type_Slug",
                table: "Piranha_Taxonomies",
                columns: new[] { "GroupId", "Type", "Slug" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Piranha_Sites_Piranha_Languages_LanguageId",
                table: "Piranha_Sites",
                column: "LanguageId",
                principalTable: "Piranha_Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piranha_Sites_Piranha_Languages_LanguageId",
                table: "Piranha_Sites");

            migrationBuilder.DropTable(
                name: "Piranha_ContentFieldTranslations");

            migrationBuilder.DropTable(
                name: "Piranha_ContentGroups");

            migrationBuilder.DropTable(
                name: "Piranha_ContentTaxonomies");

            migrationBuilder.DropTable(
                name: "Piranha_ContentTranslations");

            migrationBuilder.DropTable(
                name: "Piranha_ContentFields");

            migrationBuilder.DropTable(
                name: "Piranha_Languages");

            migrationBuilder.DropTable(
                name: "Piranha_Content");

            migrationBuilder.DropTable(
                name: "Piranha_Taxonomies");

            migrationBuilder.DropTable(
                name: "Piranha_ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Piranha_Sites_LanguageId",
                table: "Piranha_Sites");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Piranha_Sites");

            migrationBuilder.DropColumn(
                name: "MetaFollow",
                table: "Piranha_Posts");

            migrationBuilder.DropColumn(
                name: "MetaIndex",
                table: "Piranha_Posts");

            migrationBuilder.DropColumn(
                name: "MetaPriority",
                table: "Piranha_Posts");

            migrationBuilder.DropColumn(
                name: "MetaFollow",
                table: "Piranha_Pages");

            migrationBuilder.DropColumn(
                name: "MetaIndex",
                table: "Piranha_Pages");

            migrationBuilder.DropColumn(
                name: "MetaPriority",
                table: "Piranha_Pages");
        }
    }
}