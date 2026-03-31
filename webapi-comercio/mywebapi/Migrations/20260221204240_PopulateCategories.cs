using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mywebapi.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Name,ImgUrl) Values('Bebidas','bebidas.jpg')");
            mb.Sql("Insert into Categorias(Name,ImgUrl) Values('Lanches','lanches.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
