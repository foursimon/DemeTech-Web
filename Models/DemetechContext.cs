using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DemeTech.Models;

public partial class DemetechContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DemetechContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DemetechContext(DbContextOptions<DemetechContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }


    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<Produto> AlimentosParaVendas { get; set; } 
	public virtual DbSet<ItensCarrinho> ItensCarrinhos { get; set; }

	public virtual DbSet<CarrinhoDeProduto> Carrinho { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Cnpj);

            entity.ToTable("CLIENTE");

            entity.Property(e => e.Cnpj)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("cnpj");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Endereco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("endereco");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Numero)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senha");
        });

		modelBuilder.Entity<Produto>(entity =>
		{
			entity.HasKey(e => e.CodigoAlimento).HasName("PK__ALIMENTO__B4F0AF7DE65947A8");

			entity.ToTable("ALIMENTOS_PARA_VENDA");

			entity.Property(e => e.CodigoAlimento)
				.HasMaxLength(100)
				.IsUnicode(false)
				.HasColumnName("cod_ali_vendas");
			entity.Property(e => e.Descricao)
				.HasMaxLength(100)
				.IsUnicode(false)
				.HasColumnName("descricao");
			entity.Property(e => e.Imagem).HasColumnName("imagem");
			entity.Property(e => e.NomeDoProduto)
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("nome");
			entity.Property(e => e.Peso).HasColumnName("peso");
			entity.Property(e => e.Preco).HasColumnName("preco");
			entity.Property(e => e.QuantidadeEstoque).HasColumnName("quantidade");
			entity.Property(e => e.Categoria)
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("tipo_ali");

			//entity.HasOne(d => d.CodVendaNavigation).WithMany(p => p.AlimentosParaVenda)
			//	.HasForeignKey(d => d.CodVenda)
			//	.HasConstraintName("FK1ALIMENTOS_PARA_VENDA");
		});
		OnModelCreatingPartial(modelBuilder);

		modelBuilder.Entity<CarrinhoDeProduto>(entity =>
		{
			entity.HasKey(e => e.CarrinhoId).HasName("PK__CARRINHO__14C409DF765E9E18");

			entity.ToTable("CARRINHO");

			entity.Property(e => e.CarrinhoId)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("carrinho_id");
			entity.Property(e => e.Cnpj)
				.HasMaxLength(14)
				.IsUnicode(false)
				.HasColumnName("cnpj");
			entity.Property(e => e.PrecoTotal).HasColumnName("preco_total");
			entity.Property(e => e.QuantidadeDeProdutos).HasColumnName("quantidade");

			entity.HasOne(d => d.CnpjNavigation).WithMany(p => p.Carrinhos)
				.HasForeignKey(d => d.Cnpj)
				.HasConstraintName("fk_carrinho1");
		});

		modelBuilder.Entity<ItensCarrinho>(entity =>
		{
			entity.HasKey(e => e.ItensId).HasName("PK__itens_ca__72010F8E07EB1803");
			entity.ToTable("itens_carrinho");
			entity.Property(e => e.ItensId)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("id_itens_carrinho");

			entity.Property(e => e.CarrinhoId)
				.HasMaxLength(4)
				.IsUnicode(false)
				.HasColumnName("carrinho_id");
			entity.Property(e => e.CodProduto)
				.HasMaxLength(100)
				.IsUnicode(false)
				.HasColumnName("cod_ali_vendas");

			entity.HasOne(d => d.Carrinho).WithMany()
				.HasForeignKey(d => d.CarrinhoId)
				.HasConstraintName("fk_carrinho_id");

			entity.HasOne(d => d.CodigoProdutoNavigation).WithMany()
				.HasForeignKey(d => d.CodProduto)
				.HasConstraintName("fk_cod_ali_vendas");
		});
	}

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
