using System;
using System.Collections.Generic;
using DAO;
using DAO.Repositorios.EF.ModuloCliente;
using DAO.Repositorios.EF.ModuloCorretor;
using DAO.Repositorios.EF.ModuloImovel;
using DAO.Repositorios.EF.ModuloLogin;
using Imobiliaria.Dominio.ModuloCorretor;
using Imobiliaria.Dominio.ModuloImovel;
using Imobiliaria.Dominio.ModuloUsuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Imobiliarias;

public partial class ImobiliariaDbContext : DbContext
{
	private string ConnectionString { get; set; }
	public ImobiliariaDbContext(IConfiguration configuration)
	{
		ConnectionString = configuration.GetConnectionString("Master");
	}
	public ImobiliariaDbContext(IOptions<ConnectionStrings> optionsObject, DbContextOptions<ImobiliariaDbContext> options)
		: base(options)
	{
		ConnectionStrings conexoes = optionsObject.Value;
		ConnectionString = conexoes.Master;
	}



    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Corretor> Corretores { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Imovel> Imoveis { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Perfil> Perfis { get; set; }

    public virtual DbSet<MensagensContato> MensagensContatos { get; set; }

 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

		ClienteEntityConfiguration clienteEntityConfiguration = new();
		CorretorEntityConfiguration corretorEntityConfiguration = new();
		ImovelEntityConfiguration imovelEntityConfiguration = new();
		UsuarioEntityConfiguration usuarioEntityConfiguration = new();

		modelBuilder.ApplyConfiguration(clienteEntityConfiguration);
		modelBuilder.ApplyConfiguration(corretorEntityConfiguration);
		modelBuilder.ApplyConfiguration(imovelEntityConfiguration);
		modelBuilder.ApplyConfiguration(usuarioEntityConfiguration);


		
        modelBuilder.Entity<MensagensContato>(entity =>
        {
            entity.HasKey(e => e.MensagemId).HasName("PK__Mensagen__7C0322C6B2D1EA19");

            entity.ToTable("MensagensContato");

            entity.Property(e => e.DataEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.MensagensContatos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensagens__Clien__4BAC3F29");

            entity.HasOne(d => d.Corretor).WithMany(p => p.MensagensContatos)
                .HasForeignKey(d => d.CorretorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensagens__Corre__4CA06362");

            entity.HasOne(d => d.Imovel).WithMany(p => p.MensagensContatos)
                .HasForeignKey(d => d.ImovelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensagens__Imove__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

		public void Seed()
		{
			//Database.EnsureCreated();

			if (Corretores.Count() == 0)
			{
				Corretor corretor = new()
				{
					Cpf = "12345678915",
					Creci = "123",
					Email = "john@devolta.com",
					Nome = "John 4",
					Telefone = "666 99996669"
				};
				Corretores.Add(corretor);
				SaveChanges();
			}

			if (Clientes.Count() == 0)
			{
				Cliente cliente = new()
				{
					Cpf = "12345678915",
					Email = "john@devolta.com",
					Nome = "John 4",
					Telefone = "666 99996669"
				};
				Clientes.Add(cliente);
				SaveChanges();
			}

			if (Imoveis.Count() == 0)
			{
				Imovel imovel = new Imovel()
				{
					Area = 50,
					ClienteDonoId = Clientes.First().ClienteId,
					CorretorGestorId = Corretores.First().CorretorId,
					Descricao = "descricao",
					Endereco = "na nuvem de poeira",
					Disponivel = true,
					Negocio = 1,
					Tipo = 1,
					Valor = 2300000
				};
				this.Imoveis.Add(imovel);
				SaveChanges();
			}

			if (Usuarios.Count() == 0)
			{
				Usuarios.Add(new()
				{
					Email = "john2@wick.com",
					Nome = "John2",
					SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
					Perfil = new Perfil() { Nome = "Cliente" }//1
				});
				SaveChanges();
			}

			if (Perfis.Count() == 0)
			{
				Perfis.Add(new Perfil() { Nome = "Cliente" });
				Perfis.Add(new Perfil() { Nome = "Corretor" });
				Perfis.Add(new Perfil() { Nome = "Administrador" });
				SaveChanges();
			}

			Perfil.Perfis = Perfis.ToList();
		}


	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
