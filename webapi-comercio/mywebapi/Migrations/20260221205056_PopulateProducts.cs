using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mywebapi.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
    mb.Sql("INSERT INTO Produtos (CategoriaId, DataCadastro, Descricao, Estoque, ImagemUrl, Nome, Preco) " +
           "VALUES (1, datetime('now'), 'Refrigerante lata 350ml', 100, 'refrigerante.jpg', 'Coca-Cola Lata', 5.50);");

    mb.Sql("INSERT INTO Produtos (CategoriaId, DataCadastro, Descricao, Estoque, ImagemUrl, Nome, Preco) " +
           "VALUES (2, datetime('now'), 'Mc Lanche', 200, 'lanche.jpg', 'Mc Lanche Especial', 15.50);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
