using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAccBE.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrgStructureID",
                table: "UserInfo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DayOff",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOff = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOffName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOff", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LeaveDay",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveDays = table.Column<double>(type: "float", nullable: false),
                    LeaveHours = table.Column<double>(type: "float", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaveDayTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveDayTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveDay", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LeaveDayType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveDayTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPerMonth = table.Column<double>(type: "float", nullable: false),
                    IsAnlLeave = table.Column<double>(type: "float", nullable: false),
                    IsSickLeave = table.Column<double>(type: "float", nullable: false),
                    IsPreLeave = table.Column<double>(type: "float", nullable: false),
                    IsNormalLeave = table.Column<double>(type: "float", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveDayType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrgStructure",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgStructureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgStructure", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OverTime",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterHours = table.Column<double>(type: "float", nullable: false),
                    ShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OverTimeTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OverTimeTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverTime", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OverTimeType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverTimeTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coefficient = table.Column<double>(type: "float", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverTimeType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roster",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TueShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WedShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThuShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SatShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SunShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TueShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WedShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThuShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FriShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SatShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SunShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roster", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoOut = table.Column<double>(type: "float", nullable: false),
                    WorkHours = table.Column<double>(type: "float", nullable: false),
                    BreakInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BreakOutTime = table.Column<double>(type: "float", nullable: false),
                    IsNightShift = table.Column<bool>(type: "bit", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOff");

            migrationBuilder.DropTable(
                name: "LeaveDay");

            migrationBuilder.DropTable(
                name: "LeaveDayType");

            migrationBuilder.DropTable(
                name: "OrgStructure");

            migrationBuilder.DropTable(
                name: "OverTime");

            migrationBuilder.DropTable(
                name: "OverTimeType");

            migrationBuilder.DropTable(
                name: "Roster");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropColumn(
                name: "OrgStructureID",
                table: "UserInfo");
        }
    }
}
