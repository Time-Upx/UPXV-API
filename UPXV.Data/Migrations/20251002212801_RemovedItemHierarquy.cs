using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPXV.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedItemHierarquy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_Items_ItemNid",
                table: "Consumables");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrimonies_Items_ItemNid",
                table: "Patrimonies");

            migrationBuilder.DropTable(
                name: "ItemTag");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Units_Tid",
                table: "Units");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tags_Tid",
                table: "Tags");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Status_Tid",
                table: "Status");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Patrimonies_Tid",
                table: "Patrimonies");

            migrationBuilder.DropIndex(
                name: "IX_Patrimonies_ItemNid",
                table: "Patrimonies");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Consumables_Tid",
                table: "Consumables");

            migrationBuilder.DropIndex(
                name: "IX_Consumables_ItemNid",
                table: "Consumables");

            migrationBuilder.DropColumn(
                name: "ItemNid",
                table: "Patrimonies");

            migrationBuilder.DropColumn(
                name: "ItemNid",
                table: "Consumables");

            migrationBuilder.AlterColumn<string>(
                name: "Abbreviation",
                table: "Units",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tags",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Status",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RegisteredBy",
                table: "Patrimonies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Patrimonies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Consumables",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConsumableTag",
                columns: table => new
                {
                    ConsumableNid = table.Column<int>(type: "int", nullable: false),
                    TagsNid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableTag", x => new { x.ConsumableNid, x.TagsNid });
                    table.ForeignKey(
                        name: "FK_ConsumableTag_Consumables_ConsumableNid",
                        column: x => x.ConsumableNid,
                        principalTable: "Consumables",
                        principalColumn: "Nid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumableTag_Tags_TagsNid",
                        column: x => x.TagsNid,
                        principalTable: "Tags",
                        principalColumn: "Nid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PatrimonyTag",
                columns: table => new
                {
                    PatrimonyNid = table.Column<int>(type: "int", nullable: false),
                    TagsNid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatrimonyTag", x => new { x.PatrimonyNid, x.TagsNid });
                    table.ForeignKey(
                        name: "FK_PatrimonyTag_Patrimonies_PatrimonyNid",
                        column: x => x.PatrimonyNid,
                        principalTable: "Patrimonies",
                        principalColumn: "Nid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatrimonyTag_Tags_TagsNid",
                        column: x => x.TagsNid,
                        principalTable: "Tags",
                        principalColumn: "Nid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Units_Abbreviation",
                table: "Units",
                column: "Abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_Tid",
                table: "Units",
                column: "Tid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Tid",
                table: "Tags",
                column: "Tid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Status_Tid",
                table: "Status",
                column: "Tid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patrimonies_Tid",
                table: "Patrimonies",
                column: "Tid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumables_Tid",
                table: "Consumables",
                column: "Tid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableTag_TagsNid",
                table: "ConsumableTag",
                column: "TagsNid");

            migrationBuilder.CreateIndex(
                name: "IX_PatrimonyTag_TagsNid",
                table: "PatrimonyTag",
                column: "TagsNid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumableTag");

            migrationBuilder.DropTable(
                name: "PatrimonyTag");

            migrationBuilder.DropIndex(
                name: "IX_Units_Abbreviation",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_Tid",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Tid",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Status_Tid",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Patrimonies_Tid",
                table: "Patrimonies");

            migrationBuilder.DropIndex(
                name: "IX_Consumables_Tid",
                table: "Consumables");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Patrimonies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Consumables");

            migrationBuilder.AlterColumn<string>(
                name: "Abbreviation",
                table: "Units",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tags",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Status",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Patrimonies",
                keyColumn: "RegisteredBy",
                keyValue: null,
                column: "RegisteredBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RegisteredBy",
                table: "Patrimonies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ItemNid",
                table: "Patrimonies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemNid",
                table: "Consumables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Units_Tid",
                table: "Units",
                column: "Tid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tags_Tid",
                table: "Tags",
                column: "Tid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Status_Tid",
                table: "Status",
                column: "Tid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Patrimonies_Tid",
                table: "Patrimonies",
                column: "Tid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Consumables_Tid",
                table: "Consumables",
                column: "Tid");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Nid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Nid);
                    table.UniqueConstraint("AK_Items_Tid", x => x.Tid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItemTag",
                columns: table => new
                {
                    ItemNid = table.Column<int>(type: "int", nullable: false),
                    TagsNid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTag", x => new { x.ItemNid, x.TagsNid });
                    table.ForeignKey(
                        name: "FK_ItemTag_Items_ItemNid",
                        column: x => x.ItemNid,
                        principalTable: "Items",
                        principalColumn: "Nid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemTag_Tags_TagsNid",
                        column: x => x.TagsNid,
                        principalTable: "Tags",
                        principalColumn: "Nid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Patrimonies_ItemNid",
                table: "Patrimonies",
                column: "ItemNid");

            migrationBuilder.CreateIndex(
                name: "IX_Consumables_ItemNid",
                table: "Consumables",
                column: "ItemNid");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTag_TagsNid",
                table: "ItemTag",
                column: "TagsNid");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_Items_ItemNid",
                table: "Consumables",
                column: "ItemNid",
                principalTable: "Items",
                principalColumn: "Nid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrimonies_Items_ItemNid",
                table: "Patrimonies",
                column: "ItemNid",
                principalTable: "Items",
                principalColumn: "Nid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
