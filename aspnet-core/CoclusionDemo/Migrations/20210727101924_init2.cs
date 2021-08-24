using Microsoft.EntityFrameworkCore.Migrations;

namespace TreeListDemo.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Fields",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldsPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldType = table.Column<byte>(type: "tinyint", nullable: false),
                    DisplayFieldprefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkingFieldPrefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryTable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fields",
                schema: "dbo");
        }
    }
}
