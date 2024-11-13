using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imobiliaria.Dominio.ModuloUsuario;

namespace DAO.Repositorios.EF.ModuloLogin
{

    public class UsuarioEntityConfiguration : IEntityTypeConfiguration<Usuario>
		{
			public void Configure(EntityTypeBuilder<Usuario> builder)
			{
				builder.ToTable("Usuarios");

				builder.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__68DA341CB2529ACE");


				builder.HasKey(u => u.UsuarioId); // Chave primária
				builder.Property(u => u.Nome).IsRequired().HasMaxLength(100); // Nome obrigatório com limite de 100 caracteres
				builder.Property(u => u.Email).IsRequired().HasMaxLength(100); // Email obrigatório e único
				builder.HasIndex(u => u.Email).IsUnique(); // Garantir que o email seja único
				builder.Property(u => u.SenhaHash).IsRequired().HasMaxLength(256); // Hash da senha
				builder.Property(u => u.DataCriacao).HasDefaultValueSql("GETDATE()"); // Data de criação padrão

				// Relacionamento entre Usuario e Perfil (muitos para um)
				builder.HasOne(u => u.Perfil)
					.WithMany(p => p.Usuarios)
					.HasForeignKey(u => u.PerfilId)
					.IsRequired(); // Perfil obrigatório

			}
		}
	}
