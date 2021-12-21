using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreWebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblStudent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStudent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblSubject",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(maxLength: 50, nullable: false),
                    Teacher = table.Column<string>(maxLength: 50, nullable: false),
                    Classroom = table.Column<string>(maxLength: 50, nullable: false),
                    Mark = table.Column<double>(type: "Float", nullable: true),
                    IDStudent = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblSubject_tblStudent_IDStudent",
                        column: x => x.IDStudent,
                        principalTable: "tblStudent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblSubject_IDStudent",
                table: "tblSubject",
                column: "IDStudent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblSubject");

            migrationBuilder.DropTable(
                name: "tblStudent");
        }
    }
}
