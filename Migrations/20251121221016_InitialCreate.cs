using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mentor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_USER",
                columns: table => new
                {
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_FULLNAME = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    DS_EMAIL = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    DS_PASSWORD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CAREER_GOAL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_USER", x => x.ID_USER);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_SKILL",
                columns: table => new
                {
                    ID_SKILL = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_SKILL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_SKILL", x => x.ID_SKILL);
                    table.ForeignKey(
                        name: "FK_TB_M_SKILL_TB_M_USER_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "TB_M_USER",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_SKILL_ID_USER",
                table: "TB_M_SKILL",
                column: "ID_USER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_SKILL");

            migrationBuilder.DropTable(
                name: "TB_M_USER");
        }
    }
}
