using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IPG.Projeto.MVC.Migrations
{
    public partial class Council_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AreaID",
                table: "Council",
                newName: "CouncilID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouncilID",
                table: "Council",
                newName: "AreaID");
        }
    }
}
