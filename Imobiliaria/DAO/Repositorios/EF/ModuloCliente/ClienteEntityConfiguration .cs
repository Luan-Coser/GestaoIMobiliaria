using Imobiliarias;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imobiliaria.Dominio.ModuloUsuario;

namespace DAO.Repositorios.EF.ModuloCliente
{
    public class ClienteEntityConfiguration : IEntityTypeConfiguration<Cliente>
	{
		public void Configure(EntityTypeBuilder<Cliente> builder)
		{
			builder.ToTable("Clientes");
			builder.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0871A158499");

			builder.HasIndex(e => e.Cpf, "UQ__Clientes__C1F897319F8FF376").IsUnique();

			builder.Property(e => e.Cpf)
					.HasMaxLength(11)
					.HasColumnName("CPF");
			builder.Property(e => e.Email).HasMaxLength(100);
			builder.Property(e => e.Nome).HasMaxLength(100);
			builder.Property(e => e.Telefone).HasMaxLength(20);

			builder
				.HasOne(e => e.Usuario)
				.WithOne(e => e.Cliente)
				.HasForeignKey<Usuario>(e => e.ClienteId)
				.HasConstraintName("FK_Usuario_Cliente");

		}
	}
}
