
#pragma warning disable CS1591

// <auto-generated>

namespace Nabble.Core.Data.Migrations
{
	using System;
	using Microsoft.Data.Entity.Migrations;

	public partial class Initial : Migration
	{
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable("Badge");
			migrationBuilder.DropTable("Project");
			migrationBuilder.DropTable("Request");
		}

		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Badge",
				columns:
					table =>
						new
						{
							Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
							BadgeIdentifier = table.Column<string>(nullable: true),
							Created = table.Column<DateTime>(nullable: false)
						},
				constraints: table => { table.PrimaryKey("PK_Badge", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "Project",
				columns:
					table =>
						new
						{
							Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
							AccountName = table.Column<string>(nullable: true),
							DateTime = table.Column<DateTime>(nullable: false),
							ProjectName = table.Column<string>(nullable: true)
						},
				constraints: table => { table.PrimaryKey("PK_Project", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "Request",
				columns:
					table =>
						new
						{
							Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
							DateTime = table.Column<DateTime>(nullable: false)
						},
				constraints: table => { table.PrimaryKey("PK_Request", x => x.Id); });

			migrationBuilder.CreateIndex(
				name: "IX_Badge_BadgeIdentifier",
				table: "Badge",
				column: "BadgeIdentifier",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Project_AccountName_ProjectName",
				table: "Project",
				columns: new[] { "AccountName", "ProjectName" },
				unique: true);
		}
	}
}