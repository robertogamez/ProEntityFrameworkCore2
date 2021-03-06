﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedApp.Migrations
{
    public partial class GeneratedDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "GenratedValue",
                table: "Employees",
                nullable: true,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenratedValue",
                table: "Employees");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Employees",
                rowVersion: true,
                nullable: true);
        }
    }
}
