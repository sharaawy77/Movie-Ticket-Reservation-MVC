﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Data.Migrations
{
    public partial class seedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
                (
                table:"Roles",
                schema:"Security",
                columns: new[] {"Id","Name","NormalizedName","ConcurrencyStamp"},
                values:new object[] {Guid.NewGuid().ToString(),"Admin","Admin".ToUpper(),Guid.NewGuid().ToString()}
                );
            migrationBuilder.InsertData
               (
               table: "Roles",
               schema: "Security",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(), Guid.NewGuid().ToString() }
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [Security].[Roles] ");
        }
    }
}
